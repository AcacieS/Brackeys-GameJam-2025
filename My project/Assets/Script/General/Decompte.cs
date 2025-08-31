using UnityEngine;
using System.Collections;
using TMPro;

public class Decompte : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText; // UI text to show countdown
    [SerializeField] private float countdownTime = 3f; // seconds
    //[SerializeField] private MonoBehaviour[] scriptsToEnable; // scripts to enable after countdown
    [SerializeField] private GameObject[] scriptsToEnable;
    [SerializeField] private GameObject[] toDisable;

    private void Start()
    {
        // Disable all scripts first
        foreach (var s in scriptsToEnable)
        {
            //s.enabled = false;
            s.SetActive(false);
        }

        // Start the countdown
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(timeLeft).ToString();

            Debug.Log("countdownText.text" + countdownText.text);

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        // Enable all the other scripts
        foreach (var s in scriptsToEnable)
        {
            s.SetActive(true);
            //s.enabled = true;
        }
        foreach (var s in toDisable)
        {
            s.SetActive(false);
            //s.enabled = true;
        }


        if (countdownText != null)
            countdownText.gameObject.SetActive(false); // hide after "GO!"

        
    }
}