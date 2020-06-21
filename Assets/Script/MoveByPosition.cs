using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPosition : MonoBehaviour
{

    private Transform mTransform;
    private Rigidbody2D mRigidbody;
    public Vector3 DirectionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //获取自身 Transform组件和Rigidbody组件的引用
        mTransform = gameObject.GetComponent<Transform>();
        mRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void FixedUpdate() {
        //使用系统预设的w,a,s,d 控制Cube移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, v, 0);

        //刚体移动的特点：物体的位置+方向，太快就方向*一个小数，使之慢一点
        //mRigidbody.MovePosition(mTransform.position + direction * 0.2f);
        mRigidbody.MovePosition(mTransform.position + direction * 0.2f);
    }

    private void PlayerMove() {
        

    }
}
