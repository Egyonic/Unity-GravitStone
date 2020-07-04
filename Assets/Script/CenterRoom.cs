using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterRoom : MonoBehaviour
{
    public Transform collider1; //第一个和第二个用来挡住的碰撞体
    public Transform collider2;
   
    public Vector3 move;
    public bool[] RoomTriggers;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        //move = new Vector3(0, -1, 0);
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

    public void destroyCollider1() {
        if(collider1 != null)
            Destroy(collider1.gameObject); //消除第一个碰撞体
    }

    public void destroyCollider2() {
        if (collider2 != null)
            Destroy(collider2.gameObject); //消除第二个
    }
}
