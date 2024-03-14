using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    public static GameOverChecker Instance;
    public float detectionInterval = 0.2f;

    void Start()
    {
        // 일정한 간격으로 CheckForFruits 함수 호출
        InvokeRepeating("CheckForFruits", 0f, detectionInterval);
    }

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 'CrashedFruit' 태그를 가진 오브젝트와만 충돌 처리
        if (collider.CompareTag("FreshFruit"))
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    void CheckForFruits()
    {
        // Collider 범위 내의 모든 오브젝트 가져오기
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);

        bool freshFruitDetected = false;

        // Collider 범위 내의 오브젝트들을 검사
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("FreshFruit"))
            {
                freshFruitDetected = true;
                break; // 하나라도 FreshFruit 태그를 가진 오브젝트가 있으면 종료
            }
        }

        // freshFruitDetected에 따라 게임 상태 변경
        if (freshFruitDetected)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}
