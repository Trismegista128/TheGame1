using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animator doorAnimator;
    private GameManager manager;
    private SpriteRenderer renderer;
    private bool alreadyOpened;

    // Start is called before the first frame update
    void Start()
    {
        var managerObject = GameObject.FindGameObjectWithTag("GameController");
        manager = managerObject.GetComponent<GameManager>();

        doorAnimator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        renderer.enabled = true;
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
        doorAnimator.SetBool("Activate", areOpen);
    }

    //private IEnumerator PlayAndDisappear(bool areOpen)
    //{
    //    alreadyOpened = areOpen;
    //    doorAnimator.Play("Activate");
    //    yield return new WaitForSeconds(doorAnimator.);
    //    doorObject.SetActive(!areOpen);
    //    //doorAnimator.SetBool("Activate", areOpen);
    //}

    public void AnimationEnd()
    {
        renderer.enabled = false;
    }
}
