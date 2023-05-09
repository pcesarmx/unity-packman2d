using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherColider)
    {
        if ( otherColider.tag == "Player" )
        {
            Destroy(this.gameObject);
            UIManager.sharedInstance.ScorePoints(100);
        }
    }
}
