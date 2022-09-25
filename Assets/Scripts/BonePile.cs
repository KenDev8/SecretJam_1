using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class BonePile : MonoBehaviour
{
    public LayerMask bettyLayer;
    private Rigidbody2D rb;

    [Header("Bones Parameters")]
    public int boneCount = 0;
    public int boneThresholdToFall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.useFullKinematicContacts = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, bettyLayer))
        {
            Betty betty = collision.gameObject.GetComponent<Betty>();
            boneCount += betty.DiscardBonesToPile();

            if(boneCount >= boneThresholdToFall)
            {
                PileFall();
            }
        }
    }

    private void PileFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
