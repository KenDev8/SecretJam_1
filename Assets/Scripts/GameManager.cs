using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class GameManager : MonoBehaviour
{

    //_________________ Signelton Pattern _________________//
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    //_________________ Signelton Pattern _________________//

    [Space]
    [Header("General")]
    public int currentRunNum = 0;
    public int currentUpgradeThreshold;
    public List<GraveCluster> gravesClusters;

    [Space]
    [Header("Run 1")]
    public int run1ClusterMaxSpawns0 = 5;
    public int run1ClusterMaxSpawns1 = 10;
    public int run1ClusterMaxSpawns2 = 10;
    public int run1ClusterMaxSpawns3 = 15;
    public int run1BonesToUpgradePile = 15;
    public int run1HeavyThreshold = 15;

    [Space]
    [Header("Run 2")]
    public int run2ClusterMaxSpawns0 = 8;
    public int run2ClusterMaxSpawns1 = 12;
    public int run2ClusterMaxSpawns2 = 15;
    public int run2ClusterMaxSpawns3 = 20;
    public int run2BonesToUpgradePile = 65;
    public int run2HeavyThreshold = 25;

    [Space]
    [Header("Run 3")]
    public int run3ClusterMaxSpawns0 = 10;
    public int run3ClusterMaxSpawns1 = 15;
    public int run3ClusterMaxSpawns2 = 20;
    public int run3ClusterMaxSpawns3 = 25;
    public int run3BonesToUpgradePile = 120;
    public int run3HeavyThreshold = 50;


    private void Start()
    {
        currentUpgradeThreshold = run1BonesToUpgradePile;
        UpgradeRun();
    }

    public void BonesDiscardedToPile(int _bonesAmount)
    {
        if(_bonesAmount > currentUpgradeThreshold)
        {
            currentRunNum++;
        }
    }

    public void UpgradeRun()
    {
        currentRunNum++;
        foreach (GraveCluster cluster in gravesClusters)
            cluster.clusterSpawned = false;


        switch (currentRunNum)
        {
            case 1:
                gravesClusters[0].maxSpawns = run1ClusterMaxSpawns0;
                gravesClusters[1].maxSpawns = run1ClusterMaxSpawns1;
                gravesClusters[2].maxSpawns = run1ClusterMaxSpawns2;
                gravesClusters[3].maxSpawns = run1ClusterMaxSpawns3;
                Betty.Instance.heavyBonesThreshold = run1HeavyThreshold;
                BonePile.Instance.bonesToUpgrade = run1BonesToUpgradePile;
                break;
            case 2:
                gravesClusters[0].maxSpawns = run2ClusterMaxSpawns0;
                gravesClusters[1].maxSpawns = run2ClusterMaxSpawns1;
                gravesClusters[2].maxSpawns = run2ClusterMaxSpawns2;
                gravesClusters[3].maxSpawns = run2ClusterMaxSpawns3;
                Betty.Instance.heavyBonesThreshold = run2HeavyThreshold;
                BonePile.Instance.bonesToUpgrade = run2BonesToUpgradePile;
                break;
            case 3:
                gravesClusters[0].maxSpawns = run3ClusterMaxSpawns0;
                gravesClusters[1].maxSpawns = run3ClusterMaxSpawns1;
                gravesClusters[2].maxSpawns = run3ClusterMaxSpawns2;
                gravesClusters[3].maxSpawns = run3ClusterMaxSpawns3;
                Betty.Instance.heavyBonesThreshold = run3HeavyThreshold;
                BonePile.Instance.bonesToUpgrade = run3BonesToUpgradePile;
                break;
        }
    }
}
