using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform[] waypoints;
    int currentWaypoint = 0;
    bool shouldWaitHome = false;
    bool isPacmanKilled = false;


    public float speed = 0;

    private void Update()
    {
        GetComponent<Animator>().SetBool("PackmanInv", GameManager.sharedInstance.invincibleTime > 0);
    }

    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.gamePaused || GameManager.sharedInstance.gameStarted == false || shouldWaitHome == true || isPacmanKilled == true)
        {
            GetComponent<AudioSource>().volume = 0.0f;
            return;
        }

        GetComponent<AudioSource>().volume = 0.192f;
        float distanceToWaypoint = Vector2.Distance((Vector2) this.transform.position,
            (Vector2) waypoints[currentWaypoint].position );

        if ( distanceToWaypoint <= 0.1f )
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            Vector2 newDirection = waypoints[currentWaypoint].position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", newDirection.x);
            GetComponent<Animator>().SetFloat("DirY", newDirection.y);
        } else
        {
            Vector2 newPos = Vector2.MoveTowards((Vector2)this.transform.position,
                waypoints[currentWaypoint].position,
                speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(newPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherColider)
    {
        if (otherColider.tag == "Player")
        {
            if (GameManager.sharedInstance.invincibleTime <= 0)
            {
                GameManager.sharedInstance.gameStarted = false;
                GameManager.sharedInstance.PlayPacmanDeath();
                isPacmanKilled = true;
                //Destroy(otherColider.gameObject);
                otherColider.GetComponent<Animator>().SetBool("PacmanAlive", false);
                otherColider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                StartCoroutine("RestartGame");
            } else
            {
                GameObject home = GameObject.Find("Ghost Home");
                this.transform.position = home.transform.position;
                this.currentWaypoint = 0;
                shouldWaitHome = true;
                StartCoroutine("AwakeFromHome");
            }
        } 

    }

    IEnumerator RestartGame() {
        yield return new WaitForSecondsRealtime(4f);
        GameManager.sharedInstance.RestartGame();
    }

    IEnumerator AwakeFromHome()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        this.shouldWaitHome = false;
        this.speed *= 1.2f;
    }
}
