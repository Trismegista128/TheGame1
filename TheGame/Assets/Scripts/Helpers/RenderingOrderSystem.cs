using UnityEngine;

public class RenderingOrderSystem : MonoBehaviour
{
    private SpriteRenderer renderer;
    void Start()
    {
        var component = GetComponent<SpriteRenderer>();
        renderer = component ?? GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
    }
}
