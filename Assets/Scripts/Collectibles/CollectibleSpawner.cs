using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject healPrefab;
    [SerializeField] private GameObject autoPrefab;
    [SerializeField] private float coinChance;
    [SerializeField] private float healChance;
    [SerializeField] private float autoChance;
    [SerializeField] private GameObject collectibleFolder;

    private List<Vector2Int> spawnQueueList = new();

    private void OnEnable() => GridController.OnGridInitalized += SpawnCollectibles;

    private void OnDisable() => GridController.OnGridInitalized -= SpawnCollectibles;

    private void SpawnCollectibles()
    {
        Vector2Int gridDimensions = new(GridController.grid.GetLength(0), GridController.grid.GetLength(1));

        for (int y = 0; y < gridDimensions.y; y++)
        {
            for (int x = 0; x < gridDimensions.x; x++)
            {
                if (GridController.grid[x, y])
                {
                    spawnQueueList.Add(new Vector2Int(x, y));
                }
            }
        }

        foreach(Vector2Int item in ShuffleSpawnQueue(spawnQueueList))
        {
            InstantiateCollectible(item.x, item.y);
        }
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

    private void InstantiateCollectible(int x, int z)
    {
        //auto
        if (Random.Range(0f, 1f) <= autoChance)
        {
            GameObject auto = Instantiate(autoPrefab, new(x, 0.25f, z), Quaternion.identity);
            auto.transform.parent = collectibleFolder.transform;
        }
        //heal
        else if (Random.Range(0f, 1f) <= healChance)
        {
            GameObject heal = Instantiate(healPrefab, new(x, 0.25f, z), Quaternion.identity);
            heal.transform.parent = collectibleFolder.transform;
        }
        //coin
        else if (Random.Range(0f, 1f) <= coinChance)
        {
            GameObject coin = Instantiate(coinPrefab, new(x, 0.25f, z), Quaternion.identity);
            coin.transform.parent = collectibleFolder.transform;
        }
        
        
    }
}
