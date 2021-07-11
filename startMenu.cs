using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            SceneManager.LoadScene("whitehouse");
        }
        else if (Input.GetKey("2"))
        {
            SceneManager.LoadScene("forest");
        }
        else if (Input.GetKey("3"))
        {
            SceneManager.LoadScene("test");
        }

    }
}
