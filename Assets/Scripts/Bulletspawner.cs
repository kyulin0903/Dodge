using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletSpawner : MonoBehaviour
{
    [FormerlySerializedAs("bulletPerfab")]
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 2f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;
    private AudioSource gun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindFirstObjectByType<PlayerController>().transform;
        gun = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate * GameManager.Instance.CurrentSpawnScale)
        {
            timeAfterSpawn = 0f;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            gun?.Play();
            bullet.transform.LookAt(target);
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
