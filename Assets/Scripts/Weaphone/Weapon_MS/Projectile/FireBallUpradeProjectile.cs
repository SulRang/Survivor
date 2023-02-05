using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallUpradeProjectile : Projectile
{
    Rigidbody2D rigidBody;
    [SerializeField]
    GameObject explosion;
    // Start is called before the first frame update
    protected override void Start()
    {
        //����ü Ÿ�� �������� �̵�
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.AddForce((Vector2.down + Vector2.left).normalized * 1000);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            if(rigidBody.velocity.y < 2.0f)
            {
                collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * power, ForceMode2D.Impulse);
                collision.GetComponent<Monster>().GetDamage(damage);
                GameObject explosionObj = Instantiate(explosion, collision.transform);
                explosionObj.transform.parent = null;
                Destroy(gameObject, 0.1f);
            }

        }
    }
}