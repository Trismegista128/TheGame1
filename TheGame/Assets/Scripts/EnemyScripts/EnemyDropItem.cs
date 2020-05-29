using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Types;
using System.Linq;
using System.Collections.Generic;

public class EnemyDropItem : MonoBehaviour
{
    public ItemType[] Dropping;

    [SerializeField]
    private Transform tr;

    public void Drop()
    {
        var resources = Resources.LoadAll<PickableItem>("Items");
        var filteredResources = resources.Where(x => Dropping.Contains(x.KindOf)).ToList();

        var loterryIndex = Random.Range(0, 2);
        var pickedItem = filteredResources[loterryIndex];

        Instantiate(pickedItem.gameObject, tr.position, new Quaternion());
    }
}
