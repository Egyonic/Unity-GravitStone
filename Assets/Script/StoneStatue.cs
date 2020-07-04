using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneStatue : MonoBehaviour
{
    public int statueNember;//设置石像的序号，用于对应玩家石像数组中的序号
    private static PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //设置玩家已经获得了石像道具
        playerController.CenterRoomTrigger[statueNember] = true;
        Destroy(gameObject);
    }
}
