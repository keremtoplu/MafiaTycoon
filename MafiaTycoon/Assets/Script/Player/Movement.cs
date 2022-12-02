using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    Animator anim;
    private float speed=1f;
    private Quaternion beginRotation;
    private Transform playerLookPos;
    public Transform PlayerLookPos=>playerLookPos;

    public float Speed
    { 
        get{
        
            return speed; 
        }
        set
        {
            speed=value;
        } }
    void Start()
    {
        anim=GetComponent<Animator>();
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        beginRotation=transform.rotation;
        anim.SetTrigger("IsRun");
        
    }

    void Update()
    {
        transform.position+=new Vector3(0,0,1f*Time.deltaTime*speed);  
    }

    private void OnTriggerEnter(Collider other) 
    {
        var scenario=other.GetComponent<Scenerio>();
        var finalPlatform=other.GetComponent<FinalPlatform>();
        if(scenario)
        {
            
            playerLookPos=scenario.LookAtPos;
            GameManager.Instance.UpdateGameState(GameStates.Scenerio);
            
        }
        else if(finalPlatform)
        {
            GameManager.Instance.UpdateGameState(GameStates.Final);
        }
    }

    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        switch (state)
        {
            case GameStates.Start:
                transform.rotation=beginRotation;
                anim.SetTrigger("IsRun");
                break;

            case GameStates.Scenerio:

                anim.ResetTrigger("IsRun");
                anim.SetTrigger("Idle");
                speed=0f;
                transform.LookAt(playerLookPos.position);
                break;
            case GameStates.Final:
                anim.SetTrigger("Idle");
                speed=0f;
                break;
        }
    }
}
