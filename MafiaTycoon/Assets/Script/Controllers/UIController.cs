using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
   [SerializeField]
   private GameObject choosePanel;
   [SerializeField]
   private GameObject savePeoplePanel;

   [SerializeField]
   private Movement player;

   private float timer;

   private bool isTransition=false;


    private void Start()
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        choosePanel.SetActive(false);
        savePeoplePanel.SetActive(false);
        
    }

    private void Update() 
    {
        timer-=Time.deltaTime;    
        if(timer<=0 && isTransition==true)
        {
            choosePanel.SetActive(false);
            savePeoplePanel.SetActive(true);
        }
    }


    public void HelpButton()
    {
        GameManager.Instance.UpdateGameState(GameStates.Start);
    }

    public void DoNothingButton()
    {
        GameManager.Instance.UpdateGameState(GameStates.Start);
    }

    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        switch (state)
        {
            case GameStates.Start:
                isTransition=false;
                savePeoplePanel.SetActive(false);
                player.Speed=1f;
                break;

            case GameStates.Scenerio:
                choosePanel.SetActive(true);
                timer=2f;
                isTransition=true;
                Debug.Log("asd");
                break;
            
        }
    }
}
