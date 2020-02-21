using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDestructor : MonoBehaviour
{
    [SerializeField] private Sprite _damaged;
    [SerializeField] private Sprite _fullyDamaged;
    private SpriteRenderer _castle;
    private bool _isDamaged = false;

    private void Start()
    {
        _castle = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetDamagedSprite()
    {
        if (!_isDamaged)
        {
            _castle.sprite = _damaged;
            _isDamaged = true;
        }
    }

    public void SetDestroyedSprite()
    {
        _castle.sprite = _fullyDamaged;
    }
}
