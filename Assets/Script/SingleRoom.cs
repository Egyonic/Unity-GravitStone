using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SingleRoom : MonoBehaviour
{
    public int row; //房间的行和列的值，用来初始化房间控制器中的数组
    public int column;
    public float speed;

    public Transform myTranform;
    //房间的发器，判断玩家是否在该房间内
    private BoxCollider2D myTrigger;
    // Start is called before the first frame update
    void Start()
    {
        myTranform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //玩家进入房间时，去
    private void OnTriggerEnter2D(Collider2D collision) {
        
    }

    public void MoveTool(Vector3 dest) {
        Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
    }
}
