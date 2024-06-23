using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{

    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close(float delayTime)
    {
        base.Close(delayTime);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        Close(0);
    }

    public void RetryButton()
    {
        Debug.Log("Bam nut Retry");

        Time.timeScale = 1; // Resume game time
        Close(0);
        Debug.Log("Dong UISetting");

        LevelManager.Instance.OnRetry();
        Debug.Log("Goi ham OnRetry");
    }
}
