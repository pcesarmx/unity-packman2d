using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text packmanLabel;
    public Text scoreLabel;
    public static UIManager sharedInstance = null;
    private int totalPoints = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {
            packmanLabel.enabled = true;
        } else {
            packmanLabel.enabled = false;
        }
        
    }

    public void ScorePoints(int points)
    {
        totalPoints += points;
        scoreLabel.text = "SCORE: " + totalPoints;
        if ( totalPoints > 27300)
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
