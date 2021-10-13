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
            Food food = new Food(10, 10);
            
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

                Global.GameBoard.Update();
                Global.GameBoard.Draw();
                
                // Wait a moment to delay the application loop
                // TODO: This is flawed.... What if the game loop takes long???
                Thread.Sleep(1000 / 4);
            }
            
            Global.GameBoard.Close();
            Console.WriteLine("Thank you for playing!");
        }
    }
}