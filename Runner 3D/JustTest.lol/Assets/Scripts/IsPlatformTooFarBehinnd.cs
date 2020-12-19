using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlatformTooFarBehinnd : MonoBehaviour
{
    [SerializeField] GameObject[] collectables;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag ("Player");
    }
    void Update()
    {
        if (transform.position.z < _player.transform.position.z - ObjectPooler._platformSize)
        {
            ObjectPooler.PoolObject(this.gameObject);
        }
    }
}
