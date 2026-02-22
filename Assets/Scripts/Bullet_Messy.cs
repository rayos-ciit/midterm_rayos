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
        t = 0f; // Reset life timer

        // REFACTOR: Reset Rigidbody velocity so old forces don't carry over
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
        // REFACTOR: Centralized creation through the Factory
        GameplayFactory.Instance.SpawnExplosion(transform.position);
        
        // REFACTOR: Replaced Destroy() with ReturnBullet()
        GameplayFactory.Instance.ReturnBullet(this);
    }
}