using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMove : MonoBehaviour
{
    public static ClawMove Instance; // �̱��� �ν��Ͻ�
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶��� ������ �����ɴϴ�.
    }

    void Update()
    {
        // GameManager�� �ν��Ͻ��� ���� ������ �Ͻ� ���� �������� Ȯ��
        if (GameManager.Instance.IsPaused)
        {
            return; // ������ �Ͻ� ���� �����̸� �Ʒ��� �ڵ带 �������� ����
        }

        // ���� ī�޶� ��ȿ���� Ȯ��
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
            if (mainCamera == null)
            {
                return;
            }
        }

        Vector3 mousePosition = Input.mousePosition; // ���콺�� ���� ��ġ�� ������

        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        // ���԰� X������ �̵��ϱ� ���� Z���� ������ ���� ��ǥ Z������ ����
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

        // ���� ��ǥ�� ��ȯ�� ���콺 ��ġ�� ������ ��ġ�� ������Ʈ
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Y���� ������ ���·� ����
        targetPosition.y = transform.position.y;

        // ���԰� ������ �� �ִ� X���� �ּ� �� �ִ� �� ����
        float minX = (float)-2.8; // ��: ������ ���� ���
        float maxX = (float)2.8; // ��: ������ ������ ���

        // X ��ġ�� �ּ� �� �ִ� ������ ����
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

        transform.position = targetPosition;
    }
}
