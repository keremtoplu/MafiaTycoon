using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    private Camera cam;
    [SerializeField]
    private Movement player;

    [SerializeField]
    private Vector3 offSet;

    private Vector3 beginOffSett;
    private Quaternion beginRot;
    void Start()
    {
        GameManager.Instance.GameStateChanged+=OnGameStateChanged;
        cam=Camera.main;
        beginOffSett=offSet;
        beginRot=cam.transform.rotation;
    }


    void Update()
    {
        
        cam.transform.position=new Vector3(cam.transform.position.x,cam.transform.position.y,player.transform.position.z+offSet.z);


    }
    private void OnGameStateChanged()
    {
        var state=GameManager.Instance.GameState;
        Debug.Log(state);
        switch (state)
        {
            case GameStates.Start:
                offSet=beginOffSett;
                cam.transform.rotation=beginRot;
                break;

            case GameStates.Scenerio:
                offSet=Vector3.zero;
                cam.transform.position=new Vector3(cam.transform.position.x,cam.transform.position.y,player.transform.position.z);
                cam.transform.LookAt(player.PlayerLookPos.position);
                Debug.Log("asd");
                break;
            
        }
    }
}
