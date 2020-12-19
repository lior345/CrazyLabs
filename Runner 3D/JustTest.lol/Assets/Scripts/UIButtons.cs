using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIButtons: MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }
    public void PressToStart()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
