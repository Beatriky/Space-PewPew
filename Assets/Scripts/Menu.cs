using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
      //public GameObject menuUI;

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
       // menuUI.SetActive(false); SceneManager.GetActiveScene().buildIndex +1

    }
    public void QuitGame()
    {   Debug.Log("quit");
        Application.Quit();

    }
    
}
