using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusStatUI : MonoBehaviour
{
    Player_Status playerStatus;
    ShowRandomItem showItem;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.Find("Player").GetComponent<Player_Status>();
        showItem = GameObject.Find("MainLevelUp").GetComponentInChildren<ShowRandomItem>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < playerStatus.accSet.Count; i++)
        {
            //Debug.Log(playerStatus.weaponSet.Count);
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[i].text = playerStatus.accSet[i];

            foreach (var item in showItem.Data)
            {
                //Debug.Log(item[1] + "," + playerStatus.weaponSet[i] + "," + item[2]);
                Debug.Log(item[1] == playerStatus.accSet[i]);
                if (playerStatus.accSet[i] == item[1])
                {
                    //Debug.Log((int.Parse(item[2]) / 5f) + "       dddddddddddddddddddddddddddddd");
                    gameObject.GetComponentsInChildren<Slider>()[i].value = int.Parse(item[2]) / 5f;
                }
            }
        }
    }
}