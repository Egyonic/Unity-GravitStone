using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    //public float flashTime;

    public Animator myAnim;    //动画组件

    //private SpriteRenderer sr; 
    //private Color originalColor;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        myAnim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        //originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {  
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
       
        health -= damage;
        //FlashColor(flashTime);

        //GameController.camShake.Shake();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if(playerHealth != null)
            {
                playerHealth.DamegePlayer(damage);
            }
            //播放怪物攻击动画
            myAnim.SetTrigger("Attack");    //触发攻击动画
            
        }
    }
}
