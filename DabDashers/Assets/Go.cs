using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour
{

    public GameObject text;
    public GameObject loading;
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Return))
	    {
	        text.SetActive(false);
            loading.SetActive(true);
            SceneManager.LoadScene((int)SceneIndex.Level1);
        }
	}
}
