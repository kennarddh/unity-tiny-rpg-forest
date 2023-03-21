using System;

using Enemy;

using Utils;

namespace Game.Achivement
{
    [Serializable]
    public class Achivements
    {
        public ArrayByEnum<EnemyType, EventVar<int>> Kills = new();

        public Achivements()
        {
            foreach (EnemyType enemyType in Enum.GetValues(typeof(EnemyType)))
            {
                Kills[enemyType] = new(0);
            }
        }
    }
}