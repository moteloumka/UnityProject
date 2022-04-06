using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private GameObject ghostSheep; 
    private GhostSheepBehavior gsB;

    public AudioSource source;
    public AudioClip getPointSound;
    void Start()
    {
        ghostSheep = GameObject.FindWithTag("GhostSheep");
        //print(ghostSheep==null);
        gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
        print(gsB == null);
    }
    
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other){
        // Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        if (!gsB.isAttacking && other.transform.parent.gameObject.tag.Equals("GhostSheep"))
        {
            Debug.Log("have we gotten here?");
            /*
            ++gsB.player1Score;
            Debug.Log("player1Score : " + gsB.player1Score);
            GameObject closestPlayer = gsB.FindClosestPlayer();
            closestPlayer.GetComponent<GhostSheepBehavior>().
            */
            print(gsB.FindClosestPlayer().name);
            if (gsB.FindClosestPlayer().name == "CelluloAgent_2"){
                ++gsB.player1Score;
                Debug.Log("player1Score : " + gsB.player1Score);
            }
            else if (gsB.FindClosestPlayer().name == "CelluloAgent_1"){
                ++gsB.player2Score;
                Debug.Log("player2Score : " + gsB.player2Score);
            }
            source.PlayOneShot(getPointSound);
                
        }
     }
}
