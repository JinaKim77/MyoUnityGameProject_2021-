using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
	
	public void PlayGame ()
    {
        SceneManager.LoadScene(1); 
    }

    public void GoMain()
    {
        SceneManager.LoadScene(0); 
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Back()
    {
         SceneManager.LoadScene(0); 
    }
}
