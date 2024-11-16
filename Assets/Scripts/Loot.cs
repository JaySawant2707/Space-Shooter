using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] List<GameObject> lootItems;

    public GameObject GetRandomLoot()
    {
        int num = Random.Range(0, lootItems.Count);
        return lootItems[num];
    }
}
