using Assets.Scripts.Ammunition;
using UnityEngine;

namespace Assets.Scripts.Towers
{
    public class LightningTower : BasicTower
    {
        [SerializeField] private ParticleSystem _lighning;
        public override void Shoot(EnemyController enemy, float damage, float radius)
        {
            _lighning.Play();
            Collider2D[] allEnimiesInRange = Physics2D.OverlapCircleAll(transform.position, radius);
            Debug.Log(allEnimiesInRange.Length);
            foreach (var enemyCollider in allEnimiesInRange)
            {
                if (enemyCollider.gameObject.GetComponent<EnemyController>() != null)
                {
                    enemyCollider.GetComponent<EnemyController>().ApplyDamage((int)damage);
                }
            }
        }
    }
}