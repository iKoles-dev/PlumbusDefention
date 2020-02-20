using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Create Enemy", order = 51)]
    public class Enemy : ScriptableObject
    {
        public Sprite EnemyImage;
        public string Name;
        public int Health;
        public int AttackStrength;
        public float AttackSpeed;
        public float MoveSpeed;
        public AnimatorController EnemyAnimator;
    }
}
