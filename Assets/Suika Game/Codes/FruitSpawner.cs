using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance; // 싱글톤 인스턴스
    public GameObject[] fruitPrefabs; // 랜덤하게 생성될 과일 프리팹들의 배열
    public Transform spawnPoint; // 집게 오브젝트에서 과일들이 생성될 포인트

    private Queue<GameObject> spawnedFruits = new Queue<GameObject>();

    public Image nextFruitUIImage; // 다음 과일을 표시할 UI 이미지
    private int nextFruitIndex; // 다음에 생성될 과일의 인덱스

    private float timeSinceLastSpawn = 0.0f; // 마지막 과일 생성 이후의 시간
    public float spawnDelay = 1.0f; // 더블클릭 문제를 방지하기 위한 생성 지연 시간

    void Start()
    {
        SpawnRandomFruit(); // 게임 시작 시 랜덤한 과일을 생성
        PrepareNextFruit(); // 게임 시작 시 다음 과일을 준비
    }

    void Update()
    {
        // GameManager의 인스턴스를 통해 게임이 일시 정지 상태인지 확인
        if (GameManager.Instance.IsPaused)
        {
            return; // 게임이 일시 정지 상태이면 아래의 코드를 실행하지 않음
        }

        // 마지막 스폰 이후의 시간 업데이트
        timeSinceLastSpawn += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeSinceLastSpawn >= spawnDelay) // 마우스 클릭 입력 감지
        {
            if (spawnedFruits.Count > 0)
            {
                GameObject spawnFruit = spawnedFruits.Dequeue(); // 생성된 과일을 가져옴
                spawnFruit.transform.parent = null; // 부모-자식 관계 해제

                //spawnFruit.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                Rigidbody2D fruitRigidbody = spawnFruit.GetComponent<Rigidbody2D>();
                if (fruitRigidbody != null)
                {
                    fruitRigidbody.gravityScale = 1.3f; // 생성된 과일의 중력을 변경
                    StartCoroutine(ChangeTagAfterDelay(spawnFruit)); // 태그 변경 코루틴 호출
                }
            }

            StartCoroutine(DelayedSpawn()); // 마우스 클릭이 감지되면 DelayedSpawn 함수를 불러옴
            timeSinceLastSpawn = 0.0f; // 타이머 리셋
        }
    }

    IEnumerator ChangeTagAfterDelay(GameObject fruit)
    {
        yield return new WaitForSeconds(1f); // 1초 기다림
        if (fruit != null)
        {
            fruit.tag = "FreshFruit"; // 태그 변경
            Debug.Log("태그 변경됨: " + fruit.tag);
        }
    }

    void Awake()
    {
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

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);  // spawnDelay 뒤에 SpawnRandomFruit 함수를 불러와 랜덤으로 과일을 생성

        SpawnRandomFruit();
        PrepareNextFruit();
    }

    void PrepareNextFruit()
    {
        // 다음 과일 인덱스를 랜덤으로 선택하지만, 과일은 생성하지 않고 저장만함
        nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        UpdateNextFruitUI(); // 다음 과일 UI 업데이트
    }

    public void SpawnRandomFruit()
    {
        // spawnPoint에 자식 오브젝트(과일)가 이미 존재하는지 확인
        if (spawnPoint.childCount > 0)
        {
            // spawnPoint에 과일이 존재하면 새로운 과일을 생성하지 않음
            return;
        }

        // 저장된 인덱스를 사용하여 집게에 과일을 생성
        GameObject fruitPrefab = fruitPrefabs[nextFruitIndex];
        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        spawnedFruit.transform.SetParent(spawnPoint, false); // 집게를 부모로 설정
        spawnedFruits.Enqueue(spawnedFruit); // 생성된 과일을 큐에 추가

        // 다음 과일을 위해 새로운 인덱스를 준비
        PrepareNextFruit();
    }

    void UpdateNextFruitUI()
    {
        // nextFruit UI에 표시될 과일의 스프라이트를 업데이트
        Sprite nextFruitSprite = fruitPrefabs[nextFruitIndex].GetComponent<SpriteRenderer>().sprite;
        nextFruitUIImage.sprite = nextFruitSprite; // UI 이미지 컴포넌트에 스프라이트를 설정
    }

    public void Reset()
    {
        while (spawnedFruits.Count > 0)
        {
            GameObject fruitToDestroy = spawnedFruits.Dequeue();
            Destroy(fruitToDestroy);
        }

        PrepareNextFruit();
        UpdateNextFruitUI();
    }
}

