using UnityEngine;

public class ShootCommand : ICommand
{
    private Transform firePoint;
    private float bulletSpeed;

    // Constructor to pass in the references it needs to shoot
    public ShootCommand(Transform firePoint, float bulletSpeed)
    {
        this.firePoint = firePoint;
        this.bulletSpeed = bulletSpeed;
    }

    public void Execute()
    {
        // REFACTOR: Uses our new Factory instead of Instantiate!
        Bullet_Messy bullet = GameplayFactory.Instance.SpawnBullet(firePoint.position, firePoint.rotation);
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}