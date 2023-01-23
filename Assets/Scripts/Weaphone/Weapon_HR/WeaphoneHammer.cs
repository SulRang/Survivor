using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaphoneHammer : Weaphone
{
    public static Vector3 Direction;
    public Transform player;

    GameObject MaceObject;
    bool isTurn = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //����ü ����
        SetSpeed(200.0f);
        //���� ��ٿ�
        SetCoolDown(2.0f);
    }

    public override void Attack()
    {
        if (projectile == null)
            return;

        float theta = Mathf.Acos(Direction.x);
        float degree = Direction.y > 0 ? Mathf.Rad2Deg * theta : Mathf.Rad2Deg * theta * (-1);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree));

        //Debug.Log(Direction);
        transform.position = new Vector3(transform.position.x + 1, transform.position.y + 1);

        //����ü ����
        MaceObject = Instantiate(projectile, transform);
        //����ü �θ� ������Ʈ ����
        MaceObject.transform.parent = null;
        MaceObject.SetActive(true);
        //����ü ���ӽð� ����
        MaceObject.GetComponent<Projectile>().SetDuration(0.5f);
        //����ü ���� �� �̵� ����
        //MaceObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, 1) * 300 * (-1));
        isTurn = true;
        transform.position = player.position;
        transform.rotation = player.rotation;
    }

    private void Update()
    {
        if (isTurn && MaceObject != null)
        {
            MaceObject.transform.RotateAround(player.position, Vector3.back, 800 * Time.deltaTime);
        }
    }
}