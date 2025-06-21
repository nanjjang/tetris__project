using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱���

    public TextMeshProUGUI txt_s;
    public TextMeshProUGUI txt_t;

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
        Userscore += 100f; // ���� ����
        Debug.Log("����: " + Userscore);
        if (Userscore % 400f == 0)
        {
            // 1200������ ���� ��
            LevelUp();
        }
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
