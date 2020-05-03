using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private GameObject doorObject;
    private GameManager manager;

    private bool alreadyOpened;

    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        doorObject = this.gameObject.transform.GetChild(0).gameObject;
        SetDoorsState(areOpen: false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.EnemiesCount() <= 0 && !alreadyOpened)
        {
            SetDoorsState(areOpen: true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyOpened && collision.tag == "Player")
        {
            manager.NextLevel();
        }
    }

    private void SetDoorsState(bool areOpen)
    {
        alreadyOpened = areOpen;
        doorObject.SetActive(!areOpen);
    }
}
