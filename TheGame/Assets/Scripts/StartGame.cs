using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    public GameManager GameManager;
    public TextMeshProUGUI TextObject;
    public float BlinkDurationInSeconds;

    private float nextChange;

    private void Start()
    {
        nextChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager.NextLevel();
        }

        Blink();
    }

    private void Blink()
    {
        if (Time.time > nextChange && TextObject != null)
        {
            var currentColor = TextObject.color;
            TextObject.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a == 0 ? 255 : 0);
            nextChange = Time.time + BlinkDurationInSeconds;
        }
    }
}
