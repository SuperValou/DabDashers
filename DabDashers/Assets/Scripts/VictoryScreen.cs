using Assets.Scripts.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float aimedSize = 5;

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

        if (this.transform.localScale.x < aimedSize)
        {
            this.transform.localScale *= 1.1f;
        }
    }
}
