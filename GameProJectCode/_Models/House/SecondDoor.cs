using Gamee.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models.House
{
    public class SecondDoor : StandartActiveSprite
    {
        public SecondDoor(Vector2 position, string textureName, GameServiceContainer gameServiceContainer) : base(position, textureName, gameServiceContainer)
        {
            _activeObjectComponent.AddInteraction("Открыть", FirstAttempToOpen);
            _activeObjectComponent.AddInteraction("Осмотреть", Investigate);
            eventManager.Subscribe("OpenDoorInHouse_dexterity",UnlockTheDoor);
            eventManager.Subscribe("OpenDoorInHouse_strength", UnlockTheDoor);
            
        }
        private void UnlockTheDoor()
        {
            _activeObjectComponent.RemoveInteraction("Попытаться открыть", TryOpen);
            _activeObjectComponent.AddInteraction("Войти", OpenDoor);
        }
        private void OpenDoor()
        {
            eventManager.Invoke("OpenDoor");
        }
        private void FirstAttempToOpen()
        {
             _subtitleManager.AddSubtitle("Тоже закрыто. Но замок на двери выглядит достаточно хлиплым. Возможнжо я бы мог его открыть");
            _activeObjectComponent.RemoveInteraction("Открыть", FirstAttempToOpen);
            _activeObjectComponent.AddInteraction("Попытаться открыть", TryOpen);

        }
        private void Investigate()
        {
            _subtitleManager.AddSubtitle("Я всегда задовался вопросом зачем две двери в дом, но я так и не получил ответа");
        }
        private void TryOpen()
        {
            gameObjectManager.StartDialog("0");
            
        }
    }
}
