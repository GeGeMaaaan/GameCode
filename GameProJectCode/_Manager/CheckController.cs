using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gamee._Manager
{
    public class CheckController
    {
        private int lastDiceRoll1;
        private int lastDiceRoll2;
        private MainStats lastStatCheck;
        private int lastNeedCheck;
        public Dictionary<MainStats, int> StatsDictionary;
        private SpriteFont font;
        private GameManager gameManager;
        private Random random;

        public CheckController(StatsComponent heroStats, GameManager gameManager)
        {
            StatsDictionary = heroStats.GetCurrentStats();
            this.gameManager = gameManager;
            random = new Random();
            
        }


            public bool Check(MainStats stats, int check)
            {
                int firstCube = random.Next(1, 7);
                int secondCube = random.Next(1, 7);
                lastDiceRoll1 = firstCube;
                lastDiceRoll2 = secondCube;
                lastStatCheck = stats;
                lastNeedCheck = check;

                if (firstCube == 1 && secondCube == 1)
                {
                    return false;
                }
                else if (secondCube == 6 && firstCube == 6)
                {
                    return true;
                }
                else
                {
                    return firstCube + secondCube + StatsDictionary[stats] >= check;
                }
            }

        // Первая реализация - использует поля класса
        public (MainStats,int) GetLastStatCheck()
        {
            return (lastStatCheck,StatsDictionary[lastStatCheck]);
        }
        public int GetLastDice1()
        {
            return lastDiceRoll1;
        }
        public int GetLastDice2()
        {
            return lastDiceRoll2;
        }
        public int GetLastNeedCheck()
        {
            return lastNeedCheck;
        }



    }
}
