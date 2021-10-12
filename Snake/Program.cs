using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Snake
{
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public struct Position
    {
        public int X;
        public int Y;
    }
    
    
    public struct SnakeBodyPart
    {
        public char BodyPart;
        public int X;
        public int Y;
    }
    
    public class Snake
    {
        // private LinkedList<SnakeBodyPart>BodyParts = new();
        public Direction direction = Direction.Right;
        private Position headPosition;
        private List<Position> BodyParts = new();
        private ScreenBuffer _screenBuffer;

        public Snake(int x, int y, ScreenBuffer screenBuffer)
        {
            headPosition = new Position {X = x, Y = y};
            _screenBuffer = screenBuffer;
            
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

            if (headPosition.X > _screenBuffer.roomWidth - 1) headPosition.X = 0;
            else if (headPosition.X < 0) headPosition.X = _screenBuffer.roomWidth - 1;
            
            if (headPosition.Y > _screenBuffer.roomHeight - 1) headPosition.Y = 0;
            else if (headPosition.Y < 0) headPosition.Y = _screenBuffer.roomHeight - 1;
        }

        public void Draw(ScreenBuffer buffer)
        {
            buffer.Draw("X", headPosition.X, headPosition.Y);

            foreach (var bodyPart in BodyParts)
            {
                buffer.Draw("#", bodyPart.X, bodyPart.Y);
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            
            ScreenBuffer screenBuffer = new ScreenBuffer();
            Snake snake = new Snake(5, 1, screenBuffer);
            
            while (!quit)
            {
                snake.Move();
                snake.Draw(screenBuffer);
                screenBuffer.DrawScreen();
            
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo inputKey = Console.ReadKey();
                    if (inputKey.Key == ConsoleKey.RightArrow)
                    {
                        snake.ChangeDirection(Direction.Right);
                    }
                    else if (inputKey.Key == ConsoleKey.DownArrow)
                    {
                        snake.ChangeDirection(Direction.Down);
                    }
                    else if (inputKey.Key == ConsoleKey.UpArrow)
                    {
                        snake.ChangeDirection(Direction.Up);
                    }
                    else if (inputKey.Key == ConsoleKey.LeftArrow)
                    {
                        snake.ChangeDirection(Direction.Left);
                    }
                    else if (inputKey.Key == ConsoleKey.X)
                    {
                        quit = true;
                    }
                }
                
                Thread.Sleep(1000 / 4);
            }
            
            screenBuffer.CloseAlternateBuffer();
            screenBuffer.ShowCursor();
            Console.WriteLine("Thank you for playing!");
            
        }
    }
}