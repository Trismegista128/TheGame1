using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public bool IsUpdateRequired;

    private Dictionary<string, SpriteRenderer> hearths;
    // Start is called before the first frame update
    void Start()
    {
        hearths = new Dictionary<string, SpriteRenderer>();
        var allRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>().ToList();

        foreach(var component in allRenderers)
        {
            if (component.name == "Life container") continue;

            hearths.Add(component.name, component);
        }

        IsUpdateRequired = true;
    }

    public void UpdateHealthbar(int lifesLeft)
    {
        if (!IsUpdateRequired) IsUpdateRequired = false;

        foreach(var heart in hearths)
        {
            heart.Value.gameObject.SetActive(false);
        }

        for (var i = 1; i <= lifesLeft; i++)
        {
            hearths[$"Life{i}"].gameObject.SetActive(true);
        }
    }
}
