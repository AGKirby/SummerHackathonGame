using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObstacles : MonoBehaviour
{
    public GameObject obstacleJump;
    public GameObject obstacleSlide;
    private GameManagerBehavior gameManager;
    public int timer = 205;
    public float slideHeight = -1.1f;
    public float jumpHeight = -0.35f;

    private int counter = 0;
    private System.Random rnd;
    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(counter == timer)
        {
            // spawn obstacle?
            float spawn = ((float) rnd.Next(1, 101)) / 100.0f;
            float chance = 0.6f - (((float) Math.Min(gameManager.level-1, 12)) / 21.0f);
            if(spawn > chance)
            {
                //spawn obstacle
                float type = rnd.Next(1, 11);
                if(type <= 6)
                {
                    GameObject newObstacle = (GameObject) Instantiate(obstacleJump);
                    newObstacle.transform.position = new Vector3(transform.position.x, jumpHeight, 0);
                } 
                else
                {
                    GameObject newObstacle = (GameObject)Instantiate(obstacleSlide);
                    newObstacle.transform.position = new Vector3(transform.position.x, slideHeight, 0);
                }
            }
            counter = 0;
        }
        counter++;
    }
}
