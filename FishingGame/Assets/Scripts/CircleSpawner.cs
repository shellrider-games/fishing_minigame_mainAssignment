using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawner : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField][Range(0,32)] private int amount;

    public void OnDrawGizmos()
    {
        Vector3[] points = new Vector3[32];
        for (int i = 0; i < 32; i++)
        {
            points[i] = transform.position 
                        + radius * new Vector3(
                            Mathf.Cos(2 * i * Mathf.PI / 32),
                            0,
                            Mathf.Sin(2 * i * Mathf.PI / 32)
                            );
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawLineStrip(points, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPrefabs();
    }
    
    private void SpawnPrefabs()
    {
        for (int i = 0; i < amount; i++)
        {
            float radius = Random.Range(0, this.radius);
            float angle = Random.Range(0, 2 * Mathf.PI);
            Instantiate(prefabToSpawn, 
                transform.position + radius * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle))
                , Quaternion.Euler(0,Random.Range(0,360),0));
        }
    }
}
