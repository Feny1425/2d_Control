using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recharge_Jumps : MonoBehaviour
{
    public string PlayerTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PlayerTag)
        {
            character_Controll controller = other.GetComponent<character_Controll>();
            controller.restartJumpTimes();
        }
    }
}
