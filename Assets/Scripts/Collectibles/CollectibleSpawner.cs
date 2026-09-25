using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectibleSpawner : MonoBehaviour
{
    [Tooltip("Elements indexed closer to 0 have greater spawn priority")]
    [SerializeField] private CollectibleSO[] collectibles;
    [SerializeField] private GameObject collectibleFolder;
    [SerializeField] private float respawnDelay;

    public static event Action ReInitalizeGrid;

    private void OnEnable() => GetComponent<GridController>().OnGridInitalized += SpawnCollectibles;

    private void OnDisable() => GetComponent<GridController>().OnGridInitalized -= SpawnCollectibles;

    private void SpawnCollectibles()
    {
        List<Vector2Int> spawnPosList = ShuffleSpawnQueue(GetSpawnQueueList());

        foreach (CollectibleSO collectible in collectibles)
        {
            int currentSpawnedAmount = GetCurrentSpawnedAmount(collectible);

            if (currentSpawnedAmount < collectible.spawnAmount)
            {
                int spawnAmount;

                if (currentSpawnedAmount == 0) spawnAmount = collectible.spawnAmount;
                else spawnAmount = (int)((collectible.spawnAmount - currentSpawnedAmount) * collectible.repawnPercent);

                for (int i = 0; i < spawnAmount; i++)
                {
                    if (spawnPosList.Count <= 0) return;
                    InstantiateCollectible(collectible, spawnPosList[0]);
                    spawnPosList.RemoveAt(0);
                }
            }
        }

        StartCoroutine(RespawnTimer());
    }

    private List<Vector2Int> ShuffleSpawnQueue(List<Vector2Int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            
            Vector2Int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    private List<Vector2Int> GetSpawnQueueList()
    {
        List<Vector2Int> spawnQueueList = new();

        Vector2Int gridDimensions = new(GetComponent<GridController>().grid.GetLength(0), GetComponent<GridController>().grid.GetLength(1));

        for (int y = 0; y < gridDimensions.y; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                if (GetComponent<GridController>().grid[x, y])
                {
                    spawnQueueList.Add(new Vector2Int(x, y));
                }
            }
        }

        //foreach(Vector2Int vector in spawnQueueList)
        //{
        //    Debug.Log(vector);
        //}

        return spawnQueueList;
    }

    private int GetCurrentSpawnedAmount(CollectibleSO collectible)
    {
        int counter = 0;

        foreach(Transform obj in collectibleFolder.transform)
        {
            if(obj.gameObject.tag == collectible.prefab.tag)
            {
                counter++;
            }
        }

        return counter;
    }

    private void InstantiateCollectible(CollectibleSO collectible, Vector2Int position)
    {
        GameObject obj = Instantiate(collectible.prefab, new(position.x, 0.25f, position.y), Quaternion.identity);
        obj.transform.parent = collectibleFolder.transform;
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnDelay);
        ReInitalizeGrid?.Invoke();
    }
}
