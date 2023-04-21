using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Object;
    public Transform[] spawnPoints;

    private void Start()
    {
        Spawner();
    }

    void Spawner()
    {
        for (int i = 0; i < spawnPoints.Length;)
        {
            Instantiate(Object, spawnPoints[i].position, spawnPoints[i].rotation);
            i++;
        }
    }
}
