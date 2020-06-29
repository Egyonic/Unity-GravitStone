using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool isSearching;    //是否处于锁定玩家的状态
    public bool isAttacking;    //攻击状态
    public bool isRest; //休息状态

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SearchPlayerRoom() {
        //通过BossRoomController来返回玩家所在的房间

    }

}
