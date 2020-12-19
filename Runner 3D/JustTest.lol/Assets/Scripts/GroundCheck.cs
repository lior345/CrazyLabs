using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Controller _player;
    private void Start()
    {
        _player = this.GetComponent<Controller>();
    }

    private void OnCollisionStay(Collision collision)
    {
        _player.isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _player.isGrounded = false;
    }
}

