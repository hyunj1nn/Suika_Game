using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤 인스턴스

    public GameObject gameOverMenuUI;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI scoreText; // 점수를 표시하는 Text 컴포넌트
    public GameObject gameOverUI;

    public bool IsPaused { get; private set; } // 일시 정지 상태

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
    //  일시 정지  //
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true); // 일시 정지 메뉴 UI 활성화
        Time.timeScale = 0f; // 시간을 멈추어서 게임 일시 정지
        IsPaused = true; // 일시 정지 상태로 설정
    }
    //  게임 재개  //
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // 일시 정지 메뉴 UI 비활성화
        Time.timeScale = 1f; // 시간을 정상으로 돌려 게임을 재개
        IsPaused = false; // 일시 정지 상태 해제
    }
    //   재시작   //
    public void RestartGame()
    {
        Score.Instance.ResetScore();

        // ESC 메뉴 게임 오버 UI 비활성화
        if (pauseMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(false);
        }
        if (gameOverUI.activeSelf)
        {
            gameOverUI.SetActive(false);
        }
        // 게임 상태 초기화
        Time.timeScale = 1; // 시간 스케일을 다시 정상으로 설정

        if (FruitSpawner.Instance != null)
        {
            FruitSpawner.Instance.SpawnRandomFruit();
        }

        // "Fruit" 태그와 "FreshFruit" 태그를 가진 모든 과일 파괴
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
        IsPaused = false; // 게임을 재시작할 때 일시 정지 상태를 해제
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");

        // 게임 오버 UI를 활성화합니다.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Application.Quit(); // 애플리케이션 종료
    }
}
