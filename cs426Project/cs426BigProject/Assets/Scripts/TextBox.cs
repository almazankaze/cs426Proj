using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextBox : MonoBehaviour
{
    private Text myText;
    public GameObject textBox;
    public GameObject nextText;
    public GameObject continueText;

    public string[] DialogueStrings;

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Space;

    private bool isStringBeingRevealed = false;
    private bool isDialoguePlaying = false;
    private bool isEndOfDialogue = false;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        myText.text = "";

        isDialoguePlaying = true;
        StartCoroutine(StartDialogue());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDialoguePlaying)
        {
            textBox.SetActive(false);
            nextText.SetActive(true);
        }
    }

    // start the dialogue
    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;

        // keep showing text while there is text to show
        while (currentDialogueIndex < dialogueLength || !isStringBeingRevealed)
        {
            if (!isStringBeingRevealed)
            {
                isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    isEndOfDialogue = true;
                }
            }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }
        isEndOfDialogue = false;
        isDialoguePlaying = false;
    }

    // display the text
    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        myText.text = "";

        // while there is text in the array
        while (currentCharacterIndex < stringLength)
        {
            myText.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;


            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else
            {
                continueText.SetActive(true);
                break;
            }
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        isStringBeingRevealed = false;
        continueText.SetActive(false);
        myText.text = "";
    }
}
