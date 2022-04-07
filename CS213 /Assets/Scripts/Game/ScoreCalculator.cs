using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject ghostSheep; 
    private GhostSheepBehavior gsB;
    private Boolean draw;
    public Text score;
    
    void Start()
    {
        ghostSheep = GameObject.FindWithTag("GhostSheep");
        gsB = ghostSheep.GetComponent<GhostSheepBehavior>();
        if (gsB.player1Score == gsB.player2Score)
            score.text ="It's a draw with "+ gsB.player1Score.ToString()+" points :)";
        
        if (gsB.player1Score > gsB.player2Score)
            score.text = "Player 1 has won with "+gsB.player1Score.ToString()+" points";
        else
            score.text = "Player 2 has won with "+gsB.player2Score.ToString()+" points";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
