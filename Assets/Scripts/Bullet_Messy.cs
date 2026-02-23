using UnityEngine;

public class Bullet_Messy : MonoBehaviour
{
    float life = 2.5f;
    float t;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        t = 0f; 
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= life) 
        {
            GameplayFactory.Instance.ReturnBullet(this);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (!gameObject.activeInHierarchy) return;
        if (c.gameObject.CompareTag("Player")) return;
        if (c.gameObject.GetComponent<Bullet_Messy>() != null) return;

        // FIX: Tell the enemy to take damage BEFORE the bullet turns off!
        Enemy_Messy enemy = c.gameObject.GetComponent<Enemy_Messy>();
        if (enemy != null)
        {
            enemy.Hit();
        }

        GameplayFactory.Instance.SpawnExplosion(transform.position);
        GameplayFactory.Instance.ReturnBullet(this);
    }
}