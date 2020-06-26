﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    // 两个自身的移动速度，将由管理模拟重力区域的父类来控制
    // 在值中通过正负值来控制方向
    private float speedX;  //  x速度
    private float speedY;  //  y速度

    private GravityAreaController gravityAreaController;
    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gravityAreaController = transform.GetComponentInParent<GravityAreaController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gravityAreaController.speedX);
    }

    private void FixedUpdate() {
        syncSpeedAndDireaction();
        Vector3 left = new Vector2(speedX, speedY);
        rigidbody2D.MovePosition(transform.position + left * Time.deltaTime);

    }

    private void syncSpeedAndDireaction() {
        speedX = gravityAreaController.getSpeedX();
        speedY = gravityAreaController.getSpeedY();
    }

  
}
