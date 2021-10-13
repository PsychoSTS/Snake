using System.Collections.Generic;

namespace Snake
{
    public class GameBoard
    {
        public int PlayerScore = 0;
        
        public List<IGameEntity> Entities = new();
        public List<IGameSystem> Systems = new();

        public int Width => _screenBuffer.Width;
        public int Height => _screenBuffer.Height;

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
        
        public void RegisterGameSystem(IGameSystem system)
        {
            Systems.Add(system);
        }

        public void Update()
        {
            foreach (var gameSystem in Systems)
            {
                gameSystem.Update();
            }
            
            foreach (IGameEntity gameEntity in Entities)
            {
                gameEntity.Update();
            }
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