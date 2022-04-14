using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineManager : MonoBehaviour
{
    public float turnSpeed = 1f;

    Vector3 direction;
    Vector3 move;
    Coroutine turnCoroutine = null;
    Animator animator;
    Rigidbody rb;
    Transform tr;

    Vector3 targetAngle;
    bool isMove;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        targetAngle = tr.eulerAngles;
    }

    private void Update()
    {
        bool tmpMove = false;
        Vector3 tmpDir = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // 1. ���࿡ �÷��̾� Y�� ���� 0 �ƴϸ� 0 �ɶ����� ȸ��
            // 2. ������ ����
            tmpDir = tr.forward;
            targetAngle = Vector3.zero;
            tmpMove = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // 1. ���࿡ �÷��̾� Y �� ���� 180 �ƴϸ� 180�ɶ����� ȸ��
            // 2. �ڷ�����.
            tmpDir = -tr.forward;
            targetAngle = new Vector3(0, 180, 0);
            tmpMove = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 1. ���࿡ �÷��̾� Y �� ���� 270 �ƴϸ� 270�ɶ����� ȸ��
            // 2. �������� ����.
            tmpDir = (tmpDir - tr.right).normalized;
            targetAngle = new Vector3(0, -90, 0);
            tmpMove = true;

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // 1. ���࿡ �÷��̾� Y �� ���� 90 �ƴϸ� 90�ɶ����� ȸ��
            // 2. ���������� ����.
            tmpDir = (tmpDir + tr.right).normalized;
            targetAngle = new Vector3(0, 90, 0);
            tmpMove = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // �����ϱ�
        }
        isMove = tmpMove;

    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            targetAngle *= turnSpeed * Time.fixedDeltaTime;
            targetAngle += tr.eulerAngles;
            tr.eulerAngles = new Vector3(tr.eulerAngles.x,
                                         Mathf.Lerp(tr.eulerAngles.y, targetAngle.y, 5f),
                                         tr.eulerAngles.z);
        }
            
    }

}

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Fall,
}
