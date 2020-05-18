using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;

    /// <summary>
    /// Commands should be ordered from top to down as presented in the UI
    /// </summary>
    public TextMeshProUGUI[] CommandsUIText;
    public Color HighlightColor;
    public Color NormalColor;

    public static bool GameIsPaused;

    private GameManager manager;
    private int selectedCommandIndex;

    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Input W on pause found");
                if (selectedCommandIndex > 0)
                {
                    selectedCommandIndex--;
                    UpdateCommandSelection();
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Input S on pause found");
                if (selectedCommandIndex < CommandsUIText.Length - 1)
                {
                    selectedCommandIndex++;
                    UpdateCommandSelection();
                }
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Input Space or Enter on pause found");
                ExecuteCommand();
            }
        }
    }

    private void UpdateCommandSelection()
    {
        Debug.Log($"Update called, index is {selectedCommandIndex}");

        foreach (var uiComponent in CommandsUIText)
        {
            uiComponent.color = NormalColor;
        }

        CommandsUIText[selectedCommandIndex].color = HighlightColor;
    }

    private void ExecuteCommand()
    {
        Debug.Log($"Execute called, index is {selectedCommandIndex}");
        if (selectedCommandIndex < 0 || selectedCommandIndex > 2)
            throw new System.ArgumentOutOfRangeException($"{nameof(selectedCommandIndex)}", $"The value of index is out of supported range. Value of {(selectedCommandIndex)} in a range (0-2)");

        if(selectedCommandIndex == 0) Resume();
        if(selectedCommandIndex == 1) Restart();
        if(selectedCommandIndex == 2) Quit();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        selectedCommandIndex = 0;
        UpdateCommandSelection();
        GameIsPaused = true;
        PauseMenuUI.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
    }

    private void Restart()
    {
        manager.Restart();
    }

    private void Quit()
    {
        manager.QuitGame();
    }
}
