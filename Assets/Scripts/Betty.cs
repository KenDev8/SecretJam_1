using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;
using TMPro;
public class Betty : MonoBehaviour, ICollector
{
    //_________________ Signelton Pattern _________________//
    public static Betty _instance;
    public static Betty Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Betty>();
            }
            return _instance;
        }
    }
    //_________________ Signelton Pattern _________________//

    private PlatformerMovement2D movement;
    private GunContoller gunContoller;
    private Animator anim;

    public int bonesCount = 0;
    public int maxBonesCount = 10;

    [Header("Heavy Betty Parameters")]
    public bool isHeavy = false;
    public int heavyEnterBonesCost;
    public int heavyBonesThreshold;
    public int heavyMaxBonesMultiplier;
    public float heavySpeedMultiplier;
    public float heavyJumpMultiplier;
    public float heavyGunSprayMultiplier;
    public TMP_Text stateText;
    public TMP_Text bonesText;
    public TMP_Text maxBonesText;
    public TMP_Text heavyTHText;

    private void Start()
    {
        movement = GetComponent<PlatformerMovement2D>();
        gunContoller = GetComponent<GunContoller>();
        anim = GetComponent<Animator>();

        gunContoller.OnShoot += BettyShot;
    }

    private void Update()
    {
        if(isHeavy)
        {
            movement.canMove = !gunContoller.isShooting;
        }

        anim.SetBool("isRunning", movement.isMoving);
        anim.SetBool("isGrounded", movement.isGrounded);
        anim.SetBool("isInAir", movement.isInAir);

        bonesText.text = bonesCount.ToString();
        maxBonesText.text = maxBonesCount.ToString();
        heavyTHText.text = heavyBonesThreshold.ToString();

    }

    public void Collect()
    {
        AudioManager.Instance.PlayPickUp();
        if(bonesCount < maxBonesCount)
        {
            bonesCount += 1;
            print(bonesCount);
        }

        if(bonesCount >= heavyBonesThreshold && !isHeavy)
        {
            EnterHeavyState();
        }
    }

    public void EnterHeavyState()
    {
        if (isHeavy)
            return;

        isHeavy = true;
        stateText.text = "Heavy >";
        bonesCount -= heavyEnterBonesCost;
        maxBonesCount += heavyMaxBonesMultiplier;
        movement.moveSpeed *= heavySpeedMultiplier;
        movement.jumpSpeed *= heavyJumpMultiplier;
        gunContoller.maxSprayDegree *= heavyGunSprayMultiplier;
    }

    public void EnterNormalState()
    {
        if (!isHeavy)
            return;

        stateText.text = "Normal <";
        isHeavy = false;
        maxBonesCount -= heavyMaxBonesMultiplier;
        movement.moveSpeed /= heavySpeedMultiplier;
        movement.jumpSpeed /= heavyJumpMultiplier;
        gunContoller.maxSprayDegree /= heavyGunSprayMultiplier;
    }

    public int DiscardBonesToPile()
    {
        AudioManager.Instance.PlayPileUpgrade();
        EnterNormalState();
        int bonesToReturn = bonesCount;
        bonesCount = 0;


        return bonesToReturn;
    }

    [Space]
    [Header("Protal Gun Parameters")]
    public SpriteRenderer gunSpriteRenderer;
    public Sprite portalGunSprite;
    public GameObject PortalGunBullets;

    public void PickUpPortalGun()
    {
        AudioManager.Instance.PlayPickUp();
        gunSpriteRenderer.sprite = portalGunSprite;
        gunContoller.projectile = PortalGunBullets;
    }

    public void AnimStep()
    {
        AudioManager.Instance.PlayPlayerRun();
    }

    public void BettyShot()
    {
        AudioManager.Instance.PlayShoot();
    }
}
