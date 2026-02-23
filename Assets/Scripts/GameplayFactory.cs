using UnityEngine;
using UnityEngine.Pool; //using unity's built-in object pooling

public class GameplayFactory : MonoBehaviour
{
    //singleton instance
    public static GameplayFactory Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private Bullet_Messy bulletPrefab;
    [SerializeField] private Enemy_Messy enemyPrefab;
    [SerializeField] private ExplosionFX_Messy explosionPrefab;

    //object pools
    private IObjectPool<Bullet_Messy> bulletPool;
    private IObjectPool<Enemy_Messy> enemyPool;
    private IObjectPool<ExplosionFX_Messy> explosionPool;

    private void Awake()
    {
        //setup for singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePools();
    }

    private void InitializePools()
    {
        // Bullet Pool
        bulletPool = new ObjectPool<Bullet_Messy>(
            createFunc: () => Instantiate(bulletPrefab),
            actionOnGet: (bullet) => bullet.gameObject.SetActive(true),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            collectionCheck: false,
            defaultCapacity: 20,
            maxSize: 100
        );

        // Enemy Pool
        enemyPool = new ObjectPool<Enemy_Messy>(
            createFunc: () => Instantiate(enemyPrefab),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: false,
            defaultCapacity: 15,
            maxSize: 50
        );

        // Explosion Pool
        explosionPool = new ObjectPool<ExplosionFX_Messy>(
            createFunc: () => Instantiate(explosionPrefab),
            actionOnGet: (fx) => fx.gameObject.SetActive(true),
            actionOnRelease: (fx) => fx.gameObject.SetActive(false),
            actionOnDestroy: (fx) => Destroy(fx.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 30
        );
    }

    public Bullet_Messy SpawnBullet(Vector3 position, Quaternion rotation)
    {
        Bullet_Messy bullet = bulletPool.Get();
        bullet.transform.SetPositionAndRotation(position, rotation);
        return bullet;
    }

    public Enemy_Messy SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy_Messy enemy = enemyPool.Get();
        enemy.transform.SetPositionAndRotation(position, rotation);
        return enemy;
    }

    public ExplosionFX_Messy SpawnExplosion(Vector3 position)
    {
        ExplosionFX_Messy fx = explosionPool.Get();
        fx.transform.position = position;
        return fx;
    }
    
    // This allows us to replace Destroy() with Release()
    //using release instead of destroy 

    public void ReturnBullet(Bullet_Messy bullet) => bulletPool.Release(bullet);
    public void ReturnEnemy(Enemy_Messy enemy) => enemyPool.Release(enemy);
    public void ReturnExplosion(ExplosionFX_Messy fx) => explosionPool.Release(fx);
}