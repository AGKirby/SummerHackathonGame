using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class character : MonoBehaviour
{
    private int counter;
    private int resetJump = 0;
    private float initialHeight;
    private float initialLocation;
    public SpriteRenderer SR;
    public int maxHeight = 1;
    public GameObject face;

    public Sprite[] running;
    private int countRunning;
    private int indexRunning;
    
    public Sprite[] jumping;
    private int countJumping;
    private int indexJumping;
    private bool isJumping = false;

    private bool isSliding = false;
    public Sprite[] sliding;
    private int countSliding;
    private int indexSliding;

    private bool isTripping = false;
    public Sprite[] tripping;
    private int countTripping;
    private int indexTripping;
    private bool tripped = false;
    private int wait = 0;
    private bool resetTripping = true;
    private bool invincible = true;

    private keepScore scoreKeeper;
    // Start is called before the first frame update
    void Start()
    {
        initialHeight = transform.position.y;
        initialLocation = transform.position.x;
        counter = 0;

        indexRunning = 0;
        indexJumping = 0;
        indexSliding = 0;
        indexTripping = 0;
        
        countRunning = running.Length;
        countJumping = jumping.Length;
        countSliding = sliding.Length;
        countTripping = tripping.Length;

        scoreKeeper = GameObject.Find("ScoreHolder").GetComponent<keepScore>();
        updateFace();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 25)
        {
            if (isJumping)
            {
                indexJumping++;
                if (indexJumping == countJumping)
                {
                    transform.position = new Vector3(transform.position.x, initialHeight);
                    resetJump = 1;
                    resumeRunning();
                } 
                SR.sprite = jumping[indexJumping];
                
            }
            else if (isSliding)
            {
                indexSliding++;
                if (indexSliding == countSliding)
                {
                    resumeRunning();
                } 
                SR.sprite = sliding[indexSliding];
            }
            else if (isTripping)
            {
                indexTripping++;
                if (indexTripping == countTripping)
                {
                    resumeRunning();
                }
                SR.sprite = tripping[indexTripping];
            }
            else
            {
                indexRunning++;
                if (indexRunning == countRunning)
                {
                    indexRunning = 0;
                }
                SR.sprite = running[indexRunning];
            }
            if (tripped && !isTripping)
            {
                if(wait >= 10)
                {
                    invincible = false;
                }
                if(wait == 80)
                {
                    tripped = false;
                    resetTripping = true;
                    wait = 0;
                }
                wait++;
            }
            counter = 0;
        }
        counter++;
        if (isJumping && resetJump == 0) //parabolic curve for the jump
        {
            float x = (indexJumping * 10 + counter  / 2) / 10;
            float l = (countJumping-1) / 2;
            float a = -maxHeight / ((float) Math.Pow(l, 2));
            float newY = a * ((float) Math.Pow((x-l),2)) + maxHeight;
            transform.position = new Vector3(transform.position.x, newY, 0);
        } 
        else if (resetJump > 0) //prevent double jump
        {
            resetJump++;
            if(resetJump == 30)
            {
                resetJump = 0;
                isJumping = false;
            }
        }
        if (isTripping) 
        {
            updateFace();
            transform.position -= new Vector3(0.015f, 0, 0);
        }
        else if (!isJumping && Input.GetKey("up"))
        {
            isJumping = true;
            isSliding = false;
            indexSliding = 0;
        }
        else if (!isJumping && Input.GetKey("down"))
        {
            isSliding = true;
            updateFace();
        }
        if (resetTripping)
        {
            transform.position += new Vector3(0.005f, 0, 0);
            if(transform.position.x >= 0)
            {
                transform.position = new Vector3(0, transform.position.y, 0);
                resetTripping = false;
            }
        }
    }

    void resumeRunning()
    {
        indexJumping = 0;
        indexSliding = 0;
        indexTripping = 0;
        isSliding = false;
        isTripping = false;
        SR.sprite = running[indexRunning];
        updateFace();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Pursuer")
        {
            scoreKeeper.updateScore();
            Debug.Log("GAME OVER!");
            SceneManager.LoadScene("start");
        }
        if (!invincible && (
            col.gameObject.tag == "Jump" && !isJumping ||  //if missed the jump
            col.gameObject.tag == "Slide" && !isSliding))   //if missed the slide)
        {
            Debug.Log("Tripped!");
            isTripping = true;
            tripped = true;
            invincible = true;
            wait = 0;
        }
    }

    private void updateFace()
    {
        if (isSliding)
        {
            face.transform.position = new Vector3(transform.position.x - 1.1f, transform.position.y - 1.2f, -1);
        }
        else
        {
            face.transform.position = new Vector3(transform.position.x + 0.4f, transform.position.y + 1.3f, -1);
        }
    }
}
