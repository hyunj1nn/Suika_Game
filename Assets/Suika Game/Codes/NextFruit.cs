using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFruit : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // 생성될 과일 프리팹들의 배열
    public Transform spawnPoint; // 과일이 스폰될 포인트
    private GameObject nextFruit; // 다음에 생성될 과일 오브젝트

    // 다음 과일을 선택하고 집게에 표시하는 메서드
    public void SelectNextFruit()
    {
        // 다음 과일 인덱스를 랜덤으로 결정
        int nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        // 다음 과일 오브젝트를 선택
        nextFruit = fruitPrefabs[nextFruitIndex];
    }

    // 마우스 클릭 이벤트에 반응하여 과일 생성
    public void OnMouseClick()
    {
        // 1초 후에 과일을 생성하는 코루틴 시작
        StartCoroutine(SpawnFruitAfterDelay());
    }


    private IEnumerator SpawnFruitAfterDelay()
    {
        // 1초 기다림
        yield return new WaitForSeconds(1f);

        // 선택된 과일 프리팹으로부터 새 과일 오브젝트를 생성
        Instantiate(nextFruit, spawnPoint.position, spawnPoint.rotation);

        // 다음 과일을 다시 선택
        SelectNextFruit();
    }

    // Start에서 다음 과일을 선택
    void Start()
    {
        SelectNextFruit();
    }
}
