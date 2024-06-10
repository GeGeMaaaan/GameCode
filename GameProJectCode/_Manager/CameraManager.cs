using Gamee._Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gamee._Manager
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public float Zoom { get; set; } = 1f; 
        private float _minX;
        private float _maxX; 
        private float _minY; 
        private float _maxY; 
        private float _verticalOffset; 
        private int _previousScrollValue;
        private Vector2 _position;
        private bool follow= true;
        public Vector2 Position;
        private float cameraSpeed = 7.5f;
        public Camera(float minX, float maxX, float minY, float maxY, float verticalOffset, int BasePosX = 0, int BasePosY = 0)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
            _verticalOffset = verticalOffset;
            _position = new Vector2(BasePosX, BasePosY);
        }

        public void Follow()
        {
            _position.X = MathHelper.Clamp(_position.X, _minX, _maxX);
            Position = _position;
            _position.Y = MathHelper.Clamp(_position.Y, _minY, _maxY);

            var zoomMatrix = Matrix.CreateScale(Zoom, Zoom, 1);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            var position = Matrix.CreateTranslation(-_position.X, -_position.Y, 0);

            Transform = position * zoomMatrix * offset;
        }

        public void HandleInput(Vector2 target,int targetWidht)
        {
            if (follow)
            {
                _position.X = target.X + targetWidht / 2;
                _position.Y = target.Y+_verticalOffset;
                
            }
        }
        public void SetVerticalOffset(float verticalOffset)
        {
            _verticalOffset = verticalOffset;
        }
    }
}
