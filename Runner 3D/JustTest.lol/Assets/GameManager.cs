using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _gravityScale;
    [SerializeField] private Camera _cam1, _cam2;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Controller _player;
    void Start()
    {
        Physics.gravity = new Vector3(0, -_gravityScale, 0);

        Screen.orientation = ScreenOrientation.Portrait;

        if (PlayerPrefs.GetInt("Camera") == 2)
        {
            _cam1.enabled = false;
            _cam2.enabled = true;
        }
    }
    private void Update()
    {
        _speedSlider.value = _player._actualSpeed;
    }
    public void CameraSwap()
    {
        if(PlayerPrefs.GetInt("Camera")==2)
        {
            PlayerPrefs.SetInt("Camera", 1);
            _cam1.enabled = true;
            _cam2.enabled = false;

        }
        else
        {
            PlayerPrefs.SetInt("Camera", 2);
            _cam1.enabled = false;
            _cam2.enabled = true;
        }
    }

}
