using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    private float spawnRate = 1.0f;
    private int score;

    public bool isGameOver;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    
    public void StartGame()
    {
        score = 0;
        isGameOver = false;
        UpdateScore(0);

        StartCoroutine(SpawnTarget());
        titleScreen.SetActive(false);
    }

    public void SetDifficulty(GameDifficulty difficulty)
    {
        switch (difficulty)
        {
            case GameDifficulty.EASY:
                spawnRate = 3.0f;
                break;
            case GameDifficulty.MEDIUM:
                spawnRate = 2.0f;
                break;
            case GameDifficulty.HARD:
                spawnRate = 1.0f;
                break;
        }
    }

    public void UpdateScore(int scoreInc)
    {
        score += scoreInc;
        scoreText.text = $"Score: {score}";
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        StopCoroutine(SpawnTarget());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            GameObject target = targets[index];
            Instantiate(target, target.transform.position, Quaternion.identity);
        }
    }
}
