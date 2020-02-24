using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Create Wave", order = 52)]
    [Serializable]
    public class Wave : ScriptableObject
    {
        public float TimeToNextWave;
        public List<EnemiesProperties> EnemiesInWave = new List<EnemiesProperties>();
    }
}