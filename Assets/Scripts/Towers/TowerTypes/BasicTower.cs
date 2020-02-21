using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public abstract class BasicTower : MonoBehaviour
    {
        public virtual void Shoot(EnemyController enemy, float damage, float radius) {}
    }
}