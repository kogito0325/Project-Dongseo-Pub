using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene1Script : MonoBehaviour
{
    public Image emptyImage; //�����̹��� : ������
    public Sprite changeSprit0;
    public Sprite changeSprit1; //�ٲ� �̹��� : ������
    public Sprite changeSprit2; // ���ΰ�

    public Text ChatText; // ���
    public Text CharacterName; //ĳ���� �̸�

    public string writerText = "";
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextPractice());
    }

    public IEnumerator NormalChat(string narrator, string narration, int Ch2)
    { 
        CharacterName.text = narrator;
        writerText = "";
        if (Ch2 == 0){ //������
            emptyImage.sprite= changeSprit0;
        }
        if (Ch2 == 1){ //������
            emptyImage.sprite = changeSprit1;
        }

        if (Ch2 == 2){//���ΰ� 
            emptyImage.sprite = changeSprit2;
        }
        for (int i = 0; i < narration.Length; i++)
        {
            writerText += narration[i];
            ChatText.text = writerText;
            yield return new WaitForSeconds(.1f);
        }

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
    }
    IEnumerator TextPractice()
    {
        string str1 = PlayerPrefs.GetString("Name1");
        string str2 = PlayerPrefs.GetString("Name2");
        yield return StartCoroutine(NormalChat(str1 + str2, "������ �ҽ�������..", 2));
        yield return StartCoroutine(NormalChat("������", "�ȳ�, " + str1 + str2 + "! ���� ������ ����?",0));
        yield return StartCoroutine(NormalChat("������", str2 + " ��¥ ���. ��ġ?",0));
        yield return StartCoroutine(NormalChat("������", "(��¥ ���� ���)",0));
        yield return StartCoroutine(NormalChat("������", "�̰� ���� �߿�!",1));
        yield return StartCoroutine(NormalChat("������", "�ϳ��� ������," + str2 + "?",1));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
