using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour
{

    public int choiceIdx; // ������ ��ư���� �ο��Ǵ� scriptIdx
    public ScriptReader scriptReader; // ��ũ��Ʈ ������ ��ȣ�ۿ� �ϱ� ���� ����

    public List<Dictionary<string, object>> scriptTable;

    private void Awake()
    {
        // ���̺� �ʱ�ȭ
        scriptTable = GameManager.Instance.scriptTable;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("InputScene");
    }


    public void SaveName(InputField inputName)
    {
        if (string.IsNullOrEmpty(inputName.text) || string.IsNullOrWhiteSpace(inputName.text))
            Debug.Log("�̸��� ����� �Է��Ͽ� �ּ���.");
        else
        {
            PlayerPrefs.SetString("name", inputName.text);
            SceneManager.LoadScene("ChatScene");
        }
    }

    public void chooseNum()
    {
        GameManager.Instance.UpdateIdx(choiceIdx);
        scriptReader.choosed = true;
    }
}