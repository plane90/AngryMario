using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class ClearFlag
    {
        public int coins;
        
        public bool CheckHitAllBlock()
        {
            return true;
        }
    }

    [System.Serializable]
    private class TimeAndCoinBoxUI
    {
        public Text coins;
        public Text remainingTime;
    }

    [SerializeField] private ClearFlag clearFlag;
    [SerializeField] private TimeAndCoinBoxUI timeAndCoinBoxUI;

    public int gainedCoins;
    public GameObject coin;
    public Transform genPosOfCoin;
    public static GameManager Instance { get; set; }

    private WaitForSeconds ws = new WaitForSeconds(0.1f);
    
    private List<GameObject> coins = new List<GameObject>();

    [Header("Time info")]
    public int maxTime = 100;

    private string finishedTime;
    private bool isRecorded = false;

    private int ElapsedTime { get { return Mathf.RoundToInt(Time.time - timeOffset); } }
    private int Minutes { get { return (int)(ElapsedTime / 60.0f); } }
    private int Seconds { get { return Mathf.RoundToInt(ElapsedTime % 60.0f); } }
    private float timeOffset;

    private bool isClearGame = false;

    private bool isEndGame = false;

    [Header("Description Info")]
    public Canvas descriptionCanvas;

    [Header("Pause Info")]
    public Canvas pauseCanvas;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        //timeAndCoinBoxUI.coins.text = "Coins\n" + gainedCoins.ToString();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        isEndGame = false;
        gainedCoins = 0;
        isRecorded = false;
    }

    public int GetNumberOfCoinsForClear()
    {
        return clearFlag.coins;
    }

    public void StartStage()
    {
        Init();
        StartCoroutine(RunTimerAndCheckTime());
    }

    private IEnumerator RunTimerAndCheckTime()
    {
        timeOffset = Time.time;

        float remainingTime = maxTime;
        while (remainingTime >= 0)
        {
            yield return null;
            remainingTime -= Time.deltaTime;
            timeAndCoinBoxUI.remainingTime.text = "남은시간\n" + RemainingMinute(remainingTime) + " : " + RemainingSecond(remainingTime);
            if(gainedCoins >= clearFlag.coins && !isRecorded)
            {
                isRecorded = true;
                finishedTime = RemainingMinute(remainingTime) + " : " + RemainingSecond(remainingTime);
            }
        }
        CheckCoinsAndAssessResult();
    }

    private void CheckCoinsAndAssessResult()
    {
        if (gainedCoins >= clearFlag.coins)
            ClearStage();
        else
            TimeOver();
    }

    private int RemainingMinute(float remainingTime)
    {
        return (int)(remainingTime / 60.0f);
    }

    private int RemainingSecond(float remainingTime)
    {
        return Mathf.RoundToInt(remainingTime % 60.0f);
    }

    private void EndGame()
    {
        //isEndGame = true;
        //UIManager.Instance.ShowEndGameUI();
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.endGame, false);
    }

    private void ClearStage()
    {
        //isEndGame = true;
        UIManager.Instance.ShowClearStageUI(finishedTime, gainedCoins.ToString());
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.clearStage, false);
    }

    private void TimeOver()
    {
        UIManager.Instance.ShowTimeOverUI();
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.timeOver, false);
    }
    
    public void AddCoin()
    {
        gainedCoins++;

        Vector3 genPosOffset = Vector3.up + UnityEngine.Random.insideUnitSphere * 0.5f;
        GameObject generatedCoin = Instantiate(coin, genPosOfCoin.position + genPosOffset, Quaternion.identity);
        coins.Add(generatedCoin);
        timeAndCoinBoxUI.coins.text = "Coins\n" + gainedCoins.ToString();
    }
}
