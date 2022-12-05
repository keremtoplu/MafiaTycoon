using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    [SerializeField]
    private Movement player;

    [SerializeField]
    private int damage;

    public void SaveButton()
    {
       var possibility=Random.Range(1f,10f);
       if(possibility>=1 && possibility<=6)
       {
            player.CurrnetClanMember+=1;
       }
       else
       {
            player.Health-=damage;
       }
       GameManager.Instance.UpdateGameState(GameStates.InGame);
    }
    
    public void DoNothingButton()
    {
        GameManager.Instance.UpdateGameState(GameStates.InGame);

    }
}
