using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public GameObject NamingPannel;
    public void GameStart()
    {
        SceneManager.LoadScene("URP2DSceneTemplate");
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
