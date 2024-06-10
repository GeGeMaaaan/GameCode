using Gamee._Models;
using Gamee.Interface;
using Gamee.Manager;
using Gamee;
using System.Collections.Generic;
using System.Linq;
using Gamee._Manager;

public class FightManager
{
    private Skill _selectedSkill;
    private int _currentTime;
    private bool _isPaused;
    private double _elapsedTime;
    private List<ICharacter> _combatants;
    private List<ScheduledSkillExecution> _scheduledSkills;
    private List<SkillIcon> _skillIcons;
    private Hero _hero;
    private GameGrid _grid;
    public Dictionary<ICharacter,GridCell> _combatantPosDic { get; private set; }
    private SpriteFont _font;
    private Dictionary<ICharacter, bool> _characterSkillInProgress;
    private Texture2D _skillRangeTexture;
    private Texture2D _defaultCellTexture; 
    private Texture2D _hoverDefault;
    private Texture2D _rangeHoverDefault;
    private Texture2D _occupationTexture;
    private Dictionary<ICharacter, Texture2D> _characterIcons;
    private Dictionary<Type, Texture2D> _skillTypeIcons;
    private GameServiceContainer serviceContainer;
    public FightManager()
    {
        _combatantPosDic = new Dictionary<ICharacter, GridCell>();
        _characterSkillInProgress = new Dictionary<ICharacter, bool>();
        _currentTime = 0;
        _isPaused = true;
        _elapsedTime = 0.0;
        _combatants = new List<ICharacter>();
        _scheduledSkills = new List<ScheduledSkillExecution>();
        _skillIcons = new List<SkillIcon>();
    }

    public void Initialize(Hero hero, GameGrid grid,GameServiceContainer serviceContainer)
    {
        this.serviceContainer = serviceContainer;
        _hero = hero;
        _grid = grid;
        _occupationTexture = grid._occupationTexture;
        _skillRangeTexture = grid.skillRangeTexture; 
        _defaultCellTexture = grid.defaultTexture; 
        _hoverDefault = grid.defaultHoverTexture;
        _rangeHoverDefault = grid.SkillRangeHoverTexture;
        _font = Globals.Content.Load<SpriteFont>("font/File");
        LoadIcons();
        LoadSkillIcons();
        var xy = _grid.SetBaseHeroPosition(hero.CollisionComponent.BoundingBox);
        AddCombatant(hero, 0, 0);
    }

    public void AddCombatant(ICharacter combatant, int xPos, int yPos)
    {
        _combatants.Add(combatant);
        combatant.StatsComponent.EnterAFight();
        combatant.SkillComponent.EnterAFight();
        combatant.ChangeFightPos(_grid, xPos, yPos);
        _combatantPosDic[combatant] = _grid.GetCells()[xPos, yPos];
        _characterSkillInProgress[combatant] = false; 
        _characterIcons[combatant] = Globals.Content.Load<Texture2D>($"Icons/{combatant.Name}");
    }

    public void ScheduleSkillExecution(Skill skill, ICharacter user, int executionTime, GridCell target = null)
    {
        _scheduledSkills.Add(new ScheduledSkillExecution(skill, user,_currentTime, executionTime, _characterIcons[user], _skillTypeIcons[skill.GetType()], target));
        _characterSkillInProgress[user] = true; 
    }

    private void LoadSkillIcons()
    {
        Texture2D iconTexture = Globals.Content.Load<Texture2D>("SkillIcon");
        int leftHandSkillCount = _hero.SkillComponent.GetLeftHandSkills().Count;
        int rightHandSkillCount = _hero.SkillComponent.GetRightHandSkills().Count;
        int regularSkillCount = _hero.SkillComponent.GetRegularSkills().Count;
        int cellSkillCount = _hero.SkillComponent.GetCellSkills().Count;
        _skillIcons = new List<SkillIcon>();
        for (int i = 0; i < leftHandSkillCount; i++)
        {
            Rectangle boundingBox = new Rectangle(10, 400 + i * 50, 40, 40);
            _skillIcons.Add(new SkillIcon(_hero.SkillComponent.GetLeftHandSkills()[i], boundingBox, iconTexture,SkillIconPosition.left));
        }

        for (int i = 0; i < rightHandSkillCount; i++)
        {
            Rectangle boundingBox = new Rectangle(1820, 400 + i * 50, 40, 40);
            _skillIcons.Add(new SkillIcon(_hero.SkillComponent.GetRightHandSkills()[i], boundingBox, iconTexture, SkillIconPosition.right));
        }

        for (int i = 0; i < regularSkillCount+cellSkillCount; i++)
        {
            Rectangle boundingBox = new Rectangle(860 + i * 50, 200, 40, 40);
            if(i<regularSkillCount)
            {
                _skillIcons.Add(new SkillIcon(_hero.SkillComponent.GetRegularSkills()[i], boundingBox, iconTexture, SkillIconPosition.mid));
            }
            else
            {
                _skillIcons.Add(new SkillIcon(_hero.SkillComponent.GetCellSkills()[i-regularSkillCount], boundingBox, iconTexture, SkillIconPosition.mid));
            }
        }
    }

    private void LoadIcons()
    {
        _characterIcons = new Dictionary<ICharacter, Texture2D>();
        _skillTypeIcons = new Dictionary<Type, Texture2D>();
        _skillTypeIcons[typeof(AttackSkill)] = Globals.Content.Load<Texture2D>("Icons/AttackSkillIcon");
        _skillTypeIcons[typeof(MovementSkill)] = Globals.Content.Load<Texture2D>("Icons/MovementSkillIcon");
        _skillTypeIcons[typeof(ComboSkill)] = Globals.Content.Load<Texture2D>("Icons/MovementSkillIcon");
    }
    private void DrawTimeline()
    {
        int startX = 800;
        int startY = 10;
        foreach (var scheduledSkill in _scheduledSkills)
        {
            if (scheduledSkill.User != _hero)
            {
                int iconX = startX;
                int iconY = startY;
                Globals.SpriteBatch.Draw(scheduledSkill.UserIcon, new Rectangle(iconX, iconY, 40, 40), Color.White);
                Globals.SpriteBatch.DrawString(_font, $"{scheduledSkill.StartTime} -> {scheduledSkill.ExecutionTime}", new Vector2(iconX, iconY + 45), Color.White);
                Globals.SpriteBatch.Draw(scheduledSkill.SkillIcon, new Rectangle(iconX+20, iconY + 100, 20, 20), Color.White);
                startX =startX+100;
            }
        }
    }
    public void Update(float elapsedTime)
    {
        if (_combatants.Count == 1&& _combatants[0] == _hero)
        {
            serviceContainer.GetService<EventManager>().Invoke("EndFight");
            serviceContainer.GetService<EventManager>().Invoke("GameWon");
        }
        foreach(var cell in _grid.GetCells())
        {
            cell.OccupationCharacter = null;
        }
        foreach (var com in _combatantPosDic.Keys)
        {
            var gridCell = _grid.GetCells()[com.GridCoordinate.Item1, com.GridCoordinate.Item2];
            _combatantPosDic[com] = gridCell;
            gridCell.SetOccupationCharacter(com);
            if(gridCell.GetTexture()!=_grid.enemyAttackTexture)
                gridCell.SetTexture(_occupationTexture);
            if (gridCell.unique)
            {
                com.SkillComponent.SetCellSkill(gridCell.GetCellSkill());
                
            }
            else
            {
                com.SkillComponent.SetCellSkill(new List<Skill>());
            }
        }
        if (!_isPaused)
        {
            _elapsedTime += elapsedTime;

            if (_elapsedTime >= 0.5)
            {
                _currentTime++;
                _elapsedTime -= 0.5;
            }
            List<ScheduledSkillExecution> executedSkills = new List<ScheduledSkillExecution>();

            foreach (var scheduledSkill in _scheduledSkills.Where(skill => skill.ExecutionTime <= _currentTime && skill.Skill is MovementSkill))
            {
                (scheduledSkill.Skill as MovementSkill).Execute(scheduledSkill.User, _grid, _combatantPosDic[scheduledSkill.User].Coordinate);
                var gridCell = _grid.GetCells()[scheduledSkill.User.GridCoordinate.Item1, scheduledSkill.User.GridCoordinate.Item2];
                _combatantPosDic[scheduledSkill.User] = gridCell;
                gridCell.SetOccupationCharacter(scheduledSkill.User);
                executedSkills.Add(scheduledSkill);
            }

            foreach (var scheduledSkill in _scheduledSkills.Where(skill => skill.ExecutionTime <= _currentTime && skill.Skill is ComboSkill))
            {
                if (scheduledSkill.Target.OccupationCharacter != null)
                    (scheduledSkill.Skill as ComboSkill).ExecuteAttack(scheduledSkill.User, scheduledSkill.Target.OccupationCharacter);
                (scheduledSkill.Skill as ComboSkill).ExecuteMovement(scheduledSkill.User, _grid, _combatantPosDic[scheduledSkill.User].Coordinate);
                var gridCell = _grid.GetCells()[scheduledSkill.User.GridCoordinate.Item1, scheduledSkill.User.GridCoordinate.Item2];
                _combatantPosDic[scheduledSkill.User] = gridCell;
                gridCell.SetOccupationCharacter(scheduledSkill.User);
                executedSkills.Add(scheduledSkill);
            }

            foreach (var scheduledSkill in _scheduledSkills.Where(skill => skill.ExecutionTime <= _currentTime && skill.Skill is AttackSkill))
            {
                if (scheduledSkill.Target.OccupationCharacter != null)
                    (scheduledSkill.Skill as AttackSkill).Execute(scheduledSkill.User, scheduledSkill.Target.OccupationCharacter);
                executedSkills.Add(scheduledSkill);
            }

            foreach (var executedSkill in executedSkills)
            {
                _scheduledSkills.Remove(executedSkill);
                _characterSkillInProgress[executedSkill.User] = false;
            }

            if (_selectedSkill == null && executedSkills.Count > 0)
            {
                foreach (var skill in executedSkills)
                {
                    if (skill.User == _hero)
                    {
                        _isPaused = true;
                        break;
                    }
                }

            }
        }
        foreach (var combatant in _combatants.ToList()) 
        {
            if (combatant.StatsComponent.CurrentHealth <= 0)
            {
                HandleCharacterDeath(combatant);
            }
        }

        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

        
        LoadSkillIcons();
        UpdateGrid();
        CheckClick();
        HandleEnemyAI();
        UpdateSkillRangeDisplay();
        foreach (var icon in _skillIcons)
        {
            icon.Update(mousePosition);

        }
    }
    private void HandleCharacterDeath(ICharacter character)
    {
        RemoveCombatant(character);

        if (character == _hero)
        {
            serviceContainer.GetService<EventManager>().Invoke("GameOver");
            _isPaused = true;
        }
        
    }

    public void RemoveCombatant(ICharacter combatant)
    {
        _combatants.Remove(combatant);
        _combatantPosDic.Remove(combatant);
        _characterSkillInProgress.Remove(combatant);
        var cell = _combatantPosDic.ContainsKey(combatant) ? _combatantPosDic[combatant] : null;
        if (cell != null)
        {
            cell.SetOccupationCharacter(null);
        }
    }
    public void CheckClick()
    {
        bool selectSkill = false;
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

        if (InputManager.wasClickForInteraction)
        {
            foreach (var icon in _skillIcons)
            {
                if (icon.BoundingBox.Contains(mousePosition))
                {
                    _selectedSkill = icon.Skill;
                    _isPaused = true;
                    selectSkill = true;
                    break;
                }
            }

            if (_selectedSkill != null)
            {
                if (_selectedSkill is AttackSkill && !selectSkill)
                {
                    foreach (var cells in _grid.GetCells())
                    {
                        if (cells.BoundingBox.Contains(InputManager.MousePositionWorld))
                        {
                            if (IsTargetInRange(_selectedSkill, _hero, cells))
                            {
                                ScheduleSkillExecution(_selectedSkill, _hero, _currentTime + _selectedSkill.ExecutionTime, cells);
                                _selectedSkill = null;
                                _isPaused = false;
                                break;
                            }

                        }
                    }
                }
                else if(_selectedSkill is ComboSkill && !selectSkill)
                {
                    foreach (var cells in _grid.GetCells())
                    {
                        if (cells.BoundingBox.Contains(InputManager.MousePositionWorld))
                        {
                            if (IsTargetInRange(_selectedSkill, _hero, cells))
                            {
                                ScheduleSkillExecution(_selectedSkill, _hero, _currentTime + _selectedSkill.ExecutionTime, cells);
                                _selectedSkill = null;
                                _isPaused = false;
                                break;
                            }

                        }
                    }
                }
                else if (_selectedSkill is MovementSkill)
                {
                    ScheduleSkillExecution(_selectedSkill, _hero, _currentTime + _selectedSkill.ExecutionTime);
                    _selectedSkill = null;
                    _isPaused = false;
                }
            }
        }
        else if (InputManager.MouseState.RightButton == ButtonState.Pressed)
        {
            _selectedSkill = null;
        }
    }

    public bool IsTargetInRange(Skill skill, ICharacter user, GridCell target)
    {
        int X1 = _combatantPosDic[user].Coordinate.x;
        int Y1 = _combatantPosDic[user].Coordinate.y;
        int X2 = target.Coordinate.x;
        int Y2 = target.Coordinate.y;

        int distance = Math.Abs(X1 - X2) + Math.Abs(Y1 - Y2);
        return distance <= skill.Range;
                                      }

    private void HandleEnemyAI()
    {
        foreach (var enemy in _combatants)
        {
            if (enemy is Hero)
                continue;

            if (!_characterSkillInProgress[enemy])
            {
                var enemyAI = new EnemyAI(enemy, this);
                enemyAI.TakeTurn();
            }
        }
    }

    private void UpdateSkillRangeDisplay()
    {
        if (_selectedSkill != null)
        {
            foreach (var cell in _grid.GetCells())
            {
                if (IsTargetInRange(_selectedSkill, _hero, cell))
                {
                    cell.SetTexture(_skillRangeTexture);
                    cell.SetHoverTexture(_rangeHoverDefault);
                }
                else
                {
                    cell.SetTexture(_defaultCellTexture);
                    cell.SetHoverTexture(_hoverDefault);
                }
            }
        }
        else
        {
            foreach (var cell in _grid.GetCells())
            {
                cell.SetTexture(_defaultCellTexture);
                cell.SetHoverTexture(_hoverDefault);
            }
        }
        foreach (var enemy in _combatants.Where(c => !(c is Hero)))
        {
            var enemyAI = new EnemyAI(enemy, this);
            var plannedAttacks = enemyAI.GetPlannedAttackTargets();

            foreach (var cell in plannedAttacks)
            {
                cell.SetTexture(_grid.enemyAttackTexture); 
                cell.SetHoverTexture(_grid.enemyAttackTexture);
            }
        }
    }
    public List<ScheduledSkillExecution> GetScheduledSkills()
    {
        return _scheduledSkills;
    }
    public void DrawGrid()
    {
        _grid.Draw();
    }
    public void UpdateGrid()
    {
        _grid.Update();
    }

    public void DrawUI()
    {
        Globals.SpriteBatch.DrawString(_font, _currentTime.ToString(), new Vector2(960, 150), Color.White);
        foreach (var icon in _skillIcons)
        {
            icon.Draw();
        }
        DrawTimeline();
    }

    public int CurrentTurn => _currentTime;
    public List<ICharacter> Participants => _combatants;

    public class ScheduledSkillExecution
    {
        public Skill Skill { get; }
        public ICharacter User { get; }
        public GridCell Target { get; }
        public int ExecutionTime { get; }
        public Texture2D UserIcon { get; }
        public Texture2D SkillIcon { get; }
        public int StartTime { get; }
        public ScheduledSkillExecution(Skill skill, ICharacter user,int startTime, int executionTime, Texture2D userIcon, Texture2D skillIcon, GridCell target = null)
        {
            StartTime=startTime;
            Skill = skill;
            User = user;
            Target = target;
            ExecutionTime = executionTime;
            UserIcon = userIcon;
            SkillIcon = skillIcon;
        }
    }
}
