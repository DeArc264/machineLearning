using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class randomSpawn : MonoBehaviour
{
    public GameObject spikePrefab;
    public avoidSpikes player;

    float Timer = 0;
    float startingTimer = 1;

    void Start()
    {
        Timer = startingTimer;
    }
    void Update()
    {
        Timer -= 1 * Time.deltaTime;
        if(Timer <= 0)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4, 5), 5, Random.Range(-4, 5));
            GameObject spike = Instantiate(spikePrefab, randomSpawnPosition, Quaternion.identity);
            player.spikeRBs.Add(spike.GetComponent<Rigidbody>());
            spike.GetComponent<destroySpike>().player = player;
            Timer = startingTimer;
        }
    }
}
