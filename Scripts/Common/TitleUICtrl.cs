using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUICtrl : MonoBehaviour
{
    public void OnStart()
    {
        UIManager.Instance.OnClickStartButtonOfTitle();
    }

    public void OnQuit()
    {
        UIManager.Instance.OnClickQuitButtonOfTitle();
    }
}
