using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour
{

    public int choiceIdx; // ������ ��ư���� �ο��Ǵ� scriptIdx
    public string choiceContent;
    public ScriptReader scriptReader; // ��ũ��Ʈ ������ ��ȣ�ۿ� �ϱ� ���� ����
    public Text choiceTxt;

    public List<Dictionary<string, object>> scriptTable; // ��ũ��Ʈ ���̺�

    private void Awake()
    {
        // ���̺� �ʱ�ȭ
        scriptTable = GameManager.Instance.scriptTable;
    }
    public void StartGame()
    {   // ���� ���� ��ư - �̸� �Է� ȭ�� �ε�
        SceneManager.LoadScene("InputScene");
    }


    public void SaveName(InputField inputName)
    {   // �̸� �����ϴ� ��ư - ���� �� ��ȭ ȭ�� �ε�
        if (string.IsNullOrEmpty(inputName.text) || string.IsNullOrWhiteSpace(inputName.text))
            Debug.Log("�̸��� ����� �Է��Ͽ� �ּ���.");
        else
        {
            PlayerPrefs.SetString("name", inputName.text);
            SceneManager.LoadScene("ChatScene");
        }
    }

    public void chooseNum()
    {   // ������ ��ư - ScriptReader�� Ŭ���� ���� ����
        GameManager.Instance.UpdateIdx(choiceIdx);
        scriptReader.choosed = true;
    }
}