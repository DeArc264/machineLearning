using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class avoidSpikes : Agent
{
    [SerializeField] private Transform targetTransform;

    float currentTime = 0;
    public List<Rigidbody> spikeRBs;
    [SerializeField] BufferSensorComponent sensorComponent;

    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        foreach (Rigidbody rb in spikeRBs)
        {
            float[] observation = {rb.position.x,rb.position.y,rb.position.z, rb.velocity.y};
            sensorComponent.AppendObservation(observation);
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 5f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

        Debug.Log(moveSpeed);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continousActions = actionsOut.ContinuousActions;
        continousActions[0] = Input.GetAxis("Horizontal");
        continousActions[1] = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<spike>(out spike spike))
        {
            AddReward(-2f);
            EndEpisode();
        }
        if (other.TryGetComponent<wall>(out wall wall))
        {
            AddReward(-5f);
            EndEpisode();
        }
    }

    public void Update()
    {
        currentTime += 1 * Time.deltaTime;
        if(currentTime >= 5)
        {
            AddReward(+1f);
            currentTime = 0;
        }
    }
}
