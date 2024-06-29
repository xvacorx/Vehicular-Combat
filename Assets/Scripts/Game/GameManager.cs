using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] Slider slider;
    public float gameDuration = 300f;
    public string winSceneName = "WinScene";
    public string loseSceneName = "LoseScene";

    bool gameIsOver = false;
    private void Start()
    {
        player.gameObject.SetActive(true);
        PlayerManager.Instance.ResetPlayer();
    }
    void Update()
    {
        if (gameIsOver)
            return;

        gameDuration -= Time.deltaTime;
        timer.text = "Time Left: " + gameDuration.ToString("F2");
        slider.value = PlayerManager.Instance.hP;

        if (gameDuration <= 0)
        {
            WinGame();
        }

        if (PlayerManager.Instance.hP <= 0)
        {
            LoseGame();
        }
    }

    void WinGame()
    {
        gameIsOver = true;
        SceneManager.LoadScene(winSceneName);
    }

    void LoseGame()
    {
        gameIsOver = true;
        Invoke("LoadLoseScene", 5f);
    }

    void LoadLoseScene()
    {
        SceneManager.LoadScene(loseSceneName);
    }
}