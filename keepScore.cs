using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepScore : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        DontDestroyOnLoad(transform.gameObject);
    }

    public void updateScore()
    {
        score = gameManager.score;
    }
}
