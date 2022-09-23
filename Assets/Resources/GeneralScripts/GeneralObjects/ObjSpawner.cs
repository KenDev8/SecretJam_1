using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KenDev
{
    public enum SpawnerType { onSelf, onPointsList, onScaleArea }

    public class ObjSpawner : MonoBehaviour
    {
        [Header("Spawn Parameters")]
        public List<GameObject> objectsList;
        public float spawnRateInSec = 1f;
        public bool rapidSpawn = true;

        [Space]
        [Header("Spawner Type Parameters")]
        public SpawnerType spawnerType;
        public List<Transform> spawnPoints;


        private bool spawnEnabled = true;
        private float curSpawnTime = 0f;
        private bool canSpawn = true;
        private float speed_multiplier = 1f;

        private SpriteRenderer sr;



        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            sr.enabled = false;
        }

        //Spawn handeling
        private void HandleSpawn()
        {
            if ((curSpawnTime >= spawnRateInSec) && canSpawn)
            {
                Spawn();
                if (!rapidSpawn)
                    CanSpawn(false);
                curSpawnTime = 0f;
            }
            else
            {
                curSpawnTime += Time.deltaTime;
            }
        }

        public void CanSpawn(bool b)
        {
            canSpawn = b;
        }

        private void Spawn()
        {
            GameObject obj = objectsList[Random.Range(0, objectsList.Count)];
            Vector2 pos = transform.position;

            switch (spawnerType)
            {
                case SpawnerType.onSelf:
                    // Spawn objects on Item Spawner game object
                    break;

                case SpawnerType.onPointsList:
                    // Spawn objects on a random spawn point from a list of points
                    if (spawnPoints.Count < 1)
                    {
                        Debug.Log("* No spawn points defined, changing to 'onSelf' mode");
                        spawnerType = SpawnerType.onSelf;
                        break;
                    }
                    pos = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                    break;

                case SpawnerType.onScaleArea:
                    // Spawn objects randomly in an area defined by the spawner local scale
                    float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
                    float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
                    pos = new Vector2(x, y);
                    break;
            }

            Instantiate(obj, pos, Quaternion.identity);
        }


        //Enable/Disable spawner
        public void EnableSpawn()
        {
            spawnEnabled = true;
        }

        public void DisableSpawn()
        {
            spawnEnabled = false;
        }

        public void SpeedUp(float _multiplier)
        {
            speed_multiplier *= _multiplier;
        }

        private void Update()
        {
            //  dont spawn if disabled or no items to spawn
            if (!spawnEnabled || objectsList.Count < 1)
                return;

            HandleSpawn();
        }
    }


}
