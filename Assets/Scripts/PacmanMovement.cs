using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 destination = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        destination = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( GameManager.sharedInstance.gamePaused || GameManager.sharedInstance.gameStarted == false)
        {
            GetComponent<AudioSource>().volume = 0.0f;
            return;
        }

        GetComponent<AudioSource>().volume = 1.0f;
        Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(newPos);

        float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);
        //Debug.Log("Pacman esta en " + this.transform.position + " y su destino es " + destination + " y se encuentra a " + distanceToDestination + " de su destino." );

        if ( distanceToDestination < 2f)
        {
            //Debug.Log("Podemos movernos si el usuario pulsa una flecha");
            if ( Input.GetKey(KeyCode.UpArrow) && CanMoveTo(Vector2.up) )
            {
                destination = (Vector2)this.transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow) && CanMoveTo(Vector2.right) )
            {
                destination = (Vector2)this.transform.position + Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && CanMoveTo(Vector2.down) )
            {
                destination = (Vector2)this.transform.position + Vector2.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && CanMoveTo(Vector2.left) )
            {
                destination = (Vector2)this.transform.position + Vector2.left;
            }
        }
        // Setear variables de animacion 
        Vector2 dir = destination - (Vector2)this.transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }

    bool CanMoveTo(Vector2 dir) {
        Vector2 pacmanPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos + dir, pacmanPos );



        // SI el colider hit es el de pacman, entonces no hubo pared
        return hit.collider == GetComponent<Collider2D>();
    }
}
