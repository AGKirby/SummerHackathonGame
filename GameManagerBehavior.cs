using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    public Text scoreLabel;
    public int score = 0;
    public int level = 1;

    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 10)
        {
            counter = 0;
            score += 1;
            scoreLabel.GetComponent<Text>().text = "SCORE: " + score;
            if(score % 100 == 0)
            {
                level++;
            }
        }
        counter++;
    }
}
