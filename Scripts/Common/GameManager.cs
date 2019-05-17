using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    private class ClearFlag
    {
        public int requiredCoins;
        
        public bool IsClear(int blockCount)
        {
            return blockCount <= 0;
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

    [Header("Coin Info")]
    private int gainedCoins;
    private List<GameObject> coins = new List<GameObject>();

    [SerializeField] private GameObject coin;
    [SerializeField] private Transform genPosOfCoin;

    [Header("Time Info")]
    private string finishedTime;
    private int ElapsedTime { get { return Mathf.RoundToInt(Time.time - timeOffset); } }
    private int Minutes { get { return (int)(ElapsedTime / 60.0f); } }
    private int Seconds { get { return Mathf.RoundToInt(ElapsedTime % 60.0f); } }
    private float timeOffset;

    [SerializeField] private int maxTime = 100;

    [Header("Block Info")]
    private int currentBlockCount = 0;

    public static GameManager Instance { get; set; }

    private bool isRecorded = false;
    private WaitForSeconds ws = new WaitForSeconds(0.1f);

    public void StartStage()
    {
        Init();
        StartCoroutine(RunTimerAndCheckTime());
    }

    public int GetRequiredCoinsForClear()
    {
        return clearFlag.requiredCoins;
    }

    public void AddCoin()
    {
        gainedCoins++;
        Vector3 genPosOffset = Vector3.up + UnityEngine.Random.insideUnitSphere * 0.5f;
        GameObject generatedCoin = Instantiate(coin, genPosOfCoin.position + genPosOffset, Quaternion.identity);
        coins.Add(generatedCoin);
        timeAndCoinBoxUI.coins.text = "Coins\n" + gainedCoins.ToString();
    }

    public void DecreaseBlockCount()
    {
        currentBlockCount--;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        currentBlockCount = FindObjectsOfType<Brick>().Length +
            FindObjectsOfType<Brick_core>().Length +
            FindObjectsOfType<Brick_power>().Length;
        gainedCoins = 0;
        isRecorded = false;
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
            if(gainedCoins >= clearFlag.requiredCoins && !isRecorded)
            {
                isRecorded = true;
                finishedTime = RemainingMinute(remainingTime) + " : " + RemainingSecond(remainingTime);
            }
            if (clearFlag.IsClear(currentBlockCount)) break;
        }
        CheckCoinsAndAssessResult();
    }

    private void CheckCoinsAndAssessResult()
    {
        if (gainedCoins >= clearFlag.requiredCoins)
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
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.endGame, false);
    }

    private void ClearStage()
    {
        UIManager.Instance.ShowClearStageUI(finishedTime, gainedCoins.ToString());
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.clearStage, false);
    }

    private void TimeOver()
    {
        UIManager.Instance.ShowTimeOverUI(gainedCoins.ToString());
		SoundManager.Instance.AudioSetting(SoundManager.Instance.gameObject, SoundManager.Instance.timeOver, false);
    }
}
