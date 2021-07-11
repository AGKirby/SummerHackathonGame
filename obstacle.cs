using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public float moveBy = -0.1f;
    private int counter;
    public BoxCollider2D BC;
    private GameManagerBehavior gameManager;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 5)
        {
            transform.position += new Vector3(moveBy - 0.01f * (gameManager.level - 1), 0, 0);
            counter = 0;
        }
        counter++;
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        BC.isTrigger = true;
    }
}
