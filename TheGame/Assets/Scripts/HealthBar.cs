using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

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
    }

    public void UpdateHealthbar(int lifesLeft)
    {
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
