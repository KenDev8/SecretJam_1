using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KenDev
{
    public class Dropper : MonoBehaviour
    {
        public GameObject itemDrop;
        public int maxDropCount = 1;

        public void Drop()
        {
            for(int i = 0; i < Random.Range(1, maxDropCount); i++)
            {
                Instantiate(itemDrop, transform.position, Quaternion.identity);
            }
        }
    }
}
