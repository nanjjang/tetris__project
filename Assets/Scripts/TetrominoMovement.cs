using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    [SerializeField] float movePos = 1; // 좌우 아래 이동간격
    [SerializeField] float downRate = 1; //아래 이동 시간 간격
    public bool isMove = true;

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
        if (Input.GetKeyDown(KeyCode.A)) MoveX(-1);
        else if (Input.GetKeyDown(KeyCode.W)) Rotation();
        else if (Input.GetKeyDown(KeyCode.D)) MoveX(+1);
        else if (Input.GetKeyDown(KeyCode.S)) MoveY();

        if (Input.GetKeyDown(KeyCode.Space)) Rotation();

    }

    void MoveX(float x) //좌우 이동
    {
        x = x * movePos;
        transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
    }

    void MoveDown() // 아래 이동
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - movePos, 0);
    }

    void MoveY() // 키 입력 아래 이동
    {
        MoveDown();

        if (mDownCol != null) StopCoroutine(mDownCol);

        mDownCol = MoveDownCor();
        StartCoroutine(mDownCol);
    }

    IEnumerator MoveDownCor()
    {
        while (isMove)
        {
            yield return new WaitForSeconds(downRate);
            MoveDown();
        }
    }

    void Rotation()
    {
        if (rotPos.Length == 0) return;

        rot++;
        if (rot > ROTATION.ROT270) rot = ROTATION.ROT0;

        block.localPosition = rotPos[(int)rot%2];

        block.localRotation = Quaternion.Euler(0, 0, int.Parse(rot.ToString().Substring(3)));
    }

    public enum ROTATION
    {
        ROT0,
        ROT90,
        ROT180,
        ROT270
    }
}
