//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChaseControll : MonoBehaviour
//{
//    private Transform flying_enemy;
//    //public Enemy_Flying[] enemyArray;

//    void Start()
//    {
//        flying_enemy = GameObject.FindGameObjectWithTag("FlyingEnemy").transform;

//        //for (int i = 0; i < numEenemies; i++)
//        //{

//        //}
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            flying_enemy.chase = true;
//            //foreach (FlyingEnemyScript enemy)
//            //{
                
//            //}
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            flying_enemy.chase = false;
//            //foreach (FlyingEnemy enemy in enemyArray)
//            //{
                
//            //}
//        }
//    }
//}
