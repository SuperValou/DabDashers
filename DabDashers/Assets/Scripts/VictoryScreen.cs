using Assets.Scripts.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetButtonDown("Menu"))
        {
            SceneManager.LoadScene((int)SceneIndex.MainMenu);
        }
    }
}
