using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    [SerializeField]
    public float _setDuration = 2.0f;

    [SerializeField]
    GameObject explosionObj;

    protected override void Start()
    {
        base.Start();
        SetDuration(_setDuration);
        Invoke("explosion", _setDuration - 0.4f);
    }

    void explosion()
    {
        explosionObj.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
