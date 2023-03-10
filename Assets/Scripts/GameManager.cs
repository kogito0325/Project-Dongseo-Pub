using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //게임매니저의 인스턴스를 담는 전역변수(static 변수이지만 이해하기 쉽게 전역변수라고 하겠다).
    //이 게임 내에서 게임매니저 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것이다.
    //보안을 위해 private으로.
    private static GameManager instance = null;

    public List<Dictionary<string, object>> scriptTable;
    public List<Dictionary<string, object>> chapterTable;
    public List<Dictionary<string, object>> nameTable;

    public int contextIdx = 0;
    public int month = 3;
    public int loveSummer = 0;
    public int loveFall = 0;
    public int loveWinter = 0;
    public int money = 0;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameMgr이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameMgr)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        InitGame();
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void UpdateIdx(int idx)
    {
        contextIdx = idx;
        Debug.Log("idx updated -> " + contextIdx.ToString());
    }

    public void InitGame()
    {
        //contextIdx = PlayerPrefs.GetInt("contextIdx", 0); <- 디버그 해야돼서 일단 매 시작 때마다 초기화 해줌
        //month = PlayerPrefs.GetInt("month", 3);
        /*loveSummer = PlayerPrefs.GetInt("loveSummer", 0);
        loveFall = PlayerPrefs.GetInt("loveFall", 0);
        loveWinter = PlayerPrefs.GetInt("loveWinter", 0);*/
        //money = PlayerPrefs.GetInt("money", 0);

        loveSummer = Random.Range(0, 100);
        loveFall= Random.Range(0, 100);
        loveWinter = Random.Range(0, 100);
        contextIdx = 1;
        month = Random.Range(3, 7);
        money = Random.Range(0, 10000000);

        scriptTable = CSVReader.Read("ScriptTable");
        chapterTable = CSVReader.Read("ChapterTable");
        nameTable = CSVReader.Read("CharacterNameTable");
    }

    public void PauseGame()
    {

    }

    public void ContinueGame()
    {

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void StopGame()
    {
        PlayerPrefs.SetInt("contextIdx", contextIdx);
        PlayerPrefs.SetInt("month", month);
        PlayerPrefs.SetInt("loveSummer", loveSummer);
        PlayerPrefs.SetInt("loveFall", loveFall);
        PlayerPrefs.SetInt("loveWinter", loveWinter);
        PlayerPrefs.SetInt("money", money);
    }
}