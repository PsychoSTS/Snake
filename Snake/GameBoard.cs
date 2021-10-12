using System.Collections.Generic;

namespace Snake
{
    public class GameBoard
    {
        public List<IGameEntity> Entities = new List<IGameEntity>();

        public int Width
        {
            get { return _screenBuffer.Width; }
        } 
        
        public int Height
        {
            get { return _screenBuffer.Height; }
        } 

        private ScreenBuffer _screenBuffer;
        
        
        public GameBoard()
        {
            _screenBuffer = new ScreenBuffer();
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            _screenBuffer.Fill('.');
        }

        public void RegisterGameEntity(IGameEntity entity)
        {
            Entities.Add(entity);
        }

        public void Draw()
        {
            foreach (IGameEntity gameEntity in Entities)
            {
                gameEntity.Draw(_screenBuffer);
            }
            
            _screenBuffer.DrawScreen();
        }

        public void Close()
        {
            _screenBuffer.CloseAlternateBuffer();
            _screenBuffer.ShowCursor();
        }
    }
}