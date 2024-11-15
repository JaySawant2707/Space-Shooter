using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float TimeBetweenWaves = 2f;
    [SerializeField] bool isLooping;
    WaveConfig currentWave;

    void Start()
    {
        StartCoroutine("SpawwnEnemyWaves");
    }

    public WaveConfig GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawwnEnemyWaves()
    {
        do
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180));

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
            }
            yield return new WaitForSeconds(TimeBetweenWaves);

        }
        while (isLooping);
    }
}
