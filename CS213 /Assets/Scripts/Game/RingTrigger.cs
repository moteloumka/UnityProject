using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private GameObject ghostSheep; 
    private GhostSheepBehavior gsB;
    void Start()
    {
        ghostSheep = GameObject.FindWithTag("GhostSheep");
        gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
    }
    
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other){
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        if (!gsB.isAttacking && other.transform.parent.gameObject.tag.Equals("GhostSheep"))
        {
            /*
            ++gsB.player1Score;
            Debug.Log("player1Score : " + gsB.player1Score);
            GameObject closestPlayer = gsB.FindClosestPlayer();
            closestPlayer.GetComponent<GhostSheepBehavior>().
            */
            if (gsB.FindClosestPlayer().name == "CelluloAgent_2"){
                ++gsB.player1Score;
                Debug.Log("player1Score : " + gsB.player1Score);
            }
            else if (gsB.FindClosestPlayer().name == "CelluloAgent_1"){
                ++gsB.player2Score;
                Debug.Log("player2Score : " + gsB.player2Score);
            }
        }
     }
}
