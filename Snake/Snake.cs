using System.Collections.Generic;

namespace Snake
{
    public class Snake : IGameEntity
    {
        // private LinkedList<SnakeBodyPart>BodyParts = new();
        public Direction Direction = Direction.Right;
        public Position HeadPosition;
        private List<Position> _bodyParts = new();

        public Snake(int x, int y)
        {
            Global.GameBoard.RegisterGameEntity(this);
            HeadPosition = new Position {X = x, Y = y};
            
            AddBodyPart(x - 1, y);
            AddBodyPart(x - 2, y);
        }

        public void AddBodyPart(int x, int y)
        {
            _bodyParts.Add(
                new Position()
                {
                    X = x,
                    Y = y
                }
            );
        }

        public bool AttemptEat(Food food)
        {
            if (food.Position == HeadPosition)
            {
                AddBodyPart(food.Position.X, food.Position.Y);
                return true;
            }

            return false;
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (newDirection == Direction.Right)
            {
                if (_bodyParts[0].Y == HeadPosition.Y && _bodyParts[0].X > HeadPosition.X) return;
                Direction = newDirection;
            }
            if (newDirection == Direction.Left)
            {
                if (_bodyParts[0].Y == HeadPosition.Y && _bodyParts[0].X < HeadPosition.X) return;
                Direction = newDirection;
            } 
            else if (newDirection == Direction.Up)
            {
                if (_bodyParts[0].X == HeadPosition.X && _bodyParts[0].Y < HeadPosition.Y) return;
                Direction = newDirection;
            }
            else if (newDirection == Direction.Down)
            {
                if (_bodyParts[0].X == HeadPosition.X && _bodyParts[0].Y > HeadPosition.Y) return;
                Direction = newDirection;
            }
        }
        
        public void Move()
        {
            var last = _bodyParts[^1];
            _bodyParts.Remove(last);
            last.X = HeadPosition.X;
            last.Y = HeadPosition.Y;
            _bodyParts.Insert(0, last);
            
            if (Direction == Direction.Right) HeadPosition.X += 1;
            else if (Direction == Direction.Left) HeadPosition.X -= 1;
            else if (Direction == Direction.Up) HeadPosition.Y -= 1;
            else if (Direction == Direction.Down) HeadPosition.Y += 1;

            if (HeadPosition.X > Global.GameBoard.Width - 1) HeadPosition.X = 0;
            else if (HeadPosition.X < 0) HeadPosition.X = Global.GameBoard.Width - 1;
            
            if (HeadPosition.Y > Global.GameBoard.Height - 1) HeadPosition.Y = 0;
            else if (HeadPosition.Y < 0) HeadPosition.Y = Global.GameBoard.Height - 1;
        }

        public void Draw(ScreenBuffer screenBuffer)
        {
            screenBuffer.Draw("X", HeadPosition.X, HeadPosition.Y);

            foreach (var bodyPart in _bodyParts)
            {
                screenBuffer.Draw("#", bodyPart.X, bodyPart.Y);
            }
        }

        public void Update()
        {
            Move();
        }
    }
}