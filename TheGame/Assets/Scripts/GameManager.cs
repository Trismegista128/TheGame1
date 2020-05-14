using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PixelsInUnit = 10;
    public SpriteRenderer PrimaryWeaponSlot;
    public SpriteRenderer SecondaryWeaponSlot;
    public Sprite ActiveWeaponSprite;
    public Sprite InactiveWeaponSprite;

    private int enemiesCount;
    private Dictionary<int, GameObject> doors;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies?.Length ?? 0;

        SetupDoors();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }

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
        SceneManager.LoadScene(0);
    }

    public void GameOver()
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
