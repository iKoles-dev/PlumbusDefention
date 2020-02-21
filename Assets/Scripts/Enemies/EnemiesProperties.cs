using System;
using Assets.Scripts;

namespace Assets
{
    [Serializable]
    public class EnemiesProperties
    {
        public Enemy CurrentEnemy;
        public int PopUpIndex = 0;
        public float TimeBetweenSpawns = 0;
        public float TimeToNextEnemies = 0;
        public int EnemiesAmount = 0;

        public EnemiesProperties(Enemy enemy)
        {
            CurrentEnemy = enemy;
        }
    }
}