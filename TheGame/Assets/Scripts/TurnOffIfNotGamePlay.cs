using Assets.Scripts.Helpers;
using UnityEngine;

public class TurnOffIfNotGamePlay : MonoBehaviour
{

    public UIType TypeOfUI;
    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        var manager = managerObject.GetComponent<GameManager>();

        if (!manager.IsGamePlay)
            gameObject.SetActive(false);

        if (manager.IsGamePlay)
        {
            switch (TypeOfUI)
            {
                case UIType.Pause:
                case UIType.GameOver:
                    gameObject.SetActive(false);
                    break;
                case UIType.GameUI:
                    gameObject.SetActive(true);
                    break;
            }
        }
    }
}
