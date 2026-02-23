using UnityEngine;

public class EnemySpawner_Messy : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float spawnInterval = 0.6f;
    private float spawnTimer;

    //using tickspawner instead of update since the system is mostly physics
    public void TickSpawner()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            int i = Random.Range(0, spawnPoints.Length);
            Transform p = spawnPoints[i];

            // Uses the Factory instead of Instantiate!
            Enemy_Messy e = GameplayFactory.Instance.SpawnEnemy(p.position, p.rotation);
            e.transform.localScale = Vector3.one * Random.Range(0.7f, 1.4f);
        }
    }
}