using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance; // �̱��� �ν��Ͻ�
    public GameObject[] fruitPrefabs; // �����ϰ� ������ ���� �����յ��� �迭
    public Transform spawnPoint; // ���� ������Ʈ���� ���ϵ��� ������ ����Ʈ

    private Queue<GameObject> spawnedFruits = new Queue<GameObject>();

    public Image nextFruitUIImage; // ���� ������ ǥ���� UI �̹���
    private int nextFruitIndex; // ������ ������ ������ �ε���

    private float timeSinceLastSpawn = 0.0f; // ������ ���� ���� ������ �ð�
    public float spawnDelay = 1.0f; // ����Ŭ�� ������ �����ϱ� ���� ���� ���� �ð�

    void Start()
    {
        SpawnRandomFruit(); // ���� ���� �� ������ ������ ����
        PrepareNextFruit(); // ���� ���� �� ���� ������ �غ�
    }

    void Update()
    {
        // GameManager�� �ν��Ͻ��� ���� ������ �Ͻ� ���� �������� Ȯ��
        if (GameManager.Instance.IsPaused)
        {
            return; // ������ �Ͻ� ���� �����̸� �Ʒ��� �ڵ带 �������� ����
        }

        // ������ ���� ������ �ð� ������Ʈ
        timeSinceLastSpawn += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timeSinceLastSpawn >= spawnDelay) // ���콺 Ŭ�� �Է� ����
        {
            if (spawnedFruits.Count > 0)
            {
                GameObject spawnFruit = spawnedFruits.Dequeue(); // ������ ������ ������
                spawnFruit.transform.parent = null; // �θ�-�ڽ� ���� ����

                //spawnFruit.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                Rigidbody2D fruitRigidbody = spawnFruit.GetComponent<Rigidbody2D>();
                if (fruitRigidbody != null)
                {
                    fruitRigidbody.gravityScale = 1.3f; // ������ ������ �߷��� ����
                    StartCoroutine(ChangeTagAfterDelay(spawnFruit)); // �±� ���� �ڷ�ƾ ȣ��
                }
            }

            StartCoroutine(DelayedSpawn()); // ���콺 Ŭ���� �����Ǹ� DelayedSpawn �Լ��� �ҷ���
            timeSinceLastSpawn = 0.0f; // Ÿ�̸� ����
        }
    }

    IEnumerator ChangeTagAfterDelay(GameObject fruit)
    {
        yield return new WaitForSeconds(1f); // 1�� ��ٸ�
        if (fruit != null)
        {
            fruit.tag = "FreshFruit"; // �±� ����
            Debug.Log("�±� �����: " + fruit.tag);
        }
    }

    void Awake()
    {
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

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);  // spawnDelay �ڿ� SpawnRandomFruit �Լ��� �ҷ��� �������� ������ ����

        SpawnRandomFruit();
        PrepareNextFruit();
    }

    void PrepareNextFruit()
    {
        // ���� ���� �ε����� �������� ����������, ������ �������� �ʰ� ���常��
        nextFruitIndex = Random.Range(0, fruitPrefabs.Length);
        UpdateNextFruitUI(); // ���� ���� UI ������Ʈ
    }

    public void SpawnRandomFruit()
    {
        // spawnPoint�� �ڽ� ������Ʈ(����)�� �̹� �����ϴ��� Ȯ��
        if (spawnPoint.childCount > 0)
        {
            // spawnPoint�� ������ �����ϸ� ���ο� ������ �������� ����
            return;
        }

        // ����� �ε����� ����Ͽ� ���Կ� ������ ����
        GameObject fruitPrefab = fruitPrefabs[nextFruitIndex];
        GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        spawnedFruit.transform.SetParent(spawnPoint, false); // ���Ը� �θ�� ����
        spawnedFruits.Enqueue(spawnedFruit); // ������ ������ ť�� �߰�

        // ���� ������ ���� ���ο� �ε����� �غ�
        PrepareNextFruit();
    }

    void UpdateNextFruitUI()
    {
        // nextFruit UI�� ǥ�õ� ������ ��������Ʈ�� ������Ʈ
        Sprite nextFruitSprite = fruitPrefabs[nextFruitIndex].GetComponent<SpriteRenderer>().sprite;
        nextFruitUIImage.sprite = nextFruitSprite; // UI �̹��� ������Ʈ�� ��������Ʈ�� ����
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

