using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAura : MonoBehaviour
{
    public float level = 0;

    [SerializeField]
    float damage = 1f;

    [SerializeField]
    Player_Move player;

    [SerializeField]
    GameObject laurel;

    [SerializeField]
    GameObject upgradeObj;

    [SerializeField]
    int classIdx = 1;

    bool isUpgrade = false;

    float cooltime = 0f;

    void Start()
    {
        damage *= Player_Status.instance.DMG;
        ScaleUpdate();
        this.gameObject.transform.SetParent(player.transform);
    }

    public void LevelUp()
    {
        ++level;
        ScaleUpdate();

        // 전직 or 업그레이드 가능 여부 확인
        if (level >= 5)
        {
            if (laurel.GetComponent<ACC_Laurel>().GetLevel() >= 5)
            {
                UpgradeWithACC();
            }
            else
            {
                Upgrade();
            }
        }
    }

    // Scroll 활성화 및 업그레이드, 크기 관련 장신구 활성화 및 업그레이드 시 실행해줘야함
    public void ScaleUpdate()
    {
        transform.localScale = new Vector2((level * 2f) * Player_Status.instance.SIZE, (level * 2f) * Player_Status.instance.SIZE);
    }

    // 오라 업그레이드. 조건은 월계관과 오라 모두 5레벨 이상. 범위가 더 넓은 오라로 업그레이드
    public void UpgradeWithACC()
    {
        if (laurel.GetComponent<ACC_Laurel>().GetLevel() >= 5 && level >= 5 && !Player_Status.instance.HasClass(classIdx))
        {
            // 사전에 전직이 되어있으면 해당 기능 취소.
            if (isUpgrade)
            {
                StopCoroutine(UpgradeAttackCoroutine());
                isUpgrade = false;
            } 
            upgradeObj.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    // 오라 전직. 조건은 오라 5레벨 이상. 범위가 더 넓은 오라 일정 시간 간격마다 추가 활성화
    public void Upgrade()
    {
        if (level >= 5 && !Player_Status.instance.HasClass(classIdx))
        {
            isUpgrade = true;
            StartCoroutine(UpgradeAttackCoroutine());
        }
    }

    // 오라 전직 시 사용 코루틴. 3초 간격으로 3초동안 오라 활성화
    IEnumerator UpgradeAttackCoroutine()
    {
        while (isUpgrade)
        {
            cooltime += Time.deltaTime;
            if (cooltime >= 3f)
            {
                cooltime -= 3f;
                upgradeObj.SetActive(true);
                yield return new WaitForSeconds(3f);
                upgradeObj.SetActive(false);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<Monster>().GetDamage((int)damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            collision.GetComponent<Monster>().GetDamage((int)damage);
        }
    }
}
