using Assets.Scripts.Helpers;
using UnityEngine;

public class EnemyShooting : EnemyMovement
{
    public float FireRate;
    public GameObject BulletPrefab;

    private ShootingHelper shootingHelper;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    void Start()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    {
        base.Start();

        shootingHelper = new ShootingHelper(Tr, BulletPrefab, FireRate);
    }
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    void FixedUpdate()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    {
        base.FixedUpdate();

        var headingVector = Target.position - Tr.position;
        var direction = headingVector / headingVector.magnitude;
        shootingHelper.Fire(direction, direction);
    }
}
