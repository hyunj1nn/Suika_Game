using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    //public static Fruit instance;
    public FruitType fruitType; // 현재 과일의 타입
    public GameObject nextFruit; // 이 과일과 충돌할 때 생성될 다음 과일의 프리팹
    public GameObject combineEffectPrefab; // 합쳐질 때 생성될 이펙트의 프리팹

    private float creationTime; // 생성 시간, 충돌 처리를 위한 타임스탬프

    public enum FruitType
    {
        Cherry, Strawberry, Grape, Grapefruit, Tomato, Orange, Fig, Peach, Melon, Watermelon, Goldenwatermelon
    }

    void Start()
    {
        // 과일이 생성될 때 현재 시간으로 타임스탬프를 설정
        creationTime = Time.timeSinceLevelLoad;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // 충돌한 오브젝트가 Fruit 스크립트를 가지고 있는지 확인
        Fruit otherFruit = col.collider.GetComponent<Fruit>();
        if (otherFruit != null && otherFruit.fruitType == this.fruitType)
        {
            // 두 과일이 Watermelon 타입이면 새로운 과일을 생성X
            if (this.fruitType == FruitType.Goldenwatermelon)
            {
                return; // 함수를 바로 종료
            }

            // 같은 타입의 과일과 충돌했고, 두 개의 과일중 먼저 생성된 과일이 다음 과일로 변경
            if (creationTime < otherFruit.creationTime)
            {
                Score.Instance.AddScore(this.fruitType);  // 과일이 합쳐질 때 점수 추가

                // 새로 생겨나는 과일의 위치를 두개의 과일의 중앙에 위치
                Vector3 spawnPosition = (transform.position + otherFruit.transform.position) / 2;

                // 새로운 과일 생성 시, 현재 과일과 다른 과일을 파괴하지 않고 비활성화만 진행
                otherFruit.gameObject.SetActive(false);
                this.gameObject.SetActive(false);

                // 새로운 과일이 생성된 후, 비활성화된 과일 객체를 파괴
                Destroy(otherFruit.gameObject, 0.1f); // 약간의 지연을 주어 파괴
                Destroy(this.gameObject, 0.1f); // 약간의 지연을 주어 파괴

                if (nextFruit != null)
                {
                    GameObject newFruit = Instantiate(nextFruit, spawnPosition, Quaternion.identity);
                    // 새로운 과일의 태그를 "CrashedFruit"으로 설정
                    newFruit.tag = "FreshFruit";
                    // 새로 생성된 다음 단계의 과일의 중력을 설정
                    Rigidbody2D newFruitRigidbody = newFruit.GetComponent<Rigidbody2D>();
                    if (newFruitRigidbody != null)
                    {
                        newFruitRigidbody.gravityScale = 1.3f;
                    }

                    // 새로운 과일이 생성됐을 때 효과음 재생
                    AudioSource newFruitSound = newFruit.GetComponent<AudioSource>();
                    if (newFruitSound != null)
                    {
                        newFruitSound.Play();
                    }
                    // 새로운 과일이 생성됐을 때 이펙트 재생
                    if (combineEffectPrefab != null)
                    {
                        Instantiate(combineEffectPrefab, transform.position, Quaternion.identity);
                    }
                }

                // 현재 과일과 충돌한 과일을 파괴
                Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
