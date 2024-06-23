using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if(character != null )
        {
            LevelManager.Instance.OnFinishGame();
            if(character is Player)
            {
                UIManager.Instance.OpenUI<Win>();
            }
            else
            {
                UIManager.Instance.OpenUI<Lose>();
            }
            UIManager.Instance.CloseUI<GamePlay>();

            GameManager.Instance.ChangeState(GameState.Pause);

            character.ChangeAnim("dance");
            character.transform.eulerAngles = Vector3.up * 180;
            character.OnInit();
        }
    }
}
