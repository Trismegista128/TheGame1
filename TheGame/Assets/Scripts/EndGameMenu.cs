using UnityEngine;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    public GameObject EndGameMenuUI;
    public GameManager manager;

    /// <summary>
    /// Commands should be ordered from top to down as presented in the UI
    /// </summary>
    public TextMeshProUGUI[] CommandsUIText;
    public Color NormalColor;
    public Color HighlightColor;
    
    private int selectedCommandIndex;
    private bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (selectedCommandIndex > 0)
                {
                    selectedCommandIndex--;
                    UpdateCommandSelection();
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (selectedCommandIndex < CommandsUIText.Length - 1)
                {
                    selectedCommandIndex++;
                    UpdateCommandSelection();
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteCommand();
            }
        }
    }

    public void ShowGameOverMenu()
    {
        ShowMe();
    }

    private void UpdateCommandSelection()
    {
        foreach (var uiComponent in CommandsUIText)
        {
            uiComponent.color = NormalColor;
        }

        CommandsUIText[selectedCommandIndex].color = HighlightColor;
    }
    private void ExecuteCommand()
    {
        if (selectedCommandIndex < 0 || selectedCommandIndex >= CommandsUIText.Length)
            throw new System.ArgumentOutOfRangeException($"{nameof(selectedCommandIndex)}", $"The value of index is out of supported range. Value of {(selectedCommandIndex)} in a range (0-{CommandsUIText.Length})");

        if (selectedCommandIndex == 0) manager.Restart();
        if (selectedCommandIndex == 1) manager.QuitGame();
    }

    private void HideMe()
    {
        EndGameMenuUI.SetActive(false);
    }

    private void ShowMe()
    {
        selectedCommandIndex = 0;
        EndGameMenuUI.SetActive(true);
        UpdateCommandSelection();
    }
}
