using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;
using TMPro;

public class BonePile : MonoBehaviour
{
    public LayerMask bettyLayer;
    public TMP_Text currentText;
    public TMP_Text neededText;


    private Rigidbody2D rb;

    [Header("Bones Parameters")]
    public int boneCount = 0;
    public int bonesToUpgrade;
    public int boneThresholdToFall;
    private int currentPileStage = 0;

    [Header("Pile Stages")]
    public SpriteRenderer pile_0;
    public SpriteRenderer pile_1;
    public SpriteRenderer pile_2;

    //_________________ Signelton Pattern _________________//
    public static BonePile _instance;
    public static BonePile Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BonePile>();
            }
            return _instance;
        }
    }
    //_________________ Signelton Pattern _________________//

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.useFullKinematicContacts = true;

        pile_0.enabled = false;
        pile_1.enabled = false;
        pile_2.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, bettyLayer))
        {
            Betty betty = collision.gameObject.GetComponent<Betty>();
            if (betty.bonesCount <= 0)
                return;
            
            boneCount += betty.DiscardBonesToPile();

            if (boneCount >= bonesToUpgrade)
            {
                UpgradePile();
                GameManager.Instance.UpgradeRun();
            }

            if(boneCount >= boneThresholdToFall)
            {
                UpdatePileText();
                PileFall();
            }
        }
    }

    public void SetBonesToUpgrade(int _amount)
    {
        bonesToUpgrade = _amount;
        UpdatePileText();
    }

    private void UpdatePileText()
    {
        currentText.text = boneCount.ToString();
        neededText.text = bonesToUpgrade.ToString();
    }

    private void PileFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        AudioManager.Instance.PlayPileFall();
    }

    public void UpgradePile()
    {
        currentPileStage++;

        switch(currentPileStage)
        {
            case 1:
                pile_0.enabled = true;
                break;
            case 2:
                pile_1.enabled = true;
                break;
            case 3:
                pile_2.enabled = true;
                break;
        }
    }
}
