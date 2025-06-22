using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱���

    public TextMeshProUGUI txt_s;
    public TextMeshProUGUI txt_t;

    [Header("1~7단계 업그레이드 프리팹")]
    public GameObject[] upgradePrefabs = new GameObject[7];
    private int currentLevel = 0; //레벨

    //public InputField username;

    private float time = 0f;
    float highestScore = 0f;
    float currentScore = 0f;

    public float Userscore = 0f;

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //if(PlayerPrefs.HasKey("Score"))
        //{
        //    highestScore = PlayerPrefs.GetFloat("Score");
        //}
        //else
        //{
        //    highestScore = 0f;
        //}
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "URP2DSceneTemplate")
        {
            time += Time.deltaTime;
            txt_t.text = time.ToString("F2"); // Display to two decimal places.
        }

        txt_s.text = Userscore.ToString("F1");
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

    void LevelUp()
    {

    }
    //------save data-------//    �ð� ����� ����Ⱓ�̱⵵ �ؼ� ����.

    public void SaveHighestScore() //���� ������ �� �ְ� ��� ���Ű����ϰ� ��������.
    {
        if(currentScore > highestScore)
        {
            highestScore = currentScore;
            
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
