using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool IsUpdateRequired;

    private Dictionary<string, Image> hearths;
    // Start is called before the first frame update
    void Start()
    {
        hearths = new Dictionary<string, Image>();
        var allRenderers = gameObject.GetComponentsInChildren<Image>().ToList();

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
