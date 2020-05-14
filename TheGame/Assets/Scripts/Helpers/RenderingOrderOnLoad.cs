using UnityEngine;

public class RenderingOrderOnLoad : MonoBehaviour
{
    public float ObjectHeight;

    private SpriteRenderer renderer;
    void Start()
    {
        var component = GetComponent<SpriteRenderer>();
        renderer = component ?? GetComponentInChildren<SpriteRenderer>();

        renderer.sortingOrder = (int)((renderer.transform.position.y + ObjectHeight) * -100);
    }
}
