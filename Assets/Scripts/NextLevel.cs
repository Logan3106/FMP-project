using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private BridgeScript bridgeScript;

    public Transform[] Enemies;
    public int kills = 0;
    public GameObject bridge;

    void Start()
    {
        bridgeScript = bridge.GetComponent<BridgeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kills == 8)
        {
            bridge.GetComponent<MeshRenderer>().enabled = true;
            bridge.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void AddKill()
    {
        kills++;
    }
}
