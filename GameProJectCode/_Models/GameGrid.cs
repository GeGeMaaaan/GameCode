using Gamee;
using Gamee._Models;
using Gamee.Interface;
using Gamee.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class GameGrid
{
    private GridCell[,] cells;
    private Vector2 startPosition;
    private int width;
    private int height;
    private int cellSize;
    private List<int> DontDrawRowList;
    public Texture2D defaultTexture { get; private set; }// Текстура по умолчанию
    public Texture2D skillRangeTexture { get; private set; }
    public Texture2D enemyAttackTexture { get; private set; }
    public Texture2D defaultHoverTexture { get; private set; }
    public Texture2D SkillRangeHoverTexture { get; private set; }
    public Texture2D _occupationTexture {  get; private set; }
    public GameGrid(Vector2 startPosition, int width, int height, int cellSize)
    {
        _occupationTexture = Globals.Content.Load<Texture2D>("CellOcupation");
        defaultHoverTexture = Globals.Content.Load<Texture2D>("CellDefaultHover");
        skillRangeTexture = Globals.Content.Load<Texture2D>("CellPotential");
        defaultTexture = Globals.Content.Load<Texture2D>("CellDefault");
        SkillRangeHoverTexture = Globals.Content.Load<Texture2D>("CellSelected");
        enemyAttackTexture = Globals.Content.Load<Texture2D>("CellEnemy");
        this.startPosition = startPosition;
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        cells = new GridCell[width, height];

        CreateGrid();
       
        
    }

    public Vector2 GetCellPos(int x, int y)
    {
        return cells[x, y].Position;
    }

    private void CreateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 cellPos = startPosition + new Vector2(x * cellSize, -y * cellSize);
                cells[x, y] = new GridCell(cellPos, cellSize, (x, y), defaultTexture, defaultHoverTexture);
            }
        }
    }

    public (int, int) SetBaseHeroPosition(Rectangle heroRectangle)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (heroRectangle.Intersects(cells[x, y].BoundingBox))
                {
                    return (x, y);
                }
            }
        }
        return (0, 0);
    }

    public GridCell[,] GetCells()
    {
        return cells;
    }
    public void SetUniqueCellSkill(List<(int,int)> pos,List<Skill> skills)
    {
        foreach(var p in pos)
        {
            cells[p.Item1,p.Item2].SetCellSkill(skills);
        }
    }
    public GridCell GetCellByPosition(Vector2 position)
    {
        for (int y = 0; y < height; y++)
        {
            
                for (int x = 0; x < width; x++)
                {
                    if (cells[x, y].BoundingBox.Contains(position))
                    {
                        return cells[x, y];
                    }
                }
            
            
        }
        return null;
    }
    public void DontDraw(List<int> dontDraw)
    {
        DontDrawRowList = dontDraw;
    }
    public void Draw()
    {
        for (int y = 0; y < height; y++)
        {
            if (!DontDrawRowList.Contains(y))
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 position = cells[x, y].Position;
                    Texture2D texture = cells[x, y].GetTexture();
                    Globals.SpriteBatch.Draw(texture, position, Color.White);
                }
            }
        }
    }
    public void Update()
    {
        foreach(GridCell cell in cells)
        {
            cell.Update(InputManager.MousePositionWorld);
        }
    }
}

public class GridCell
{
    public bool unique { get; set; }
    private List<Skill> givenSkill;
    public (int x, int y) Coordinate { get; }
    public Vector2 Position { get; }
    public int Size { get; }
    public Rectangle BoundingBox { get; }
    public ICharacter OccupationCharacter { get; set; }
    private Texture2D currentTexture;
    private Texture2D defaultTexture;
    private Texture2D hoverTexture;

    public GridCell(Vector2 position, int size, (int x, int y) coordinate, Texture2D defaultTexture,Texture2D hoverTexture)
    {
        unique = false;
        this.hoverTexture=hoverTexture;
        Coordinate = coordinate;
        Position = position;
        Size = size;
        BoundingBox = new Rectangle(position.ToPoint(), new Point(size, size));
        this.defaultTexture = defaultTexture; // Инициализация текстуры по умолчанию
        currentTexture = defaultTexture;
    }
    public void SetCellSkill(List<Skill> skills)
    {
        unique = true;
        givenSkill = skills;

    }
    public List<Skill> GetCellSkill()
    {
        return givenSkill;
    }
    public void SetOccupationCharacter(ICharacter occupationCharacter)
    {
        OccupationCharacter = occupationCharacter;
    }

    public void SetTexture(Texture2D newTexture)
    {
        defaultTexture = newTexture;
    }
    public void SetHoverTexture(Texture2D newTexture)
    {
        hoverTexture = newTexture;
    }

    public Texture2D GetTexture()
    {
        return currentTexture;
    }
    public void ResetTexture()
    {
        currentTexture = defaultTexture;
    }

    public void Update(Vector2 mousePosition)
    {
        if (BoundingBox.Contains(mousePosition))
        {
            
                currentTexture = hoverTexture;
            
        }
        else
        {
            
                currentTexture = defaultTexture;
            
        }
    }
}
