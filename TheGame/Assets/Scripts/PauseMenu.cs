﻿using TMPro;
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
        if (!manager.IsGamePlay) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (manager.IsPaused)
                Resume();
            else
                Pause();
        }

        if (manager.IsPaused)
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

    private void UpdateCommandSelection()
    {
        foreach (var uiComponent in CommandsUIText)
        {
            uiComponent.color = NormalColor;
        }

        CommandsUIText[selectedCommandIndex].color = HighlightColor; ;
    }

    private void ExecuteCommand()
    {
        if (selectedCommandIndex < 0 || selectedCommandIndex >= CommandsUIText.Length)
            throw new System.ArgumentOutOfRangeException($"{nameof(selectedCommandIndex)}", $"The value of index is out of supported range. Value of {(selectedCommandIndex)} in a range (0-{CommandsUIText.Length})");

        if(selectedCommandIndex == 0) Resume();
        if(selectedCommandIndex == 1) manager.Restart();
        if(selectedCommandIndex == 2) manager.QuitGame();
    }

    private void Pause()
    {
        Time.timeScale = 0;
        selectedCommandIndex = 0;
        UpdateCommandSelection();
        manager.IsPaused = true;
        PauseMenuUI.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        manager.IsPaused = false;
        PauseMenuUI.SetActive(false);
    }
}
