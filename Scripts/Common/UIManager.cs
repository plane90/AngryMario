using System.Collections;
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
        LoadSceneAndStartStage("World_1");
    }

    private void LoadSceneAndStartStage(string sceneName)
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
        yield return SceneManager.LoadSceneAsync(sceneName);

        target = Instantiate(fadeScreen, GameObject.Find("VR Origin").transform);
        while (alpha > 0)
        {
            alpha -= 1.5f * Time.deltaTime;
            target.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        if (currentStage != 0)
        {
            GameManager.Instance.gameObject.SetActive(true);
            GameManager.Instance.StartStage();
        }

        if(currentStage == 1)
        {
            descriptionUI.canvas.enabled = true;
            descriptionUI.description.text = "또는 Coin " +
                GameManager.Instance.GetRequiredCoinsForClear().ToString() + " 개 획득\n" + "(Coin의 획득처)";
            descriptionUI.canvas.referencePixelsPerUnit++;
            Time.timeScale = 0;
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
        descriptionUI.canvas.referencePixelsPerUnit--;
        descriptionUI.canvas.enabled = false;
        timeOverUI.canvas.enabled = false;
        clearUI.canvas.enabled = false;
        Time.timeScale = 1;
    }

    public void OnClickRestartButton()
    {
        timeOverUI.canvas.enabled = false;
        LoadSceneAndStartStage("World_" + currentStage.ToString());
    }

    public void OnClickGoTitleButton()
    {
        currentStage = 0;
        GameManager.Instance.gameObject.SetActive(false);
        timeOverUI.canvas.enabled = false;
        clearUI.canvas.enabled = false;
        endGameUI.canvas.enabled = false;
        LoadSceneAndStartStage("TitleMenu");
    }

    public void OnClickNextButton()
    {
        currentStage = 2;
        clearUI.canvas.enabled = false;
        LoadSceneAndStartStage("World_" + currentStage.ToString());
    }
    
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

    public void ShowTimeOverUI(string gainedCoins)
    {
        timeOverUI.canvas.enabled = true;
        timeOverUI.gainedCoin.text = gainedCoins;
        timeOverUI.canvas.referencePixelsPerUnit++;
        timeOverUI.canvas.referencePixelsPerUnit--;
    }
}
