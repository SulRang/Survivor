using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_JS : MonoBehaviour
{
    float curTime = 0.0f;
    [SerializeField]
    float damage = 1;
    [SerializeField]
    float size = 2.0f;
    [SerializeField]
    float duration = 5.0f;
    [SerializeField]
    float power = 2.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.localScale = new Vector3(size + Player_Status.instance.Duration, size + Player_Status.instance.Duration, 1);
        damage += Player_Status.instance.DMG;         //�ӽ� ������ ��ġ
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > duration)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<Monster>().GetDamage((int)damage);
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * power, ForceMode2D.Impulse);
        }
    }
    public void SetDuration(float _duration)
    {
        duration = _duration;
    }

    public void SetSize(float _size)
    {
        size += _size;
        transform.localScale = new Vector3(size + Player_Status.instance.Duration, size + Player_Status.instance.Duration, 1);
    }
}