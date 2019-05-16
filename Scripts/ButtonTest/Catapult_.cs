using UnityEngine;

public class Catapult_ : Controllable
{
    public override void OnCtrl(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Left:
                Debug.Log("Left");
                break;
            case ButtonType.Right:
                Debug.Log("Right");
                break;
            case ButtonType.Up:
                Debug.Log("Up");
                break;
            case ButtonType.Down:
                Debug.Log("Down");
                break;
        }
        Debug.Log("OnCtrl");
    }
}
