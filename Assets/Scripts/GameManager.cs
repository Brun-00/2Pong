using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BallController ballController;
    public GameObject screenEndGame;
    public int winPoints;
    public int playerScore = 0;
    public int enemyScore = 0;
    public Text playerScoreText;
    public Text enemyScoreText;
    public Transform playerPaddle;
    public Transform enemyPaddle;
    public Text textEndGame;
    public AudioSource winSound;

    public float countdownTime = 3f;
    public Text countdownText;

    private Vector3 playerStartPosition = new Vector3(-7f, 0f, 0f);
    private Vector3 enemyStartPosition = new Vector3(7f, 0f, 0f);

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        playerScore = 0;
        enemyScore = 0;
        enemyScoreText.text = enemyScore.ToString();
        playerScoreText.text = playerScore.ToString();
        StartNewRound();
    }

    private void StartNewRound()
    {
        StartCoroutine(NewRoundRoutine());
    }

    private IEnumerator NewRoundRoutine()
    {
        playerPaddle.position = playerStartPosition;
        enemyPaddle.position = enemyStartPosition;
        ballController.PlaceAtCenter();

        float timer = countdownTime;
        while (timer > 0f)
        {
            if (countdownText != null)
                countdownText.text = Mathf.Ceil(timer).ToString();

            yield return null;
            timer -= Time.deltaTime;
        }

        if (countdownText != null)
            countdownText.text = "";

        ballController.Launch();
    }

    public void ScorePlayer()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        if (!CheckWin())
            StartNewRound();
    }

    public void ScoreEnemy()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();
        if (!CheckWin())
            StartNewRound();
    }

    public bool CheckWin()
    {
        if (enemyScore >= winPoints || playerScore >= winPoints)
        {
            EndGame();
            return true;
        }
        return false;
    }

    public void EndGame()
    {
        winSound.Play();
        screenEndGame.SetActive(true);
        bool playerWon = playerScore > enemyScore;
        string winner = SaveController.Instance.GetName(playerWon);
        if (string.IsNullOrEmpty(winner))
            winner = playerWon ? "Player 1" : "Player 2";
        textEndGame.text = "Winner: " + winner;
        SaveController.Instance.SaveWinner(winner);
        Invoke("LoadMenu", 4f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}