using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRoomTrigger : MonoBehaviour
{
    //public int triggerId;
    private CenterRoom centerRoom;  //中间房间的CenterRoom脚本对象
    public static PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        centerRoom = GetComponentInParent<CenterRoom>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D") {
            
            //检查玩家是否有拿到石像物品（触发器），三个都有的情况
            if(playerController.CenterRoomTrigger[0] && playerController.CenterRoomTrigger[1]
                && playerController.CenterRoomTrigger[2]) {
                centerRoom.destroyCollider2();  //房间下移到最下方
            }
            //只有第一个中间的触发器的情况
            if (playerController.CenterRoomTrigger[0]) {
                centerRoom.RoomTriggers[0] = true;
                centerRoom.destroyCollider1();  //房间下移到中间
                centerRoom.setStatueVisible(0);
            }

            if (playerController.CenterRoomTrigger[1]) {
                centerRoom.RoomTriggers[1] = true;
                centerRoom.setStatueVisible(1);
            }

            if (playerController.CenterRoomTrigger[2]) {
                centerRoom.RoomTriggers[2] = true;
                centerRoom.setStatueVisible(2);
            }
        }
    }
}
