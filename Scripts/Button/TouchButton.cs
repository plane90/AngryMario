using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//당 버튼에 컨트롤러가 닿으면 눌리고 하는 애니메이션을 실행 시킨다.
public class TouchButton : MonoBehaviour          
{
    private Animator buttonAnim;                  //버튼 애니메이터 변수를 생성

    private readonly int hashIsLD = Animator.StringToHash("isLeftDown");
    private readonly int hashIsRD = Animator.StringToHash("isRightDown");
    private readonly int hashIsFD = Animator.StringToHash("isFowardDown");
    private readonly int hashIsBD = Animator.StringToHash("isBackwardDown");
    private readonly int hashIsSHOT = Animator.StringToHash("isShotDown");
    
    private void Start()
    {
        buttonAnim = GetComponent<Animator>();
    }

    public void ButtonDown(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Left:
                buttonAnim.SetBool(hashIsLD, true);
                break;
            case ButtonType.Right:
                buttonAnim.SetBool(hashIsRD, true);
                break;
            case ButtonType.Up:
                buttonAnim.SetBool(hashIsFD, true);
                break;
            case ButtonType.Down:
                buttonAnim.SetBool(hashIsBD, true);
                break;
            case ButtonType.Shot:
                buttonAnim.SetBool(hashIsSHOT, true);
                break;
        }
    }

    public void ButtonUp(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.Left:
                buttonAnim.SetBool(hashIsLD, false);
                break;
            case ButtonType.Right:
                buttonAnim.SetBool(hashIsRD, false);
                break;
            case ButtonType.Up:
                buttonAnim.SetBool(hashIsFD, false);
                break;
            case ButtonType.Down:
                buttonAnim.SetBool(hashIsBD, false);
                break;
            case ButtonType.Shot:
                buttonAnim.SetBool(hashIsSHOT, false);
                break;
        }
    }
}
