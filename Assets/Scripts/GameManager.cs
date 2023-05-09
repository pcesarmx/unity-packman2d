using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance = null;
    public bool gameStarted = false;
    public bool gamePaused = false;
    public AudioClip pauseAudio, pacmanDied;
    
    public float invincibleTime = 0f;

    private void Awake()
    {
        if ( sharedInstance == null)
        {
            sharedInstance = this;
        }

        StartCoroutine("StartGame");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameStarted == true)
        {
            gamePaused = !gamePaused;
            PlayStopPauseMusic(gamePaused);
        }

        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
        }
    }

    void PlayStopPauseMusic(bool play)
    {
        if (play == true)
        {
            GetComponent<AudioSource>().clip = pauseAudio;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
            //GameObject home =
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity.Set(0, 0);
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 0;

        }
        else
        {
            GetComponent<AudioSource>().Stop();
            //GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    public void PlayPacmanDeath()
    {
        GetComponent<AudioSource>().clip = pacmanDied;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
    }

    IEnumerator StartGame() {
        yield return new WaitForSecondsRealtime(5.0f);
        gameStarted = true;
    }

    public void MakeInvencible(float secs) {
        invincibleTime += secs;
    }
}
