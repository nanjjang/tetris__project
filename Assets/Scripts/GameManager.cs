using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI txt_s;
    public TextMeshProUGUI txt_t;

    //public InputField username;

    private float time = 0f;
    float highestScore = 0f;
    float currentScore = 0f;

    void Awake()
    {
        if(PlayerPrefs.HasKey("Score"))
        {
            highestScore = PlayerPrefs.GetFloat("Score");
        }
        else
        {
            highestScore = 0f;
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "InGame")
        {
            time += Time.deltaTime;
            txt_t.text = time.ToString("F2"); // Display to two decimal places.
        }


        
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
