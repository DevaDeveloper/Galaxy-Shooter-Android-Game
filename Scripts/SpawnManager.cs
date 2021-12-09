using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy2Prefab;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _EnemyBoss;
    
    

    private bool _stopSpawning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnBoss());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(15f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPlace = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            GameObject newBoss = Instantiate(_EnemyBoss, spawnPlace, Quaternion.identity);
            newBoss.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(20.0f);
        }
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        while (_stopSpawning == false)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            GameObject newEnemy2 = Instantiate(_enemy2Prefab, posToSpawn, Quaternion.identity);
            newEnemy2.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        while (_stopSpawning == false)
        {
            
            Vector3 posToSpawnPowerUp = new Vector3(Random.Range(-7f, 7f), 7f, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], posToSpawnPowerUp, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5, 8));
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
