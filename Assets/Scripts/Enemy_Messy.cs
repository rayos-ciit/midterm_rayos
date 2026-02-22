using UnityEngine;

public class Enemy_Messy : MonoBehaviour
{
    public float hp = 1f;
    public float speed = 3f;

    Transform player;

    void OnEnable()
    {
        // REFACTOR: Reset stats when pulling from the pool
        hp = 1f;
        transform.localScale = Vector3.one;

        // Reset reference in case player changes or to ensure it's found on reuse
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Note: Removed the tight coupling to GameController's magic numbers!
        // The State Pattern will handle freezing/pausing the game later.

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);

        if (transform.position.y < -50f) transform.position = Vector3.zero;
    }

    void OnCollisionEnter(Collision c)
    {
        Bullet_Messy b = c.gameObject.GetComponent<Bullet_Messy>();
        if (b != null)
        {
            hp -= 1f;
            if (hp <= 0)
            {
                // REFACTOR: Use Factory for explosion and returning to pool
                GameplayFactory.Instance.SpawnExplosion(transform.position);
                GameplayFactory.Instance.ReturnEnemy(this);
            }
        }
    }
}