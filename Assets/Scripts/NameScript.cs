using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputName;

    public void SaveName()
    {
        if (string.IsNullOrEmpty(inputName.text) || string.IsNullOrWhiteSpace(inputName.text))
            Debug.Log("�̸��� ����� �Է��Ͽ� �ּ���.");
        else
        {
            PlayerPrefs.SetString("name", inputName.text); //��
            SceneManager.LoadScene("ChatScene");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            SaveName();
    }
}
