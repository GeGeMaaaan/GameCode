using Gamee._Manager;
using Gamee._Models;
using Gamee.Manager;
using System.Collections.Generic;

public class AnimationComponent : Component
{
    private readonly AnimationManager _anims;
    private MovementHeroManager _movementHeroManager;
    public AnimationComponent(Sprite owner, Dictionary<object, Animation> animations,MovementHeroManager movementHeroManager) : base(owner)
    {
         _movementHeroManager = movementHeroManager;
        _anims = new AnimationManager();
        foreach (var animation in animations)
        {
            _anims.AddAnimation(animation.Key, animation.Value);
        }
    }

    public override void Update()
    {
        // Обновляем анимацию
        _anims.Update(_movementHeroManager.Direction);
    }

    public override void Draw()
    {
        // Отрисовываем анимацию в текущей позиции владельца компонента
        _anims.Draw(_owner.Position);
    }

}