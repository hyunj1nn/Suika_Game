using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하기위해 추가

public class Score : MonoBehaviour
{
    public static Score Instance; // 싱글톤 인스턴스
    public TextMeshProUGUI scoreText; // 점수를 표시할 Text 컴포넌트의 참조
    private int currentScore = 0; // 현재 점수

    void Awake()
    {
        // 싱글톤 패턴을 사용하여 Score 인스턴스를 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 로드 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스가 있을 경우 파괴
        }
    }

    // 과일 타입에 따른 점수 증가
    public void AddScore(Fruit.FruitType fruitType)
    {
        switch (fruitType)
        {
            case Fruit.FruitType.Cherry:
                currentScore += 20; 
                break;
            case Fruit.FruitType.Strawberry:
                currentScore += 40; 
                break;
            case Fruit.FruitType.Grape:
                currentScore += 60; 
                break;
            case Fruit.FruitType.Grapefruit:
                currentScore += 80; 
                break;
            case Fruit.FruitType.Tomato:
                currentScore += 100; 
                break;
            case Fruit.FruitType.Orange:
                currentScore += 120; 
                break;
            case Fruit.FruitType.Fig:
                currentScore += 140; 
                break;
            case Fruit.FruitType.Peach:
                currentScore += 160; 
                break;
            case Fruit.FruitType.Melon:
                currentScore += 180; 
                break;
            case Fruit.FruitType.Watermelon:
                currentScore += 200; 
                break;
            case Fruit.FruitType.Goldenwatermelon:
                currentScore += 220; 
                break;
            default:
                break;
        }

        // 점수가 변경될 때마다 점수 UI를 업데이트
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();

    }
    // 점수 UI 업데이트
    private void UpdateScoreUI()
    {
        // Text 컴포넌트의 내용을 현재 점수로 업데이트
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    // 현재 점수를 외부에서 가져올 수 있도록 하는 메서드
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
