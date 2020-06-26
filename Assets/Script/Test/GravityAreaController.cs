using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 用于控制一个区域内的模拟改变重力和移动的效果
     */

public class GravityAreaController : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public Vector2 direction;   //控制区域内的移动方向
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getSpeedX() {
        return speedX * direction.x;
    }

    public float getSpeedY() {
        return speedY * direction.y;
    }
}
