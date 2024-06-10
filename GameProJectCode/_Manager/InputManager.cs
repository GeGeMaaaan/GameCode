using Gamee._Manager;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gamee.Manager
{
    public static class InputManager
    {
        private static Vector2 _mousePositionWorld;
        private static bool _wasMouseClick;
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;
        private static MouseState mouseState;
        private static bool ab;
        public static bool wasClickForInteraction;
        public static void Update(Vector2 targetPosition, Camera camera)
        {
            wasClickForInteraction = false;
            _mousePositionWorld = MousePositionInWorld(GetMousePosition(), camera);
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed) { _wasMouseClick= true; ab = true; }
            else _wasMouseClick= false;
            if(ab&&mouseState.LeftButton == ButtonState.Released)
            {
                wasClickForInteraction = true;
                ab = false;
            }
        }

        public static Vector2 GetMousePosition()
        {
            MouseState mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

         static Vector2 MousePositionInWorld(Vector2 mousePosition, Camera camera)
        {
            Matrix inverseCameraTransform = Matrix.Invert(camera.Transform);
            return Vector2.Transform(mousePosition, inverseCameraTransform);
        }
        public static bool WasKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }
        public static Rectangle MouseRectangle => new Rectangle((int)_mousePositionWorld.X, (int)_mousePositionWorld.Y, 1, 1);
        public static Vector2 MousePositionForUI =>new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        public static Vector2 MousePositionWorld => _mousePositionWorld;
        public static bool WasClick => _wasMouseClick;
        public static KeyboardState KeyboardState => _currentKeyboardState;
        public static MouseState MouseState => mouseState;
    }
}