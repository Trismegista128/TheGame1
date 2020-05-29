using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class GameManager : MonoBehaviour
{
    public int PixelsInUnit = 10;
    public Image PrimaryWeaponSlot;
    public Image SecondaryWeaponSlot;
    public Sprite ActiveWeaponSprite;
    public Sprite InactiveWeaponSprite;
    public int CurrentSceneIndex;
    public EndGameMenu endGameMenu;

    private int enemiesCount;
    private Dictionary<int, GameObject> doors;
    private int maxLevels;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        maxLevels = SceneManager.sceneCountInBuildSettings;

        if (CurrentSceneIndex != 0)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesCount = enemies?.Length ?? 0;

            SetupDoors();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        IsPaused = false;
        IsGameOver = false;
    }

    public bool IsGamePlay => CurrentSceneIndex != 0;
    public bool IsPaused;
    public bool IsGameOver;

    public int EnemiesCount()
    {
        return enemiesCount;
    }

    public void EnemyKilled()
    {
        enemiesCount--;
    }

    public void WeaponSwitch(bool isPrimary)
    {
        PrimaryWeaponSlot.sprite = isPrimary ? ActiveWeaponSprite : InactiveWeaponSprite;
        SecondaryWeaponSlot.sprite = isPrimary ? InactiveWeaponSprite : ActiveWeaponSprite;
    }

    public void NextLevel()
    {
        if (maxLevels > CurrentSceneIndex + 1)
        {
            CurrentSceneIndex++;
            SceneManager.LoadScene(CurrentSceneIndex);
        }
        else
        {
            //should be end game.
            GameOver();
        }
    }

    public void BetweenDeadAndGameOver()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var enemiesAi = enemies.Select(x => x.gameObject.GetComponent<AIPath>()).ToList();
        IsGameOver = true;

        foreach (var enemy in enemiesAi)
        {
            enemy.canMove = false;
        }
    }
    public void GameOver()
    {
        //Load title screen
        endGameMenu.ShowGameOverMenu();
    }

    public void Restart()
    {
        //To change to load current scene
        //To restart with the stats user had at the beggining of this level?
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    private void SetupDoors()
    {
        doors = new Dictionary<int, GameObject>();
        var doorsList = GameObject.FindGameObjectsWithTag("Door");

        for (var i = 0; i < doorsList.Length; i++)
        {
            doors.Add(i, doorsList[i]);
            doors[i].SetActive(false);
        }

        var activeDoorsAmount = Random.Range(1, 4);
        Debug.Log($"Random amount {activeDoorsAmount}");
        for (var i = 0; i < activeDoorsAmount; i++)
        {
            var doorsToActivate = Random.Range(0, 3);
            Debug.Log($"Doors to be activated: {doorsToActivate}");
            doors[doorsToActivate].SetActive(true);
        }
    }
}
