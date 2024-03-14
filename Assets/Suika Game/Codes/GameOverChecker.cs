using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public static GameOverChecker Instance;
    public float detectionInterval = 0.2f;

    void Start()
    {
        // ������ �������� CheckForFruits �Լ� ȣ��
        InvokeRepeating("CheckForFruits", 0f, detectionInterval);
    }

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 'CrashedFruit' �±׸� ���� ������Ʈ�͸� �浹 ó��
        if (collider.CompareTag("FreshFruit"))
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    void CheckForFruits()
    {
        // Collider ���� ���� ��� ������Ʈ ��������
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);

        bool freshFruitDetected = false;

        // Collider ���� ���� ������Ʈ���� �˻�
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FreshFruit"))
            {
                freshFruitDetected = true;
                break; // �ϳ��� FreshFruit �±׸� ���� ������Ʈ�� ������ ����
            }
        }

        // freshFruitDetected�� ���� ���� ���� ����
        if (freshFruitDetected)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}
