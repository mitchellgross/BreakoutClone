using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Blocker : MonoBehaviour {
    
    Vector2 mousePos;
    bool canClick;
    bool enableFollow;
    public GameObject ballPrefab;
    int remainingTargets;

    void Start()
    {
        mousePos = transform.position;
        canClick = true;
        enableFollow = false;
        GameObject.Find("Canvas").GetComponentInChildren<Text>().text = null;
        Time.timeScale = 1; //Resets the timeScale as a result of it equaling 0 after a win/loss
    }

    void Update()
    {
        remainingTargets = GameObject.FindGameObjectsWithTag("Target").Length;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)); //Converts the mouse's position to a useable Vector3

        if (enableFollow == true)
        {
            if (mousePos.x > -5.25f && mousePos.x < 5.25f)
            {
                transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z); //Sets this GameObject's X position to the mouse's X position
            }
            else if (mousePos.x < -5.25f)
            {
                transform.position = new Vector3(-5.25f, -4.65f, 0); //Manually set this GameObject's maximum negative X (Left)
            }
            else if (mousePos.x > 5.25f)
            {
                transform.position = new Vector3(5.25f, -4.65f, 0); //Manually set this GameObject's maximum positive X (Right)
            }
        }

        if (Input.GetMouseButtonDown(0) && canClick == true)
        {
            Instantiate (ballPrefab, transform.position + new Vector3 (0, 1, 0), Quaternion.identity); //Instantiate the ball slightly above this GameObject
            canClick = false;
            enableFollow = true;
        }

        if (Input.GetKeyDown(KeyCode.R)) //Allows the R key to reset the level
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
        if (remainingTargets <= 0)
        {
            Victory();
        }
    }

    void Victory()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").GetComponentInChildren<Text>().text = "Victory!";
    }

    void Failure()
    {
        Time.timeScale = 0;
        GameObject.Find("Canvas").GetComponentInChildren<Text>().text = "Failure! Press R to Try Again!";

    }
}
