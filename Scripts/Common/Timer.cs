using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private int ElapsedTime { get { return Mathf.RoundToInt(Time.time - timeOffset); } }
    private int Minutes { get { return (int)(ElapsedTime / 60.0f); } }
    private int Seconds { get { return Mathf.RoundToInt(ElapsedTime % 60.0f); } }
    private float timeOffset;
    private bool canRun = true;
    private WaitForSeconds ws = new WaitForSeconds(0.1f);


    private void OnEnable()
    {
        timeOffset = Time.time;
    }

    public void RunTimer(Text receiver)
    {
        StartCoroutine(DisplayCurrentTime(receiver));
    }

    private IEnumerator DisplayCurrentTime(Text timeReceiver)
    {
        timeOffset = Time.time;

        while (canRun)
        {
            yield return ws;
            timeReceiver.text = "경과시간\n" + Minutes + " : " + Seconds;
        }
    }
}
