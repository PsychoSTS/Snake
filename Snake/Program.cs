using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Snake
{

    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            
            Snake snake = new Snake(5, 1);
            
            while (!quit)
            {
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

                snake.Move();
                Global.GameBoard.Draw();
                Thread.Sleep(1000 / 4);
            }
            
            Global.GameBoard.Close();
            Console.WriteLine("Thank you for playing!");
        }
    }
}