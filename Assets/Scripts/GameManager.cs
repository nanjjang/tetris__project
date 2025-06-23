using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOver; //게임오버 UI 오브젝트

    public TextMeshProUGUI txt_s;
    public TextMeshProUGUI txt_h; //최고점수 표시용
    public TextMeshProUGUI txt_t;
    public TextMeshProUGUI txt_ms; //in menu score

    [Header("1~7단계 업그레이드 프리팹")]
    public GameObject[] upgradePrefabs = new GameObject[7];
    private int currentLevel = 0; //레벨

    //public InputField username;

    private float time = 0f;
    float highestScore = 0f;
    public float Userscore = 0f;

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (PlayerPrefs.HasKey("Score"))
        {
            highestScore = PlayerPrefs.GetFloat("Score");
            
            if(SceneManager.GetActiveScene().name == "Menu" )
                txt_h.text = highestScore.ToString("F1");
        }
        else
        {
            highestScore = 0f;
            txt_h.text = "0.0";
            if (SceneManager.GetActiveScene().name == "Menu")
                txt_h.text = highestScore.ToString("F1");
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "URP2DSceneTemplate")
        {
            time += Time.deltaTime;
            txt_t.text = time.ToString("F2"); // Display to two decimal places.
            txt_s.text = Userscore.ToString("F1");
        }
    }

    public void Init()
    {
        // 게임 초기화 로직 (필요시 추가)
        Userscore = 0f;
        currentLevel = 0;
        time = 0f;
    }

    public void Plus()
    {
        Userscore += 100f;
        Debug.Log("스코어: " + Userscore);

        int newLevel = Mathf.FloorToInt(Userscore / 1000f); 
        // 한 레벨 올릴 때만 처리하는거
        if (newLevel > currentLevel && newLevel >= 1 && newLevel <= upgradePrefabs.Length) //레벨증가 판단함
        {
            currentLevel = newLevel;
            ApplyTetrominoUpgrade(currentLevel - 1);
        }
    }

    private void ApplyTetrominoUpgrade(int tetrominoIndex) //새로운 테트로미노로 교체
    {
        var spawner = FindObjectOfType<spawnertetris>();
        if (spawner == null) return;

        GameObject newPrefab = upgradePrefabs[tetrominoIndex];
        spawner.ReplaceTetromino(tetrominoIndex, newPrefab);

        Debug.Log($"레벨업! { (tetrominoIndex+1)*1000 }점 달성: Tetromino[{tetrominoIndex}]을 새 프리팹으로 교체");
    } 

    public void RestartGame() //게임 재시작
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 씬을 다시 로드
        gameOver.SetActive(false); //게임오버 UI 비활성화
        time = 0f; //시간 초기화
        Userscore = 0f; //점수 초기화
        currentLevel = 0; //레벨 초기화
    }

    public void GameOver() //게임오버시 호출
    {
        SaveHighestScore(); //최고점수 저장
        gameOver.SetActive(true); //게임오버 UI 활성화
    }
    //------save data-------//

    void SaveHighestScore()
    {
        if(Userscore > highestScore)
        {
            highestScore = Userscore;
            
        }
        PlayerPrefs.SetFloat("Score", highestScore);
        PlayerPrefs.Save();
    }

    //void SaveUserName()
    //{
    //    PlayerPrefs.SetString("UserName", username.text);
    //}

    //void CheckUserName()
    //{

    //}


}
