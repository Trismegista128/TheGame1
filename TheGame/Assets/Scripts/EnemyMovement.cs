using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    public int Lifes;

    [HideInInspector]
    public Transform Target;

    [HideInInspector]
    public Transform Tr;

    [HideInInspector]
    private GameManager Manager;
    
    // Start is called before the first frame update
    public void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        var movement = Vector2.MoveTowards(Tr.position, Target.position, Speed * Time.deltaTime).normalized;
        Tr.position = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            LoseLife();
            Destroy(collision.transform.gameObject);
        }
    }

    private void LoseLife()
    {
        if (Lifes > 1)
            Lifes--;
        else
            OnKilled();
    }

    private void OnKilled()
    {
        Manager.EnemyKilled();
        Destroy(this.gameObject);
    }
}
