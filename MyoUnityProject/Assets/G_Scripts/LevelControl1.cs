using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl1 : MonoBehaviour
{
    //public int index;
    //public string levelName;
    //[SerializeField] private string newLevel;
    [SerializeField] private int newLevel;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //play the sound
            FindObjectOfType<AudioManager>().Play("Exit");

            Debug.Log("You are entering the door for the next level");
            // Loadinf level will build index
            SceneManager.LoadScene (newLevel);
            
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
             
        }
    }
}