using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    public GameObject button;

    public Text dialogueText;
    private Queue<string> sentences;

    public Dialogue dialogue;
    // Звуки
    [FMODUnity.EventRef]
    public string eventClickPath;

    private FMOD.Studio.EventInstance eventClick;
    // Update is called once per frame
    void Start()
    {
        eventClick = FMODUnity.RuntimeManager.CreateInstance(eventClickPath);

        sentences = new Queue<string>();
        StartDialogue(dialogue);
    }

    void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {

        dialogueText.text = "";

        yield return new WaitForSeconds(1f);
        foreach (char letter in sentence.ToCharArray())
        {
            eventClick.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            if(letter != '%')
            {
                dialogueText.text += letter;
            }
            if (letter != ' ' && letter != '%' && letter != '\n' && letter != '\r')
            {
                eventClick.start();
                
            }

            if (letter == '%')
            {
                yield return new WaitForSeconds(1f);
            }
            if (letter == ',')
            {
                yield return new WaitForSeconds(.075f);
            }
            if (letter == '.')
            {
                yield return new WaitForSeconds(.1f);
            }
                            
             yield return new WaitForSeconds(0.05f);

        }

        button.SetActive(true);
    }


    public void changeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
