using Gamee.Manager;
using Gamee;
using Gamee._Manager;

namespace Gamee
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameManager _gameManager;
        public static int ScreenHeight;
        public static int ScreenWidth;
        public Game1()
        {
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            


        }

        protected override void Initialize()
        {
           
            
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            Globals.Content = Content;
            Services.AddService(_graphics);
            _gameManager = new GameManager(Services);
            _gameManager.Init();
            // TODO: Add your initialization logic here




            //floarPositon = new Vector2(0, _graphics.PreferredBackBufferHeight - 100);
            //playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, floarPositon.Y - 38 * playerWidth);
            //playerTargetPosition = playerPosition;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;
            //player = Content.Load<Texture2D>("Player");
            //floar = Content.Load<Texture2D>("floar");
            //background = Content.Load<Texture2D>("background");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.Update(gameTime);
            _gameManager.Update();

            base.Update(gameTime);



            //mState = Mouse.GetState();
            //if(mState.LeftButton== ButtonState.Pressed && mRelesed)
            //{
            //    mRelesed = false;
            //    playerTargetPosition = new Vector2(mState.X-25* playerWidth, playerPosition.Y);
            //}
            //mRelesed = true;
            //if (Math.Abs(playerPosition.X - playerTargetPosition.X)> speed)
            //    playerPosition = new Vector2(playerPosition.X - ((playerPosition.X - playerTargetPosition.X)/Math.Abs(playerPosition.X - playerTargetPosition.X))*speed, playerPosition.Y);
            //base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           
            //_spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(floar, floarPositon, Color.White);
            _gameManager.Draw();
            

            base.Draw(gameTime);
        }
    }
}
//public class Game1 : Game
//{
//    private readonly GraphicsDeviceManager _graphics;
//    private SpriteBatch _spriteBatch;
//    private GameManager _gameManager;

//    public Game1()
//    {
//        _graphics = new GraphicsDeviceManager(this);
//        Content.RootDirectory = "Content";
//        IsMouseVisible = true;
//    }

//    protected override void Initialize()
//    {
//        _graphics.PreferredBackBufferWidth = 1024;
//        _graphics.PreferredBackBufferHeight = 768;
//        _graphics.ApplyChanges();

//        Globals.Content = Content;

//        _gameManager = new();
//        _gameManager.Init();

//        base.Initialize();
//    }

//    protected override void LoadContent()
//    {
//        _spriteBatch = new SpriteBatch(GraphicsDevice);
//        Globals.SpriteBatch = _spriteBatch;
//    }

//    protected override void Update(GameTime gameTime)
//    {
//        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
//            Exit();

//        Globals.Update(gameTime);
//        _gameManager.Update();

//        base.Update(gameTime);
//    }

//    protected override void Draw(GameTime gameTime)
//    {
//        GraphicsDevice.Clear(Color.Beige);

//        _spriteBatch.Begin();
//        _gameManager.Draw();
//        _spriteBatch.End();

//        base.Draw(gameTime);
//    }
//}
