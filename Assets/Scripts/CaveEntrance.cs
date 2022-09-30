using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class CaveEntrance : MonoBehaviour
{
    public LayerMask whatIsBetty;
    public bool isInCave = true;
    public GameObject instructions;
    private bool destroyed_inst = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatIsBetty))
        {
            if (!destroyed_inst)
            {
                destroyed_inst = true;
                Destroy(instructions);
            }

            if(isInCave)
                AudioManager.Instance.PlayGraveYardAmbient();
            else
                AudioManager.Instance.PlayCaveAmbient();
        }

    }
}
