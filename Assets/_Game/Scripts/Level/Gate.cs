using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ColorObject
{
    private BoxCollider boxCollider;

    private void Start()
    {
        // Khởi tạo boxCollider bằng cách lấy BoxCollider từ GameObject này
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider not found on the GameObject.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (boxCollider != null)
        {
            Character character = other.GetComponent<Character>();
            if (character != null) // Kiểm tra nếu other là một Character
            {
                Debug.Log("Character exited the trigger.");

                boxCollider.isTrigger = false;
            }
            else
            {
                Debug.LogError("Other object does not have a Character component.");
            }
        }
        else
        {
            if (boxCollider == null)
            {
                Debug.LogError("boxCollider is not initialized.");
            }
        }
    }
}