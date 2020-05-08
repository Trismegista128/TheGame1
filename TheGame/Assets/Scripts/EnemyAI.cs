using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;
    public Transform EnemyGFX;
    public float Speed = 300f;
    public float NextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, Target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        var direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        var force = direction * Speed * Time.deltaTime;

        rb.AddForce(force);

        //var movement = Vector2.MoveTowards(rb.position, path.vectorPath[currentWaypoint], Speed * Time.deltaTime);
        //rb.position = movement;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < NextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            EnemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            EnemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
