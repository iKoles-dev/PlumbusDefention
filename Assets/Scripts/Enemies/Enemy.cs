using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Create Enemy", order = 51)]
public class Enemy : ScriptableObject
{
    public Sprite EnemyImage;
    public string Name;
    public int Health;
    public int AttackStrength;
    public float AttackSpeed;
    public float MoveSpeed;
    public GameObject EnemyAnimator;
}