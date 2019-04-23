using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject textBox;
    public GameObject nextText;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {

            SceneManager.LoadScene("SampleScene");
            textBox.SetActive(false);
            nextText.SetActive(false);
        }
    }
}
