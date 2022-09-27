using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class Grave : MonoBehaviour
{
    private Animator anim;
    private ObjSpawner spawner;
    void Start()
    {
        anim = GetComponent<Animator>();
        spawner = GetComponent<ObjSpawner>();
    }

    public void TriggerSpawn()
    {
        anim.SetTrigger("spawn");
    }

    public void SpawnSkelly()
    {
        spawner.Spawn();
    }
}

