using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType { Left, Right, Up, Down, Shot}

public class ButtonManager_ : MonoBehaviour
{
    //public List<Controllable> targets;
    private Controllable target;
    public static ButtonManager_ Instance { get; private set; }
    
    public List<TouchButton> touchCtroller;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        //touchCtroller = GameObject.Find("ctroller").GetComponent<TouchButton>
    }

    public void PushButton(ButtonType type)
    {
        if (target == null ||!target.isActiveAndEnabled)
            target = GameObject.FindObjectOfType<Controllable>();
        target.OnCtrl(type);
    }

    public void TouchCollersButtonDown(ButtonType type)
    {
        foreach (var item in touchCtroller)
        {
            item.ButtonDown(type);
        }
    }

    public void TouchCollersButtonUp(ButtonType type)
    {
        foreach (var item in touchCtroller)
        {
            item.ButtonUp(type);
        }
    }
}
