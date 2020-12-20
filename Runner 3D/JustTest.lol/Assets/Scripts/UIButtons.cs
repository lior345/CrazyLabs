using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIButtons: MonoBehaviour
{
    private GameManager _gm;
    private void Start()
    {
        Time.timeScale = 0;
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
    public void CameraChange()
    {
        _gm.CameraSwap();
    }
    public void SpeedChange()
    {
        Controller player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        player.RunSpeedChange();

    }
}
