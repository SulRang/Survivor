using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Monster : MonoBehaviour
{
    [SerializeField]
    public GameObject EXP;

    [SerializeField]
    public MonsterData monsterData;

    [SerializeField]
    Transform playerPos;

    IObjectPool<Monster> objectPool;

    int curHp;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        curHp = monsterData.hp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPos.position, monsterData.moveSpeed*Time.deltaTime);
    }

    public void ReadyDestroy()
    {
        Invoke("DestroyMonster", 10f);
    }

    public void GetDamage(int damage)
    {
        //Debug.Log(curHp);
        curHp -= damage;
        if (curHp <= 0)
        {
            ExpDrop();
            DestroyMonster();
        }
    }

    // 오브젝트 풀을 전달받음
    public void SetManagedPool(IObjectPool<Monster> _objectPool)
    {
        objectPool = _objectPool;
    }

    //오브젝트 비활성화
    public void DestroyMonster()
    {
        objectPool.Release(this);
    }

    public void ExpDrop()
    {
        Instantiate(EXP, gameObject.transform.position, gameObject.transform.rotation);
    }

    public void RePositinon(int _index)
    {
        switch (_index)
        {
            //위
            case 0:
                this.gameObject.transform.position = new Vector2(transform.position.x, playerPos.position.y - 7f);
                break;

            //아래
            case 1:
                this.gameObject.transform.position = new Vector2(transform.position.x, playerPos.position.y + 7f);
                break;
            //오른쪽
            case 2:
                this.gameObject.transform.position = new Vector2(playerPos.position.x + 12f, transform.position.y);
                break;

            //왼쪽
            case 3:
                this.gameObject.transform.position = new Vector2(playerPos.position.x - 12f, transform.position.y);
                break;

            default:
                break;
        }
    }

}
