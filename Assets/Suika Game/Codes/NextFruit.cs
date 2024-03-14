using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFruit : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // ������ ���� �����յ��� �迭
    public Transform spawnPoint; // ������ ������ ����Ʈ
    private GameObject nextFruit; // ������ ������ ���� ������Ʈ

    // ���� ������ �����ϰ� ���Կ� ǥ���ϴ� �޼���
    public void SelectNextFruit()
    {
        // ���� ���� �ε����� �������� ����
        int nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        // ���� ���� ������Ʈ�� ����
        nextFruit = fruitPrefabs[nextFruitIndex];
    }

    // ���콺 Ŭ�� �̺�Ʈ�� �����Ͽ� ���� ����
    public void OnMouseClick()
    {
        // 1�� �Ŀ� ������ �����ϴ� �ڷ�ƾ ����
        StartCoroutine(SpawnFruitAfterDelay());
    }


    private IEnumerator SpawnFruitAfterDelay()
    {
        // 1�� ��ٸ�
        yield return new WaitForSeconds(1f);

        // ���õ� ���� ���������κ��� �� ���� ������Ʈ�� ����
        Instantiate(nextFruit, spawnPoint.position, spawnPoint.rotation);

        // ���� ������ �ٽ� ����
        SelectNextFruit();
    }

    // Start���� ���� ������ ����
    void Start()
    {
        SelectNextFruit();
    }
}
