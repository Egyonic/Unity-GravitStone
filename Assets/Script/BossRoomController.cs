using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    public int itemSetCount;
    public SingleRoom[][] rooms = new SingleRoom[5][];

    // Start is called before the first frame update
    void Start()
    {
        itemSetCount = 0;
        //初始化所有的房间变量
        for (int i =0; i<5; i++) {
            for (int j = 0; j < 5; j++) {
                rooms[i][j] = new SingleRoom();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 外部设置房间
    public void SetRoom(int i, int j, SingleRoom room) {
        rooms[i][j] = room;
    }

    public void SetPlayerRoom() {

    }

    public void CheckItemCount() {
        if(itemSetCount == 4) {
            // 控制BOSS，使其无法活动
        }
    }

    public void MoveRoom() {
        //移动场景中的房间

        //改变房间在数组中的位置
        SingleRoom tmp =  rooms[1][1];
        rooms[1][1] = rooms[3][1];
        rooms[3][1] = rooms[3][3];
        rooms[3][3] = rooms[1][3];
        rooms[1][3] = tmp;
    }



}
