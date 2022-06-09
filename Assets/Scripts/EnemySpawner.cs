using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   public Enemy enemyPrefab;
    public float spawnRate = 2f;
    public int spawnAmount = 1;
    public float spawnDistance = 15f;
    public float trajectoryVar = 15f;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn),this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        for(int i= 0;i<spawnAmount;i++)
        {   
           //random spawn point
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;
            //random rotation\
            float var = Random.Range(-this.trajectoryVar, this.trajectoryVar);
            Quaternion rotation = Quaternion.AngleAxis(var,Vector3.forward);

            Enemy enemy = Instantiate(this.enemyPrefab, spawnPoint, rotation);

            enemy.size = Random.Range(enemy.minSize,enemy.maxSize);
            enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}

