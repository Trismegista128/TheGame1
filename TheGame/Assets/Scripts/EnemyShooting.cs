using Assets.Scripts.Helpers;
using UnityEngine;

public class EnemyShooting : EnemyMovement
{
    public float FireRate;
    public GameObject BulletPrefab;

    private ShootingHelper shootingHelper;

    void Start()
    {
        base.Start();

        shootingHelper = new ShootingHelper(Tr, BulletPrefab, FireRate);
    }
    void FixedUpdate()
    {
        base.FixedUpdate();

        var headingVector = Target.position - Tr.position;
        var direction = headingVector / headingVector.magnitude;
        shootingHelper.Fire(direction, direction);
    }
}
