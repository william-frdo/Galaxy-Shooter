using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] _powerups;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    // Create a coroutine to  spawn the Enemy every 5 seconds
    IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7F, 7F), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0F);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (true)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerup], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0F);
        }
    }
}
