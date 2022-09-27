using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

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

    private void Start()
    {
        movement = GetComponent<PlatformerMovement2D>();
        gunContoller = GetComponent<GunContoller>();
    }

    private void Update()
    {
        if(isHeavy)
        {
            movement.canMove = !gunContoller.isShooting;
        }
    }

    public void Collect()
    {
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
        bonesCount -= heavyEnterBonesCost;
        maxBonesCount *= heavyMaxBonesMultiplier;
        movement.moveSpeed *= heavySpeedMultiplier;
        movement.jumpSpeed *= heavyJumpMultiplier;
        gunContoller.maxSprayDegree *= heavyGunSprayMultiplier;
    }

    public void EnterNormalState()
    {
        if (!isHeavy)
            return;

        isHeavy = false;
        maxBonesCount /= heavyMaxBonesMultiplier;
        movement.moveSpeed /= heavySpeedMultiplier;
        movement.jumpSpeed /= heavyJumpMultiplier;
        gunContoller.maxSprayDegree /= heavyGunSprayMultiplier;
    }

    public int DiscardBonesToPile()
    {
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
        gunSpriteRenderer.sprite = portalGunSprite;
        gunContoller.projectile = PortalGunBullets;
    }

}
