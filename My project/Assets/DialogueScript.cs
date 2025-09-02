using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string dialogue;
    public AudioClip audioDialogue;

    public string getDialogue()
    {
        return dialogue;
    }
    public AudioClip getAudioDialogue()
    {
        return audioDialogue;
    }
}
[System.Serializable]
public class DialogueIndex
{
    public Dialogue[] Dialogues;
    public string getDialogue(int index)
    {
        return Dialogues[index].getDialogue();
    }
    public int getDialoguesSize()
    {
        return Dialogues.Length;
    }
    public AudioClip getAudioDialogue(int index)
    {
        return Dialogues[index].getAudioDialogue();
    }
}

// Every time I look at this old building, I remember the past. Back then, I was someone — the brightest star on the stage. All eyes were on me.

// But those days are long gone. The stage is falling apart now... just like I am. Broken. Forgotten.

// And yet, here you are.

// What brings you to my door, dear patient? Pain? Confusion? A need for healing?

// Whatever it is... I promise you this — you will be cured.

// No matter what it takes.

public class DialogueScript : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private GameObject button;
    [SerializeField] private DialogueIndex[] dialogues;
    [SerializeField] private GameObject NPC_dialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float wordSpeed = 0.05f;  // Default speed

    private bool isFinish = false;
    private AudioSource audioSource;
    private int i_text = 0;
    private int i_dialogue = 0;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        NPC_dialogue.SetActive(true);
        dialogueText.text = string.Empty;
        i_text = 0;
        i_dialogue = 0;
        StartDialogue();
    }
    public void NextDialogue()
    {
        if (i_dialogue + 1 >= dialogues.Length) return;

        dialogueText.text = string.Empty;
        i_dialogue++;
        i_text = 0;

        NPC_dialogue.SetActive(true);
        StartDialogue();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextTxt();
        }

    }
    private void NextTxt()
    {


        string fullText = dialogues[i_dialogue].getDialogue(i_text);

        if (dialogueText.text == fullText)
        {

            Debug.Log("Next Line");
            NextLine();

        }
        else
        {
            // Skip typing and show full line immediately
            Debug.Log("Skip show full");
            audioSource.Stop();
            StopAllCoroutines();
            dialogueText.text = fullText;


        }

    }



    private IEnumerator Typing()
    {
        dialogueText.text = "";

        foreach (char letter in dialogues[i_dialogue].getDialogue(i_text).ToCharArray())
        {

            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }


    private void NextLine()
    {
        if (i_text < dialogues[i_dialogue].getDialoguesSize() - 1)
        {
            i_text++;
            StopAllCoroutines();
            PlayVoiceLine();
            StartCoroutine(Typing());
        }
        else
        {
            NPC_dialogue.SetActive(false);
            isFinish = true;
            zeroText();
        }
    }

    public void zeroText()
    {
        button.SetActive(true);
        dialogueText.text = "";
        i_text = 0;
    }

    private void StartDialogue()
    {
        i_text = 0;
        PlayVoiceLine();
        StartCoroutine(Typing());
    }
    private void PlayVoiceLine()
    {
        audioSource.Stop();
        audioSource.clip = dialogues[i_dialogue].getAudioDialogue(i_text);
        audioSource.Play();
    }


}
