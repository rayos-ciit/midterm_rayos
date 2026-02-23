using UnityEngine;

public class Enemy_Messy : MonoBehaviour
{
    public float hp = 1f;
    public float speed = 3f;

    Transform player;

    void OnEnable()
    {
        hp = 1f;
        transform.localScale = Vector3.one;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.position);

        if (transform.position.y < -50f) transform.position = Vector3.zero;
    }

    void OnCollisionEnter(Collision c)
    {
        //hit the player trigger game over
        if (c.gameObject.CompareTag("Player"))
        {
            if (GameController_Messy.I != null)
            {
                GameController_Messy.I.GameOver(); 
            }
        }
    }

    //The bullet will call this directly to guarantee damage applies!
    public void Hit()
    {
        hp -= 1f;
        if (hp <= 0)
        {
            GameplayFactory.Instance.SpawnExplosion(transform.position);
            GameplayFactory.Instance.ReturnEnemy(this);
        }
    }
}