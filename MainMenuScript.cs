using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _creditsScroll;   

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainScreen()
    {
        _creditsScroll.SetActive(false);
    }

    public void OpenCredits()
    {
        _creditsScroll.SetActive(true);
    }

    public void Erase() 
    {
        DataLoader.Erase();
        StartGame();
    }
}
