using Gamee._Models;
using Gamee.Manager;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Manager
{
    public class InteractionMenuManager : Menu
    {
        private Sprite _owner;
        private List<Interaction> _interactions;
        private Texture2D menuTexture;
        public InteractionMenuManager(Sprite owner, List<Interaction> actions, Texture2D texture)
        {
            _owner = owner;
            _interactions = actions;
            menuTexture = texture;
        }
        public void Update()
        {
            if (InputManager.wasClickForInteraction && isDisplay)
            {
                HandleMenuClick(_owner,InputManager.MousePositionWorld,_interactions);
            }
        }
        public void Draw()
        {
            if (_isVisible)
            {
                DisplayInteractionMenu(_owner, _interactions,menuTexture);
            }
            else isDisplay = false;
        }
         
    }
}
