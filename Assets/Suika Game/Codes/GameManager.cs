using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱��� �ν��Ͻ�

    public GameObject gameOverMenuUI;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI scoreText; // ������ ǥ���ϴ� Text ������Ʈ
    public GameObject gameOverUI;

    public bool IsPaused { get; private set; } // �Ͻ� ���� ����

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //  �Ͻ� ����  //
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // �Ͻ� ���� �޴� UI Ȱ��ȭ
        Time.timeScale = 0f; // �ð��� ���߾ ���� �Ͻ� ����
        IsPaused = true; // �Ͻ� ���� ���·� ����
    }
    //  ���� �簳  //
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // �Ͻ� ���� �޴� UI ��Ȱ��ȭ
        Time.timeScale = 1f; // �ð��� �������� ���� ������ �簳
        IsPaused = false; // �Ͻ� ���� ���� ����
    }
    //   �����   //
    public void RestartGame()
    {
        Score.Instance.ResetScore();

        // ESC �޴� ���� ���� UI ��Ȱ��ȭ
        if (pauseMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(false);
        }
        if (gameOverUI.activeSelf)
        {
            gameOverUI.SetActive(false);
        }
        // ���� ���� �ʱ�ȭ
        Time.timeScale = 1; // �ð� �������� �ٽ� �������� ����

        if (FruitSpawner.Instance != null)
        {
            FruitSpawner.Instance.SpawnRandomFruit();
        }

        // "Fruit" �±׿� "FreshFruit" �±׸� ���� ��� ���� �ı�
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (var fruit in fruits)
        {
            if (fruit.transform.parent != FruitSpawner.Instance.spawnPoint)
            {
                Destroy(fruit);
            }
        }

        GameObject[] crashedFruits = GameObject.FindGameObjectsWithTag("FreshFruit");
        foreach (var fruit in crashedFruits)
        {
            if (fruit.transform.parent != FruitSpawner.Instance.spawnPoint)
            {
                Destroy(fruit);
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        IsPaused = false; // ������ ������� �� �Ͻ� ���� ���¸� ����
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");

        // ���� ���� UI�� Ȱ��ȭ�մϴ�.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit(); // ���ø����̼� ����
    }
}
