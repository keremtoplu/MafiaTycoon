using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   [SerializeField]
   private GameObject choosePanel;
   [SerializeField]
   private GameObject startPanel;
   [SerializeField]
   private GameObject savePeoplePanel;

   [SerializeField]
   private GameObject ınGamePanel;

   [SerializeField]
   private Text clanMemberValue;

    [SerializeField]
   private Text healthValue;

   [SerializeField]
   private Movement player;

   private float timer;

   private bool isTransition=false;


    private void Start()
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        
    }


    private void Update() 
    {
        timer-=Time.deltaTime;    
        if(timer<=0 && isTransition==true)
        {
            AllPanelClose();
            savePeoplePanel.SetActive(true);
        }
    }

    private void AllPanelClose()
    {
        choosePanel.SetActive(false);
        startPanel.SetActive(false);
        savePeoplePanel.SetActive(false);
        ınGamePanel.SetActive(false);
    }

    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        switch (state)
        {
            case GameStates.Start:
                player.ClanMemberChanged+=OnClanMemberChanged;
                player.HealthChanged+=OnHealtChanged;
                AllPanelClose();
                startPanel.SetActive(true);
                break;

            case GameStates.InGame:
                isTransition=false;
                AllPanelClose();
                ınGamePanel.SetActive(true);
                player.Speed=1f;
                break;

            case GameStates.Scenerio:
                AllPanelClose();
                choosePanel.SetActive(true);
                timer=2f;
                isTransition=true;
                break;
            
        }
    }
    private void OnClanMemberChanged()
    {
        clanMemberValue.text=player.CurrnetClanMember.ToString();
    }
    private void OnHealtChanged()
    {
        
        healthValue.text=player.Health.ToString();
    }
}
