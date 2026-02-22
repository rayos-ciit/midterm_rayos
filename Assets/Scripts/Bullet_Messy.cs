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
        // 1. Ignore if already inactive (prevents crashing)
        if (!gameObject.activeInHierarchy) return;

        // 2. Ignore the player so we don't shoot ourselves!
        if (c.gameObject.CompareTag("Player")) return;

        // 3. Ignore other bullets to prevent mid-air collisions!
        if (c.gameObject.GetComponent<Bullet_Messy>() != null) return;

        // 4. Centralized creation and returning
        GameplayFactory.Instance.SpawnExplosion(transform.position);
        GameplayFactory.Instance.ReturnBullet(this);
    }
}