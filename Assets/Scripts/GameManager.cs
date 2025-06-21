using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤

    public TextMeshProUGUI txt_s;
    public TextMeshProUGUI txt_t;

    //public InputField username;

    private float time = 0f;
    float highestScore = 0f;
    float currentScore = 0f;

    public float Userscore = 0f;

    void Awake()
    {
        // 싱글톤 초기화
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
        Userscore += 100f; // 점수 증가
        Debug.Log("점수: " + Userscore);
        if (Userscore % 400f == 0)
        {
            // 1200점마다 레벨 업
            LevelUp();
        }
    }

    void LevelUp()
    {

    }
    //------save data-------//    시간 관계상 시험기간이기도 해서 보류.

    public void SaveHighestScore() //게임 끝났을 때 최고 기록 갱신가능하게 만들어야함.
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
