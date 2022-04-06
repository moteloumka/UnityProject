using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class GhostSheepBehavior : AgentBehaviour
{
    public Boolean isAttacking = false;
    private const float MIN_TIME_SHEEP = 14f;
    private const float MAX_TIME_SHEEP = 15f;
    private const float MIN_TIME_GHOST = 6f;
    private const float MAX_TIME_GHOST = 8f;
    private const float MIN_DIST_FOR_REACTION_GHOST = 20f;
    private const float MIN_DIST_FOR_REACTION_SHEEP = 6f;

    private float MIN_DIST_FOR_REACTION = 6f;

    public int player1Score = 0;
    public int player2Score = 0;
    
    /*
    private long rSheep = 0;
    private long gSheep = 0;
    private long bSheep = 0;
    
    private long rGhost = 200;
    private long gGhost = 0;
    private long bGhost = 0;
    */

    private GameObject _player1;
    private GameObject _player2;

    private MoveWithKeyboardBehavior _behavior1;
    private MoveWithKeyboardBehavior _behavior2;
    
    

    public void Start()
    {
        _player1 = GameObject.Find("CelluloAgent_2");
        _player2 = GameObject.Find("CelluloAgent_1");
        
        _behavior1 = _player1.GetComponent<MoveWithKeyboardBehavior>();
        _behavior2 = _player2.GetComponent<MoveWithKeyboardBehavior>();
        
        transformIntoSheep();
    }

    private void transformIntoGhost()
    {
        isAttacking = true;
        MIN_DIST_FOR_REACTION = MIN_DIST_FOR_REACTION_GHOST;
        float time = UnityEngine.Random.Range(MIN_TIME_GHOST, MAX_TIME_GHOST);
        this.agent.SetVisualEffect(0, Color.red, 100);
        Invoke("transformIntoSheep",time);
    }

    private void transformIntoSheep()
    {
        isAttacking = false;
        MIN_DIST_FOR_REACTION = MIN_DIST_FOR_REACTION_SHEEP;
        float time = UnityEngine.Random.Range(MIN_TIME_SHEEP, MAX_TIME_SHEEP);
        this.agent.SetVisualEffect(0, Color.green, 100);
        Invoke("transformIntoGhost",time);
    }
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Dog");
        Vector3 movementVector3 = new Vector3(0f, 0f, 0f);
        
        players = players.Where(p =>
                Vector3.Distance(p.transform.position, transform.position) < MIN_DIST_FOR_REACTION)
            .ToArray();
        //if there's no players around = just stay in the same point
        if (players.Length == 0)
            return steering;
        //the sheep is running away
        if (!isAttacking)
            foreach (GameObject player in players)
                movementVector3 += transform.position - player.transform.position;
        //sheep following closest to it player
        else
        {
            //implementation of some shit code to find closest player
            float minDist = players.Min(p => Vector3.Distance(p.transform.position , transform.position));
            players = players.Where(p =>
                    Vector3.Distance(p.transform.position , transform.position) <= minDist)
                .ToArray();
            movementVector3 = players[0].transform.position - transform.position;
        }

        steering.linear = (movementVector3 /(float)players.Length) * (agent.maxAccel / 3f) ; //
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear , agent.maxAccel)) ;
        return steering;
    }


    
    void OnCollisionEnter(Collision col)
    {
        if (isAttacking)
        {
            if (col.gameObject.name == "CelluloAgent_2")
            {
                --player1Score;
                Debug.Log("player1Score : " + player1Score);
            }
            else if (col.gameObject.name == "CelluloAgent_1")
            {
                --player2Score;
                Debug.Log("player2Score : " + player2Score);
            }
            
        }
        
    }

    public GameObject FindClosestPlayer()
    {
        Vector3 pos1 = _player1.transform.position;
        Vector3 pos2 = _player2.transform.position;
        Vector3 posGS = this.transform.position;
        GameObject closestPlayer;
        if ((posGS - pos1).magnitude <= (posGS - pos2).magnitude) closestPlayer = _player1;
        else closestPlayer = _player2;
//
        Debug.Log("the closest player is : " + closestPlayer.name);
        return closestPlayer;
    }

}
