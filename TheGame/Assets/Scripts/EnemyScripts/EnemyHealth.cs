using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float HP;
    public GameManager manager;
    public EnemyDropItem dropper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isBullet = collision.GetComponent<Bullet>();
        if(isBullet != null)
        {
            //Destroy bullet
            //Warning, this may be already too late and bullet could hit already another target.
            Destroy(collision.transform.gameObject);

            //figure out what damage.
            LoseLife(1f);
        }
    }

    private void LoseLife(float damage)
    {
        if(HP > damage)
        {
            HP -= damage;
        }
        else
        {
            dropper.Drop();
            manager.EnemyKilled();
            Destroy(this.gameObject);
        }
    }
}
