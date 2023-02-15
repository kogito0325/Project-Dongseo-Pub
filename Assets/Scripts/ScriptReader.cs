using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditorInternal;

public class ScriptReader : MonoBehaviour
{
    public Image emptyImage; // ĳ���� �̹��� Ȧ��
    public Sprite[] characterImages; // ĳ���� �̹��� ����Ʈ
    public Button[] choiceButtons; // ������ UI ����Ʈ

    public Text ChatText; // ���
    public Text CharacterName; // ĳ���� �̸�

    public string writerText = ""; // ��µǰ� �ִ� ���ڿ�
    public bool choosed = false; // ������ �������� üũ�ϴ� �ο� ����

    List<Dictionary<string, object>> scriptTable; // ��� ���̺�
    List<Dictionary<string, object>> chapterTable; // é�� ���̺�
    List<Dictionary<string, object>> nameTable; // ĳ���� �̸� ���̺�

    // Start is called before the first frame update

    private void Awake()
    {
        // ���̺� �ʱ�ȭ
        scriptTable = GameManager.Instance.scriptTable;
        chapterTable = GameManager.Instance.chapterTable;
        nameTable = GameManager.Instance.nameTable;
    }
    void Start()
    {
        // ���� �Ŵ����� ���� �ε��� ����
        // ���μ��� ����
        StartCoroutine(Process(GameManager.Instance.contextIdx));
    }

    public IEnumerator TextPrint(string narrator, string narration, int chr)
    { // ������ ĳ���� ��縦 ���ڿ��� �޾� ����ϴ� �ڷ�ƾ �Լ�

        float textSpeed = 0.02f; // �ؽ�Ʈ ��� �ӵ�
        CharacterName.text = narrator;
        writerText = "";
        emptyImage.sprite = characterImages[chr];

        for (int i = 0; i < narration.Length; i++)
        {
            writerText += narration[i];
            ChatText.text = writerText;
            yield return new WaitForSeconds(textSpeed);
        }

        while (true)
        {
            if (Input.GetMouseButtonDown(0)) // ��簡 �� ������ ���콺 �Է� ���
                break;
            yield return null;
        }
    }
    public IEnumerator NormalChat(int startIdx, int endIdx)
    { // �Ϲ����� ��縦 ����ϴ� �ڷ�ƾ �Լ�
        string gottenName = PlayerPrefs.GetString("name");
        string name;
        string script;

        for (int i = startIdx; i <= endIdx; i++)
        {
            int nameIdx = int.Parse(scriptTable[i]["CHARACTER"].ToString());
            name = nameTable[nameIdx]["NAME"].ToString();
            script = scriptTable[i]["CONTENT"].ToString();
            if (name.Equals("���ΰ�"))
                name = gottenName;
            yield return StartCoroutine(TextPrint(name, script, nameIdx)); // ��簡 �� ��µ� ������ ���
        }
    }

    public IEnumerator Process(int contextIdx)
    { // �ε����� �޾� CONTENT�� ���� ��縦 ó���ϴ� �ڷ�ƾ �Լ�
        int startIdx = int.Parse(scriptTable[contextIdx]["START_POINT"].ToString());
        int endIdx = int.Parse(scriptTable[contextIdx]["END_POINT"].ToString());
        string process = scriptTable[contextIdx]["CONTENT"].ToString();
        switch (process)
        {
            case "~��ȭ":
                yield return StartCoroutine(NormalChatProcess(startIdx, endIdx)); // �Ϲ� ��ȭ�� ���� ������ ���
                break;
            case "~������":
                yield return StartCoroutine(ChoiceProcess(startIdx, endIdx)); // ���� �� ���� ������ �� ���� ������ ���
                break;
            case "~é�ͳ�":
                yield return StartCoroutine(EndProcess(startIdx, endIdx)); // é�͸� ������ ó�� ���
                break;
        }
        contextIdx = GameManager.Instance.contextIdx;

        yield return StartCoroutine(Process(contextIdx)); // ���� CONTENT ����
    }

    IEnumerator NormalChatProcess(int startIdx, int endIdx)
    {
        yield return StartCoroutine(NormalChat(startIdx, endIdx)); // �Ϲ� ��ȭ ó�� ���
        GameManager.Instance.UpdateIdx(++endIdx);
    }

    IEnumerator ChoiceProcess(int startIdx, int endIdx)
    {
        int tempEndIdx = int.Parse(scriptTable[endIdx]["END_POINT"].ToString());
        for (int i = startIdx; i <= endIdx; i++)
        {
            ActivateButtons(i - startIdx, i); // ������ Ȱ��ȭ
        }
        
        yield return StartCoroutine(GetNumber()); // ������ �� ������ ���
        startIdx = int.Parse(scriptTable[GameManager.Instance.contextIdx]["START_POINT"].ToString());
        endIdx = int.Parse(scriptTable[GameManager.Instance.contextIdx]["END_POINT"].ToString());
        yield return StartCoroutine(NormalChat(startIdx, endIdx)); // �� �������� ���� ��� ���
        GameManager.Instance.UpdateIdx(++tempEndIdx);
    }

    IEnumerator EndProcess(int startIdx, int endIdx)
    {
        GameManager.Instance.UpdateIdx(++endIdx);
        GameManager.Instance.RestartGame();
        yield break;
    }

    void ActivateButtons(int idx, int choiceIdx)
    {
        choiceButtons[idx].gameObject.SetActive(true);
        choiceButtons[idx].GetComponent<BtnManager>().choiceIdx = choiceIdx;
    }

    IEnumerator GetNumber()
    {
        while (!choosed) // BtnManager.ChooseNum()
        {
            yield return null;
        }
        choosed = false;
        foreach (var button in choiceButtons)
            button.gameObject.SetActive(false); // ������ ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
