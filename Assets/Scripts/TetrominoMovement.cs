using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    [SerializeField] float movePos = 1; // 좌우 아래 이동간격
    [SerializeField] float downRate = 1; //아래 이동 시간 간격
    public bool isMove = true;
    public GameObject SpawnP;

    IEnumerator mDownCol;
    ROTATION rot = ROTATION.ROT0;
    [SerializeField] Vector2[] rotPos; //각도 보정치

    Transform block;

    void Start()
    {
        block = transform.GetChild(0);
        MoveY();
    }

    void Update()
    {
        if (!isMove) return;
        InputK();
    }

    void InputK()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveX(-1);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Rotation();
        else if (Input.GetKeyDown(KeyCode.RightArrow)) MoveX(+1);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) MoveY();
        else if (Input.GetKeyDown(KeyCode.Space)) HardDrop();
    }

    void MoveX(float x) //좌우 이동
    {
        x = x * movePos;
        transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
    }

    void MoveY() // 키 입력 아래 이동 // 병합했음
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - movePos, 0);

        if (mDownCol != null) StopCoroutine(mDownCol);

        mDownCol = MoveDownCor();
        StartCoroutine(mDownCol);
    }

    IEnumerator MoveDownCor()
    {
        while (isMove)
        {
            yield return new WaitForSeconds(downRate);
            //MoveDown();
            MoveY();
        }
    }

    void HardDrop() //gpt
    {
        if (!isMove) return;

        // 현재 위치에서 아래로 한 칸씩 내려가며 충돌 확인
        while (CheckValidPosition(transform.position + Vector3.down * movePos))
        {
            transform.position += Vector3.down * movePos;
        }

        // 이후 자동 하강 멈추고, 고정 처리
        isMove = false;
        if (mDownCol != null)
            StopCoroutine(mDownCol);
    }

    void Rotation()
    {
        if (rotPos.Length == 0) return;

        rot++;
        if (rot > ROTATION.ROT270) rot = ROTATION.ROT0;

        block.localPosition = rotPos[(int)rot % 2];

        block.localRotation = Quaternion.Euler(0, 0, int.Parse(rot.ToString().Substring(3)));
    }

    public enum ROTATION
    {
        ROT0,
        ROT90,
        ROT180,
        ROT270
    }

    bool CheckValidPosition(Vector3 targetPos) // gpt
    {
        // 블록의 자식들(작은 블록 4개)에 대해 각각 충돌 확인
        foreach (Transform child in block)
        {
            Vector3 childTargetPos = targetPos + (child.position - transform.position);
            Collider2D hit = Physics2D.OverlapBox(childTargetPos, Vector2.one * 0.9f, 0);

            if (hit != null && hit.transform.parent != transform)
            {
                return false; // 다른 블록과 충돌
            }
        }

        return true;
    }

}