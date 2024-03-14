using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    //public static Fruit instance;
    public FruitType fruitType; // ���� ������ Ÿ��
    public GameObject nextFruit; // �� ���ϰ� �浹�� �� ������ ���� ������ ������
    public GameObject combineEffectPrefab; // ������ �� ������ ����Ʈ�� ������

    private float creationTime; // ���� �ð�, �浹 ó���� ���� Ÿ�ӽ�����

    public enum FruitType
    {
        Cherry, Strawberry, Grape, Grapefruit, Tomato, Orange, Fig, Peach, Melon, Watermelon, Goldenwatermelon
    }

    void Start()
    {
        // ������ ������ �� ���� �ð����� Ÿ�ӽ������� ����
        creationTime = Time.timeSinceLevelLoad;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // �浹�� ������Ʈ�� Fruit ��ũ��Ʈ�� ������ �ִ��� Ȯ��
        Fruit otherFruit = col.collider.GetComponent<Fruit>();
        if (otherFruit != null && otherFruit.fruitType == this.fruitType)
        {
            // �� ������ Watermelon Ÿ���̸� ���ο� ������ ����X
            if (this.fruitType == FruitType.Goldenwatermelon)
            {
                return; // �Լ��� �ٷ� ����
            }

            // ���� Ÿ���� ���ϰ� �浹�߰�, �� ���� ������ ���� ������ ������ ���� ���Ϸ� ����
            if (creationTime < otherFruit.creationTime)
            {
                Score.Instance.AddScore(this.fruitType);  // ������ ������ �� ���� �߰�

                // ���� ���ܳ��� ������ ��ġ�� �ΰ��� ������ �߾ӿ� ��ġ
                Vector3 spawnPosition = (transform.position + otherFruit.transform.position) / 2;

                // ���ο� ���� ���� ��, ���� ���ϰ� �ٸ� ������ �ı����� �ʰ� ��Ȱ��ȭ�� ����
                otherFruit.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

                // ���ο� ������ ������ ��, ��Ȱ��ȭ�� ���� ��ü�� �ı�
                Destroy(otherFruit.gameObject, 0.1f); // �ణ�� ������ �־� �ı�
                Destroy(this.gameObject, 0.1f); // �ణ�� ������ �־� �ı�

                if (nextFruit != null)
                {
                    GameObject newFruit = Instantiate(nextFruit, spawnPosition, Quaternion.identity);
                    // ���ο� ������ �±׸� "CrashedFruit"���� ����
                    newFruit.tag = "FreshFruit";
                    // ���� ������ ���� �ܰ��� ������ �߷��� ����
                    Rigidbody2D newFruitRigidbody = newFruit.GetComponent<Rigidbody2D>();
                    if (newFruitRigidbody != null)
                    {
                        newFruitRigidbody.gravityScale = 1.3f;
                    }

                    // ���ο� ������ �������� �� ȿ���� ���
                    AudioSource newFruitSound = newFruit.GetComponent<AudioSource>();
                    if (newFruitSound != null)
                    {
                        newFruitSound.Play();
                    }
                    // ���ο� ������ �������� �� ����Ʈ ���
                    if (combineEffectPrefab != null)
                    {
                        Instantiate(combineEffectPrefab, transform.position, Quaternion.identity);
                    }
                }

                // ���� ���ϰ� �浹�� ������ �ı�
                Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
