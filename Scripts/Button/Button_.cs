using System;
using System.Collections;
using UnityEngine;

public class Button_ : MonoBehaviour
{
    private Animator anim;
    private bool isButtonDown = false;

    public ButtonType type;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    
    //    ButtonManager_.Instance.TouchCollersButtonDown(type);
    //    ButtonManager_.Instance.PushButton(type);
    //}
    //
    //private void OnTriggerExit(Collider other)
    //{
    //    ButtonManager_.Instance.TouchCollersButtonUp(type);
    //   
    //}

    public void OnClickButton()
    {
        isButtonDown = true;
        StartCoroutine(OnClickButtonAction());
    }

    public void OnExitButton()
    {
        StopAllCoroutines();
        isButtonDown = false;
        ButtonManager_.Instance.TouchCollersButtonUp(type);
    }

    private IEnumerator OnClickButtonAction()
    {
        while (isButtonDown)
        {
            yield return new WaitForSeconds(0.01f);
            ButtonManager_.Instance.TouchCollersButtonDown(type);
            ButtonManager_.Instance.PushButton(type);
        }
    }
}