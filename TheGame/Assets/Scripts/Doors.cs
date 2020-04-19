using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    public Sprite StateOpen;
    public Sprite StateClosed;

    private bool AlreadyOpened;
    private SpriteRenderer sr;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = StateClosed;
        AlreadyOpened = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(manager.EnemiesCount() <= 0 && !AlreadyOpened)
        {
            AlreadyOpened = true;
            sr.sprite = StateOpen;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(AlreadyOpened && collision.tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}
