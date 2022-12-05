using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPref;

    private int currentClanMember;
    Animator anim;
    private float speed;
    private int health=100;
    private Quaternion beginRotation;
    private Transform playerLookPos;
    public Transform PlayerLookPos=>playerLookPos;
    public int CurrnetClanMember {
         get
         {
            return currentClanMember;
        } 
        set
        {
            currentClanMember=value; 
            ClanMemberChanged?.Invoke();
        }}
    public int Health { get
    {
        return health;
    } 
    set
    {
        if(health>0)
        {
            health=value;
            HealthChanged?.Invoke();
        }
    }}

    public float Speed
    { 
        get{
        
            return speed; 
        }
        set
        {
            speed=value;
        } 
    }

    public event Action ClanMemberChanged;
    public event Action HealthChanged;

    private void Start() 
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        anim=GetComponent<Animator>();
    }
    void Update()
    {
        transform.position+=new Vector3(0,0,1f*Time.deltaTime*speed);  
    }

    public void clonePlayer()
    {
        for (int i = 1; i <= currentClanMember; i+=2)
        {
            var k=1;
            Instantiate(playerPref,new Vector3(transform.position.x+(k*0.7f),transform.position.y,transform.position.z),Quaternion.identity);
            k++;
            
        }
        for (int j = 2; j <= currentClanMember; j+=2)
        {
            var l=1;
            Instantiate(playerPref,new Vector3(transform.position.x-(l*0.7f),transform.position.y,transform.position.z),Quaternion.identity);
            l++;
        }
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
                beginRotation=transform.rotation;
                currentClanMember=0;
                speed=0;
                break;
            case GameStates.InGame:
                transform.rotation=beginRotation;
                anim.SetTrigger("IsRun");
                speed=1f;
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
