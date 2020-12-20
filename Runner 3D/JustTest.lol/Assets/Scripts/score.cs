using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmPro;
    
        void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        StartCoroutine(AdvanceScore());
    }

    void Update()
    {
        tmPro.text = "Score:" + PlayerPrefs.GetInt("score".ToString());
    }
    IEnumerator AdvanceScore()
    {
        yield return new WaitForSeconds(.1f);
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(AdvanceScore());
    }
}
