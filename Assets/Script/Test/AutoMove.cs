using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public int speed;
    // Start is called before the first frame update
    private Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 left = new Vector3(-1,0);
        //Vector3 le = left + transform.position;
        //Vector3 left = new Vector2(-speed, 0);
        //rigidbody2D.MovePosition(transform.position + left*Time.deltaTime);
        //transform.position = Vector2.MoveTowards(transform.position, le, speed * Time.deltaTime);
    }

    private void FixedUpdate() {
        Vector3 left = new Vector2(-speed, 0);
        rigidbody2D.MovePosition(transform.position + left * Time.deltaTime);
    }
}
