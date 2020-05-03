using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class ShootingHelper : MonoBehaviour
    {
        private GameObject bulletPrefab;
        private Transform tr;
        private float fireRate;
        private float lastShoot;
        
        public ShootingHelper(Transform tr, GameObject bulletPrefab, float fireRate)
        {
            this.fireRate = fireRate;
            this.bulletPrefab = bulletPrefab;
            this.tr = tr;
        }

        public void Fire(Vector3 shootDirection, Vector2 currentMovement)
        {
            if (Time.time > fireRate + lastShoot)
            {
                //deal with the problem that there was no movement yet.
                if (shootDirection == Vector3.zero) shootDirection = new Vector3(-1, 0, 0);

                //deal with the problem that there is no movement now and player was moving diagonaly.
                if (shootDirection.x != 0 && shootDirection.y != 0 && currentMovement == Vector2.zero)
                    shootDirection.y = 0;

                //deal with the problem that bullet is instantiated in wrong place of sprite;
                var positionOfBullet = CalculateBulletStartingPoint(shootDirection);

                var bullet = (Instantiate(bulletPrefab, tr.position + positionOfBullet, new Quaternion())) as GameObject;
                bullet.GetComponent<Bullet>().Initialize(shootDirection);
                lastShoot = Time.time;
            }
        }

        private Vector3 CalculateBulletStartingPoint(Vector3 shootDirection)
        {
            var positionOfBullet = Vector3.zero;
            //Facing left
            if (shootDirection.x < 0)
            {
                //Shooting left-down
                if (shootDirection.y < 0)
                {
                    positionOfBullet.x = 0;
                    positionOfBullet.y = 1;
                }
                //Shooting left-up
                else if (shootDirection.y > 0)
                {
                    positionOfBullet.x = 0;
                    positionOfBullet.y = 1;
                }
                //Shooting left
                else
                {
                    positionOfBullet.x = 0;
                    positionOfBullet.y = 1;
                }
            }
            //Facing right
            else if (shootDirection.x > 0)
            {
                //Shooting right-down
                if (shootDirection.y < 0)
                {
                    positionOfBullet.x = 1.33f;
                    positionOfBullet.y = 1;
                }
                //Shooting right-up
                else if (shootDirection.y > 0)
                {
                    positionOfBullet.x = 1.33f;
                    positionOfBullet.y = 1;
                }
                //Shooting right
                else
                {
                    positionOfBullet.x = 1.33f;
                    positionOfBullet.y = 1;
                }
            }
            //No horizontal facing
            else
            {
                //Shooting Down
                if (shootDirection.y < 0)
                {
                    positionOfBullet.x = 0.33f;
                    positionOfBullet.y = 1;
                }

                //Shooting Up
                else
                {
                    positionOfBullet.x = 1;
                    positionOfBullet.y = 2;
                }
            }

            return positionOfBullet;
        }
    }
}
