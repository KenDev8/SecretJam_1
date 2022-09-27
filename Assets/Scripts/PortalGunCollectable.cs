using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class PortalGunCollectable : MonoBehaviour
{
    public LayerMask whatIsBetty;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatIsBetty))
        {
            Betty.Instance.PickUpPortalGun();
            Destroy(gameObject);
        }
    }
}

