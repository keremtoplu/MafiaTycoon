using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Update()
    {
        transform.position+=Vector3.forward;
    }
}
