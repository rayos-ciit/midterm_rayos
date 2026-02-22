using UnityEngine;

public class ExplosionFX_Messy : MonoBehaviour
{
    public float duration = 1f;
    float t;

    void OnEnable()
    {
        //resets the timer every time it comes out of the pool
        t = 0f; 
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= duration)
        {
            //replaced Destroy() with ReturnExplosion()
            GameplayFactory.Instance.ReturnExplosion(this);
        }
    }
}