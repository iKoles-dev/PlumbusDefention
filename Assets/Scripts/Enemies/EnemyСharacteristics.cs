using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "EnemyСharacteristics", menuName = "Create EnemyСharacteristics", order = 51)]
    public class EnemyСharacteristics : ScriptableObject
    {
        public Sprite EnemyImage;
        public string Name;
        public int Health;
        public float AttackSpeed;
        public float MoveSpeed;
        public AnimatorController EnemyAnimator;
    }
}
