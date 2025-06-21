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
