using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Return))
	    {
	        SceneManager.LoadScene((int) SceneIndex.Level1);
        }
	}
}
