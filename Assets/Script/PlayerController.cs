using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;  //移动速度
    public float jumpSpeed; //跳跃速度
    public float doulbJumpSpeed;    //二段跳速度
    public float restoreTime;
    public float floatSpeed;    //漂浮速度
    public float floatingGravity;  //飘浮时的重力

    public int itemAmount;  //道具总数
    public Item[] items; //人物的道具，在Inspector的人物的该脚本组件中编辑

    private Rigidbody2D myRigidbody;
    private Animator myAnim;    //人物动画
    private BoxCollider2D myFeet;   //人物脚部的触发器
    private bool isGround;  //是否接触地面
    private bool canDoubleJump; //二段跳的判断
    private bool isOneWayPlatform;
    private int currentItemId;    //当前使用的引力石道具的ID

    //是否进入了使用重力石改变其他物体中立，移动方向的状态
    private bool isInStoneChangeStatus; 

    private bool isFloating;    //玩家是否在悬浮
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();    //设置人物的动画控制器
        myFeet = GetComponent<BoxCollider2D>(); //脚步触发器
        playerGravity = myRigidbody.gravityScale;
        isFloating = false;

        //道具相关的设置
        ItemUI.currentItem = items[0];
        GravityAreaController.currentItem = items[0];
        TranslateTrigger.spaceStone = items[1];
        currentItemId = 0;//设置第0个道具为当前道具

    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.isGameAlive)
        {
            //CheckAirStatus();
            Flip(); //实现左右翻转
            if (!isFloating) {
                Run();  //正常移动
                Jump();
            }
            else {
                Floating(); //悬浮移动
            }

            //Attack();
            SwitchItem();
            Floating();
            UseStoneItem(); //监听道具使用
            SwitchItemStatus();//切换道具状态

            CheckFloating();    //玩家是否对自己使用了重力，处于悬浮状态
            CheckGrounded();   // 检查是否与地面接触
            
            //SwitchAnimation();
            //OneWayPlatformCheck();
        }
    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }


    // 左右移动时角色的左右翻转
    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(plyerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        //Debug.Log("moveDir = " + moveDir.ToString());
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //myAnim.SetBool("Run", plyerHasXAxisSpeed);
    }

    /*
     重力式改变区域内重力移动物体的功能放到了GravityAreaController中来实现，
     人物控制器这边只需要实时的在SwitchItem方法中传递当前使用的物品过去就行
         */

    //使用引力石道具，改变道具是否使用的状态
    void UseStoneItem() {
        if (Input.GetButtonDown("UseStone")) {
            Item item = items[currentItemId];   //获取当前的物品对象
            item.isUsing = !item.isUsing;   //切换物品的使用状态
            
        }
    }


    //切换道具
    void SwitchItem() {
        if (Input.GetButtonDown("SwitchStone")) {
            // 先关闭原来道具的效果，在切换
            items[currentItemId].isUsing = false;
            int newId = (currentItemId + 1) % itemAmount;
            currentItemId = newId;

            ItemUI.currentItem = items[newId];//UI跟新
            // 跟新重力区域控制器的物品信息
            GravityAreaController.currentItem = items[newId];
            // 传送装置保存的物品信息
            //TranslateTrigger.spaceStone = items[newId];
        }

    }

    // 改变道具的使用对象，true为对自己，false为对环境
    void SwitchItemStatus() {
        if (Input.GetButtonDown("SwitchItemStatus")) {
            items[currentItemId].status = !items[currentItemId].status;
            //看状态跟新时UI那边的的item是指向同一个对象的，所以不需要重新赋值
            //ItemUI.currentItem = items[newId];//UI跟新
        }

    }

    //检查玩家是否对自己使用了重力，来改变是否处于悬浮状态
    void CheckFloating() {
        if (items[currentItemId].name == "重力石" && items[currentItemId].isUsing
            && items[currentItemId].status) {
            isFloating = true;
            myRigidbody.gravityScale = floatingGravity; //设置低重力
        }
        else {
            isFloating = false;
            myRigidbody.gravityScale = playerGravity;   //重置重力
        }
            
        //Debug.Log("漂浮状态: "+isFloating);
    }

    //人物使用重力石后在空中的漂浮移动，上下移动
    void Floating() {
        // 玩家悬浮状态下的移动控制
        if (isFloating) {
            
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            //Vector2 move = new Vector2(moveX, moveY);
            Vector2 playerVel = new Vector2(moveX * floatSpeed, moveY * floatSpeed);
            myRigidbody.velocity = playerVel;

        }
       
    }

    void Jump()
    {
        //Debug.Log(isGround);
        if (Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {
                //myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump)
                {
                    //myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }


    //void Attack()
    //{
    //    if(Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    //实现动画之间的切换
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if(myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if(isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatformCheck()
    {
        if(isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }        

        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        if(!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        //isClimbing = myAnim.GetBool("Climbing");
        //Debug.Log("isJumping:" + isJumping);
        //Debug.Log("isFalling:" + isFalling);
        //Debug.Log("isDoubleJumping:" + isDoubleJumping);
        //Debug.Log("isDoubleFalling:" + isDoubleFalling);
        //Debug.Log("isClimbing:" + isClimbing);
    }
}
