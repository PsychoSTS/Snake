using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var stopwatch = new Stopwatch();
            FoodSpawner foodSpawner = new FoodSpawner();
            Snake snake = new Snake(5, 1);
            
            while (!quit)
            {
                // Start timing the active frame
                stopwatch.Start();
                
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
                
                if (snake.AttemptEat(foodSpawner.ActiveFood))
                {
                    foodSpawner.Consume();
                }
                
                // Elapsed time for processing the frame
                stopwatch.Stop();
                int elapsed = (int)stopwatch.ElapsedMilliseconds;
                
                // Frame timing
                Thread.Sleep((1000 / 6) - elapsed);
            }
            
            Global.GameBoard.Close();
            Console.WriteLine($"Your score: {Global.GameBoard.PlayerScore}");
        }
    }
}