using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private BubbleSO[] GoodBubblesSO;
    [SerializeField] private BubbleSO BadBubblesSO;
    [Header("Target")]
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform targetGood;
    [SerializeField] private Transform targetBad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnBubbleObj()
    {
        int randomGB = Random.Range(0, 2);
        BubbleSO currentBubble;
        if (randomGB == 0)
        {
            currentBubble = BadBubblesSO;
        }
        else
        {
            int randomGoodBubble = Random.Range(0, GoodBubblesSO.Length);
            currentBubble = GoodBubblesSO[randomGoodBubble];
        }

        GameObject bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        bubble.GetComponent<BubbleScript>().SetBubble(currentBubble);
        bubble.GetComponent<BubbleScript>().SetTargets(targetPos, targetGood, targetBad);
    }
}
