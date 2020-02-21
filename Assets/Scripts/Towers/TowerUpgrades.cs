using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Towers
{
    [Serializable]
    public class TowerUpgrades
    {
        public int Cost;
        public float Range;
        public float ShootInterval;
        public float Damage;
        public int SellCost;
    }
}
