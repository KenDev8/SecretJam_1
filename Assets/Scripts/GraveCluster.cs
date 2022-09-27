using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KenDev;

public class GraveCluster : MonoBehaviour
{
    public List<Grave> graves;
    public LayerMask whatIsBetty;
    public int maxSpawns = 3;
    public float timeBtwSpawns = 1;
    public bool clusterSpawned = false;
    public int currentSpawns = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(MyUtilities.Instance.CheckCollisionWithLayer(collision.gameObject.layer, whatIsBetty))
        {
            if (clusterSpawned)
                return;

            clusterSpawned = true;
            StartCoroutine(SpawnSkellies());
        }
    }

    public IEnumerator SpawnSkellies()
    {
        for(int i = 0; i<maxSpawns; i++)
        {
            graves[Random.Range(0, graves.Count)].TriggerSpawn();
            currentSpawns++;
            yield return new WaitForSeconds(timeBtwSpawns);
        }
    }
}
