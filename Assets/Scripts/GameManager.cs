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
        // Resets both scores and starts the first round.
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
        // Resets the paddles and ball before each round.
        playerPaddle.position = playerStartPosition;
        enemyPaddle.position = enemyStartPosition;

        ballController.PlaceAtCenter();

        float timer = countdownTime;

        // Gives both players a short countdown before the ball is launched.
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

        // Starts another round unless the player has already won.
        if (!CheckWin())
            StartNewRound();
    }

    public void ScoreEnemy()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();

        // Starts another round unless the enemy has already won.
        if (!CheckWin())
            StartNewRound();
    }

    public bool CheckWin()
    {
        // Checks whether either player has reached the required score.
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

        // Determines the winner based on the final score.
        bool playerWon = playerScore > enemyScore;

        string winner = SaveController.Instance.GetName(playerWon);

        // Uses a default name if no custom name was entered.
        if (string.IsNullOrEmpty(winner))
            winner = playerWon ? "Player 1" : "Player 2";

        textEndGame.text = "Winner: " + winner;

        // Saves the winner so it can be displayed in the main menu.
        SaveController.Instance.SaveWinner(winner);

        // Returns to the main menu after displaying the result.
        Invoke("LoadMenu", 4f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}