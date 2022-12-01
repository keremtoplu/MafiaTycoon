using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    Animator anim;
    float speed=1f;
    bool isRun=true;
    void Start()
    {
        anim=GetComponent<Animator>();
        
    }

    void Update()
    {
        transform.position+=new Vector3(0,0,1f*Time.deltaTime*speed);
        if(isRun)
        {
            anim.SetTrigger("IsRun");

            Debug.Log(isRun);
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        var scenario=other.GetComponent<Scenerio>();
        if(scenario)
        {
            isRun=false;
            anim.ResetTrigger("IsRun");
            anim.SetTrigger("Idle");
            speed=0;
            transform.LookAt(scenario.transform.position);
            
        }
    }
}
