namespace Gamee
{
    public static class Globals
    {

        public const int floarLevel = 880;
        public static float TotalSeconds { get; set;}// К сожалению не хватило времени убрать синглтон
        public static ContentManager Content { get; set;}

        public static SpriteBatch SpriteBatch { get; set;}
        
        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
