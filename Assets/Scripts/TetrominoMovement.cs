using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] float movePos = 1; // 좌우 아래 이동간격
    [SerializeField] float downRate = 1; //아래 이동 시간 간격
=======
    [SerializeField] float movePos = 1f; // 좌우 및 아래 이동 단위
    [SerializeField] float downRate = 1f; // 자동 아래 이동 시간 간격
    [SerializeField] Vector2[] rotPos = new Vector2[4]; // 0, 90, 180, 270도 회전 보정값
    [SerializeField] Vector2 pos;

    Vector2 pOffset = new Vector2(-0.5f, 0.5f);

>>>>>>> Stashed changes
    public bool isMove = true;
    public GameObject[] blocks = new GameObject[4];
    public List<Transform> listB = new List<Transform>();

    IEnumerator mDownCol;
    ROTATION rot = ROTATION.ROT0;
    [SerializeField] Vector2[] rotPos; //각도 보정치

    Transform block;

    void Start()
    {
<<<<<<< Updated upstream
        block = transform.GetChild(0);
        MoveY();
=======
        block = transform.GetChild(0); // 자식 오브젝트 (블록) 받아오기
        mDownCol = MoveDownCor();
        StartCoroutine(mDownCol);

        for (int i = 0; i < 4; i++)
            listB.Add(blocks[i].transform);
>>>>>>> Stashed changes
    }

    void Update()
    {
        if (!isMove) return;
        if (Input.GetKeyDown(KeyCode.A)) MoveX(-1);
<<<<<<< Updated upstream
        else if (Input.GetKeyDown(KeyCode.W)) Rotation();
        else if (Input.GetKeyDown(KeyCode.D)) MoveX(+1);
        else if (Input.GetKeyDown(KeyCode.S)) MoveY();

        if (Input.GetKeyDown(KeyCode.Space)) Rotation();

    }

    void MoveX(float x) //좌우 이동
    {
=======
        else if (Input.GetKeyDown(KeyCode.D)) MoveX(1);
        else if (Input.GetKeyDown(KeyCode.W)) Rotation();
        else if (Input.GetKeyDown(KeyCode.S)) MoveY();

    }

    void MoveX(float x)
    {
        Vector2[] p = new Vector2[listB.Count];
        for (int i = 0; i < p.Length; i++)

        pos.x += x;
>>>>>>> Stashed changes
        x = x * movePos;
        transform.position = new Vector3(transform.position.x + x, transform.position.y, 0);
    }

    void MoveDown() // 아래 이동
    {
<<<<<<< Updated upstream
=======
        pos.y++;
>>>>>>> Stashed changes
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
