using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private ParticleSystem explosionPariclePrefab;

    private Spawner spawner;
    private Rigidbody targetRb;
    private float minSpeed = 12f;
    private float maxSpeed = 16f;
    private float maxTorque = 10f;
    private float xRange = 4f;
    private float ySpawnPos = -4f;

    public void SetSpawner(Spawner spawner)
    {
        this.spawner = spawner;
    }

    private void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
        transform.position = SpawnPosition();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private Vector3 RandomTorque()
    {
        float newX = Random.Range(-maxTorque, maxTorque);
        float newY = Random.Range(-maxTorque, maxTorque);
        float newZ = Random.Range(-maxTorque, maxTorque);
        return new Vector3(newX, newY, newZ);
    }

    private Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        spawner.UpdateScore(pointValue);
        Destroy(gameObject);
        Instantiate(explosionPariclePrefab, transform.position, Quaternion.identity);

    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!CompareTag("Bad"))
        {
            spawner.GameOver();
        }

        Target[] targets = FindObjectsOfType<Target>();
        foreach (var e in targets)
        {
            e.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            e.gameObject.GetComponent<Collider>().enabled = false;
        }
    }

}
