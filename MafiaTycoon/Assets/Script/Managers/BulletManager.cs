using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private int bulletCount;

    [SerializeField]
    private GameObject bulletPref;

    [SerializeField]
    private Transform desiredPos;

    private Queue<GameObject> bulletList;

    void Start()
    {
        bulletList=new Queue<GameObject>();
        for (int i = 0; i < bulletCount; i++)
        {
            var bullet = Instantiate(bulletPref,desiredPos.position,Quaternion.identity);
            bulletList.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }



    public void Shoot()
    {
        var bullet= bulletList.Dequeue();
        bullet.transform.eulerAngles = new Vector3(90, 0, 0);
        bullet.transform.SetParent(null);
        bullet.SetActive(true);
        bulletList.Enqueue(bullet);
    }

     
}
