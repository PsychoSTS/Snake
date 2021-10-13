using System;

namespace Snake
{
    public class FoodSpawner: IGameSystem
    {
        public Food ActiveFood;

        public FoodSpawner()
        {
            Global.GameBoard.RegisterGameSystem(this);
        }
        
        public void Update()
        {
            if (ActiveFood == null)
            {
                Spawn();
            } 
        }

        public void Spawn()
        {
            GameBoard gameBoard = Global.GameBoard;
            
            Random rand = new Random();
            int randX = rand.Next(0, gameBoard.Width - 1);
            int randY = rand.Next(0, gameBoard.Height - 1);
            ActiveFood = new Food(randX, randY);
        }

        public void Consume()
        {
            GameBoard gameBoard = Global.GameBoard;
            foreach (IGameEntity gameEntity in gameBoard.Entities)
            {
                if (ReferenceEquals(ActiveFood, gameEntity))
                {
                    gameBoard.Entities.Remove(gameEntity);
                    gameBoard.PlayerScore++;
                    Spawn();
                    break;
                }
            }
        }
    }
}