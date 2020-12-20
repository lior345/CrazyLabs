using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    #region Vareiables
    private float _nextLane;
    private Vector3 _nextPos;
    private Rigidbody _rb;
    private float _runSpeed;

    [SerializeField] private float _lerpSpeed = 5;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Slider _speedSlider;

    public float _actualSpeed;//calculated by the pre-determined speed*speed multiplier from menu
    public GameObject replay;//When failing- a replay option will appear
    public Transform cameraTransform;
    public TouchManager touchManager;
    public bool alive = true;
    public bool isGrounded;
    #endregion
    private void Start()
    {
        _nextLane = 0;//starting in the middle
        _rb = GetComponent<Rigidbody>();
        _actualSpeed = _speedSlider.value=10;
    }
    private void Update()
    {
        if (transform.position.y < -3)
        {
            alive = false;
            Time.timeScale = 0;
        }//one way to die is falling off
        if (alive)
        {
            transform.Translate(0, 0, _actualSpeed * Time.deltaTime);//forward movement*speed Multipier    
            transform.position = Vector3.Lerp(transform.position, new Vector3(_nextPos.x, transform.position.y, transform.position.z), _lerpSpeed * Time.deltaTime);//gradual side movement
            cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, transform.position.z - 6);//camera Movement

            #region Movement Inputs
            if (Input.GetKeyDown(KeyCode.D) || touchManager.swipeRight)//move right
            {
                RightMove();
            }
            if (Input.GetKeyDown(KeyCode.A) || touchManager.swipeLeft)//move left
            {
                LeftMove();
            }
            if (Input.GetKeyDown(KeyCode.Space) || touchManager.swipeUp)
            {
                Jump();
            }//jump
            #endregion
        }
        else Death();//death
    }
    public void RunSpeedChange()
    {
        _runSpeed = _speedSlider.value;
        SetSpeedMultiplier(1);
    }
    public void SetSpeedMultiplier(float multiplier)
    {
        _actualSpeed = _runSpeed * multiplier;
    }//speed calculation
    private void LeftMove()//Left Movement Check
    {
        _nextLane--;
        if (_nextLane == -2)
        {
            _nextLane = -1;
        }
        _nextPos = new Vector3(_nextLane, transform.position.y, transform.position.z);
    }
    private void RightMove()//Right Movement Check
    {
        _nextLane++;
        if (_nextLane == 2)
        {
            _nextLane = 1;
        }
        _nextPos = new Vector3(_nextLane, transform.position.y, transform.position.z);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            _rb.velocity = new Vector3(0, _jumpForce, 0);
        }
    }//jump
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Booster"))
        {
            StopCoroutine("Booster");//stoping to prevent dual corotine being called
            StartCoroutine("Booster");//starting by string restarts the coroutine instead of continuing it
        }
    }//Booster Checks
    private void OnTriggerEnter(Collider other)//Triggers Check- Obstacles / Collectables / finishline 
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Death();
        }

        else if (other.gameObject.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
            StartCoroutine(Collectables(other));
        }
    }
    IEnumerator Booster()
    {
        SetSpeedMultiplier(1.5f);
        yield return new WaitForSeconds(3);
        SetSpeedMultiplier(1);
    }//booster ramp
    IEnumerator Collectables(Collider other)
    {
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        other.gameObject.GetComponent<MeshRenderer>().enabled = true;

    }//collectables behavior
    public void Death()
    {
        replay.SetActive(true);
        alive = false;
        Time.timeScale = 0;
    }
    
}