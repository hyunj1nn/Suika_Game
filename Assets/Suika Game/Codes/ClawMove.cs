using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMove : MonoBehaviour
{
    public static ClawMove Instance; // 싱글톤 인스턴스
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라의 참조를 가져옵니다.
    }

    void Update()
    {
        // GameManager의 인스턴스를 통해 게임이 일시 정지 상태인지 확인
        if (GameManager.Instance.IsPaused)
        {
            return; // 게임이 일시 정지 상태이면 아래의 코드를 실행하지 않음
        }

        // 메인 카메라가 유효한지 확인
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
            if (mainCamera == null)
            {
                return;
            }
        }

        Vector3 mousePosition = Input.mousePosition; // 마우스의 현재 위치를 가져옴

        // 마우스 위치를 월드 좌표로 변환
        // 집게가 X축으로 이동하기 위해 Z값을 집게의 월드 좌표 Z값으로 설정
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

        // 월드 좌표로 변환된 마우스 위치로 집게의 위치를 업데이트
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Y축은 고정된 상태로 유지
        targetPosition.y = transform.position.y;

        // 집게가 움직일 수 있는 X축의 최소 및 최대 값 설정
        float minX = (float)-2.8; // 예: 상자의 왼쪽 경계
        float maxX = (float)2.8; // 예: 상자의 오른쪽 경계

        // X 위치를 최소 및 최대 값으로 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        transform.position = targetPosition;
    }
}
