namespace Snake
{
    public class Food : IGameEntity
    {
        public Position Position;

        public Food(int x, int y)
        {
            Position = new Position
            {
                X = x,
                Y = y
            };
            
            Global.GameBoard.RegisterGameEntity(this);
        }
        
        public void Draw(ScreenBuffer screenBuffer)
        {
            screenBuffer.Draw("0", Position.X, Position.Y);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}