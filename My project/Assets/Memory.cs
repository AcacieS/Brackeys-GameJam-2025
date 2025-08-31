using UnityEngine;
using UnityEngine.SceneManagement;

public class Memory : MonoBehaviour
{
    [Tooltip(" GoodMemory = + scoreValue, BadMemory = - scoreMemory")]
    public int scoreValue = 1;  
    public float destroyY = -12f;
    internal bool isGood;

    void Update()
    {
        if (transform.position.y < destroyY)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddScore(scoreValue);

            Destroy(gameObject);
        }
    }
}
