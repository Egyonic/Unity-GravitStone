﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoAnim : MonoBehaviour
{
    public int health;
    public int damage;
    public int damageByRock;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start() {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    public void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {

        health -= damage;

        //GameController.camShake.Shake();
    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D") {
            if (playerHealth != null) {
                playerHealth.DamegePlayer(damage);
            }
            //播放怪物攻击动画
            SoundManager.PlayHitByEnemy();  //播放声音

        }
        //怪物撞击到石头会扣血
        else if (other.gameObject.CompareTag("RockBlock")) {
            TakeDamage(damageByRock);
        }
    }
}
