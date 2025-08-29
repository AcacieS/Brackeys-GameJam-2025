using UnityEngine;

public class ShowDiff : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BeatGame.Current.setHard();
            Desactivate();
        }
    }
    public void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
