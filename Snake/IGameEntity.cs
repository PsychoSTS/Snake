namespace Snake
{
    public interface IGameEntity
    {
        public void Draw(ScreenBuffer screenBuffer);
        public void Update();
    }
}