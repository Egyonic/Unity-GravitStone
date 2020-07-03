using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRoom : MonoBehaviour
{
    public Transform collider1; //第一个和第二个用来挡住的碰撞体
    public Transform collider2;
   
    private Vector3 move;
    public static bool[] RoomTriggers;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0, -1, 0);
        RoomTriggers = new bool[3];
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        rigidbody2D.MovePosition(transform.position + move * Time.deltaTime);
    }

    void destroyCollider1() {
        Destroy(collider1); //消除第一个碰撞体
    }

    void destroyCollider2() {
        Destroy(collider2); //消除第二个
    }
}
