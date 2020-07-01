using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster : Enemy
{
    public float speed;
    public float startWaitTime;
    private float waitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;

    private bool IsWalk;
    private bool IsIdle;

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        myAnim.SetBool("IsWalk", true); //设置动画初始状态为行走
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public void Update()
    {
        //调用父类的Update()方法
        base.Update();
        getAnimState(); //获取动画状态值
        Debug.Log("IsWalk"+myAnim.GetBool("IsWalk"));
        Debug.Log("IsIdle" + myAnim.GetBool("IsIdle"));

        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        //myAnim.SetBool("IsWalk", true); //开始播放行走动画

        // 已经移动到了指定位置
        if (Vector2.Distance(transform.position, movePos.position) < 0.01f) {
            myAnim.SetBool("IsWalk", false);
            myAnim.SetBool("IsIdle", true);
            if (waitTime <= 0) {
                // 等待时间已经已经过了，前往下个位置
                //myAnim.SetBool("IsWalk", true);
                movePos.position = GetRandomPos();
                waitTime = startWaitTime;
            }
            else {
                // 还处于等待的状态
                waitTime -= Time.deltaTime;
                
            }
        }//还在移动的过程中
        else {
            myAnim.SetBool("IsWalk", true);
            myAnim.SetBool("IsIdle", false);
        }
    }

    Vector2 GetRandomPos() {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }

    void getAnimState() {
        IsWalk = myAnim.GetBool("IsWalk");
        IsIdle = myAnim.GetBool("IsIdle");
    }
}
