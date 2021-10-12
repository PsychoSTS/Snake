using System.Collections.Generic;

namespace Snake
{
    public class Snake : IGameEntity
    {
        // private LinkedList<SnakeBodyPart>BodyParts = new();
        public Direction direction = Direction.Right;
        private Position headPosition;
        private List<Position> BodyParts = new();

        public Snake(int x, int y)
        {
            Global.GameBoard.RegisterGameEntity(this);
            headPosition = new Position {X = x, Y = y};
            
            AddBodyPart(x - 1, y);
            AddBodyPart(x - 2, y);
        }

        public void AddBodyPart(int x, int y)
        {
            BodyParts.Add(
                new Position()
                {
                    X = x,
                    Y = y
                }
            );
        }

        public void ChangeDirection(Direction newDirection)
        {
            if (newDirection == Direction.Right)
            {
                if (BodyParts[0].Y == headPosition.Y && BodyParts[0].X > headPosition.X) return;
                direction = newDirection;
            }
            if (newDirection == Direction.Left)
            {
                if (BodyParts[0].Y == headPosition.Y && BodyParts[0].X < headPosition.X) return;
                direction = newDirection;
            } 
            else if (newDirection == Direction.Up)
            {
                if (BodyParts[0].X == headPosition.X && BodyParts[0].Y < headPosition.Y) return;
                direction = newDirection;
            }
            else if (newDirection == Direction.Down)
            {
                if (BodyParts[0].X == headPosition.X && BodyParts[0].Y > headPosition.Y) return;
                direction = newDirection;
            }
        }
        
        public void Move()
        {
            var last = BodyParts[^1];
            BodyParts.Remove(last);
            last.X = headPosition.X;
            last.Y = headPosition.Y;
            BodyParts.Insert(0, last);
            
            if (direction == Direction.Right) headPosition.X += 1;
            else if (direction == Direction.Left) headPosition.X -= 1;
            else if (direction == Direction.Up) headPosition.Y -= 1;
            else if (direction == Direction.Down) headPosition.Y += 1;

            if (headPosition.X > Global.GameBoard.Width - 1) headPosition.X = 0;
            else if (headPosition.X < 0) headPosition.X = Global.GameBoard.Width - 1;
            
            if (headPosition.Y > Global.GameBoard.Height - 1) headPosition.Y = 0;
            else if (headPosition.Y < 0) headPosition.Y = Global.GameBoard.Height - 1;
        }

        public void Draw(ScreenBuffer screenBuffer)
        {
            screenBuffer.Draw("X", headPosition.X, headPosition.Y);

            foreach (var bodyPart in BodyParts)
            {
                screenBuffer.Draw("#", bodyPart.X, bodyPart.Y);
            }
        }
    }
}