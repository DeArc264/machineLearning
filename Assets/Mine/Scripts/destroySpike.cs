using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySpike : MonoBehaviour
{
    public avoidSpikes player;
    private void Awake()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        player.spikeRBs.Remove(GetComponent<Rigidbody>());
        Object.Destroy(gameObject);
    }
}
