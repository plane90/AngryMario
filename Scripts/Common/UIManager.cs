using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    private class DescriptionUI
    {
        public Canvas canvas;
        public Text description;
    }
    [System.Serializable]
    private class EndGameUI
    {
        public Canvas canvas;
        public Text finishedTime;
        public Text gainedCoin;
    }
    [System.Serializable]
    private class ClearUI
    {
        public Canvas canvas;
        public Text finishedTime;
        public Text gainedCoin;
    }
    [System.Serializable]
    private class TimeOverUI
    {
        public Canvas canvas;
        public Text gainedCoin;
    }

    [SerializeField] private DescriptionUI descriptionUI;
    [SerializeField] private EndGameUI endGameUI;
    [SerializeField] private ClearUI clearUI;
    [SerializeField] private TimeOverUI timeOverUI;
    [SerializeField] private Canvas fadeScreen;

    public static UIManager Instance { get; set; }

    private bool isClearGame = false;
    private int currentStage = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnClickStartButtonOfTitle()
    {
        currentStage = 1;

        LoadScene("World_1");
        GameManager.Instance.StartStage();

        //Time.timeScale = 0;
    }

    private void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        var target = Instantiate(fadeScreen, GameObject.Find("VR Origin").transform);
        float alpha = 0;
        while (alpha < 1.0f)
        {
            alpha += 1.5f * Time.deltaTime;
            target.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        yield return null;
        //if (GameManager.Instance != null)
        //    GameManager.Instance.gameObject.SetActive(true);
        //descriptionUI.description.text = "또는 Coin " +
        //    GameManager.Instance.GetNumberOfCoinsForClear().ToString() + " 개 획득\n" + "(Coin의 획득처)";
        
        target = Instantiate(fadeScreen, GameObject.Find("VR Origin").transform);
        while (alpha > 0)
        {
            alpha -= 1.5f * Time.deltaTime;
            target.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public void OnClickQuitButtonOfTitle()
    {
        Debug.Log("Quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                    Application.OpenURL("http://google.com");
#else
                    Application.Quit();
#endif
    }

    public void OnClickStartButtonOfDescription()
    {
        descriptionUI.canvas.enabled = false;
        timeOverUI.canvas.enabled = false;
        clearUI.canvas.enabled = false;
        //Time.timeScale = 1;
    }

    public void OnClickRestartButton()
    {
        //isEndGame = true;
        timeOverUI.canvas.enabled = false;
        //SceneManager.LoadScene("World_" + currentStage.ToString());
        //Init();
    }

    public void OnClickGoTitleButton()
    {
        currentStage = 0;
        this.gameObject.SetActive(false);
        timeOverUI.canvas.enabled = false;
        clearUI.canvas.enabled = false;
        endGameUI.canvas.enabled = false;
        //Init();
    }

    public void OnClickGoTitle()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void OnClickNextButton()
    {
        currentStage = 2;
        clearUI.canvas.enabled = false;
        LoadScene("World_" + currentStage.ToString());
        GameManager.Instance.StartStage();
        //Init();
    }

    //public void ShowEndGameUI()
    public void ShowClearStageUI(string finishedTime, string gainedCoin)
    {
        if (currentStage == 1)
        { 
            clearUI.canvas.enabled = true;
            clearUI.canvas.referencePixelsPerUnit++;
            clearUI.finishedTime.text = finishedTime;
            clearUI.gainedCoin.text = gainedCoin;
        }
        else
        { 
            endGameUI.canvas.enabled = true;
            endGameUI.canvas.referencePixelsPerUnit++;
            endGameUI.finishedTime.text = finishedTime;
            endGameUI.gainedCoin.text = gainedCoin;
        }
    }

    //public void ShowClearStageUI()
    //{
    //    //clearCanvas.enabled = true;
    //    //timeFinishedText.text = string.Format(((int)(timeFinished / 60)).ToString()
    //    //    + " : " + Mathf.RoundToInt(timeFinished % 60).ToString());
    //    //CoinCountTextOfClear.text = coinCount.ToString();
    //    //clearCanvas.referencePixelsPerUnit++;
    //}

    public void ShowTimeOverUI()
    {

    }
}
