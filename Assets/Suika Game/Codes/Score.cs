using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro�� ����ϱ����� �߰�

public class Score : MonoBehaviour
{
    public static Score Instance; // �̱��� �ν��Ͻ�
    public TextMeshProUGUI scoreText; // ������ ǥ���� Text ������Ʈ�� ����
    private int currentScore = 0; // ���� ����

    void Awake()
    {
        // �̱��� ������ ����Ͽ� Score �ν��Ͻ��� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� �ε� �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ��� ���� ��� �ı�
        }
    }

    // ���� Ÿ�Կ� ���� ���� ����
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

        // ������ ����� ������ ���� UI�� ������Ʈ
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();

    }
    // ���� UI ������Ʈ
    private void UpdateScoreUI()
    {
        // Text ������Ʈ�� ������ ���� ������ ������Ʈ
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    // ���� ������ �ܺο��� ������ �� �ֵ��� �ϴ� �޼���
    public int GetCurrentScore()
    {
        return currentScore;
    }
}
