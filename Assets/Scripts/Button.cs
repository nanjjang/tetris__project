using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    //public GameObject NamingPannel;
    public void GameStart()
    {
        SceneManager.LoadScene("URP2DSceneTemplate");
        GameManager.Instance.Init(); // 게임 매니저 초기화
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.RestartGame(); // 게임 매니저 초기화
    }

    public void gotoMenu()
    {
        SceneManager.LoadScene("Menu"); // 메뉴 씬으로 이동
    }

    public void GameExit()
    {
        Application.Quit();
    }

    //public void Join()
    //{
    //    NamingPannel.SetActive(false);
    //}
}
