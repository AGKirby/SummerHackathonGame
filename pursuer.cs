using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pursuer : MonoBehaviour
{
    private int counter;
    public SpriteRenderer SR;
    public Sprite[] running;
    private int countRunning;
    private int indexRunning;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        indexRunning = 0;
        countRunning = running.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 30)
        {
            indexRunning++;
            if (indexRunning == countRunning)
            {
                indexRunning = 0;
            }
            SR.sprite = running[indexRunning];
            counter = 0;
        }
        counter++;
    }
}
