using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    //public GameObject NamingPannel;
    public void GameStart()
    {
        SceneManager.LoadScene("URP2DSceneTemplate");
        GameManager.Instance.Init(); // ���� �Ŵ��� �ʱ�ȭ
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.RestartGame(); // ���� �Ŵ��� �ʱ�ȭ
    }

    public void gotoMenu()
    {
        SceneManager.LoadScene("Menu"); // �޴� ������ �̵�
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
