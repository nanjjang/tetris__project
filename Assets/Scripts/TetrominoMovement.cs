using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 1.0f;                // Inspector에서 조절
    public static int height = 25;
    public static int width = 10;
    public Vector3 rotationPoint;                // 회전 중심점 (Inspector에서 세팅)

    private static Transform[,] grid = new Transform[width, height];

    void Start()
    {
        // 초기화 로직 필요 시 여기에
    }

    void Update()
    {
        // 좌/우 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
            if (!ValidMove())
                transform.position -= Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
            if (!ValidMove())
                transform.position -= Vector3.right;
        }
        // 회전
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90);
        }
        // 하드 드롭
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
            return;
        }

        // 아래로 이동
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            MoveDown();
            previousTime = Time.time;
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            // 현재 블록 비활성화 후 spawnertetris 에서 홀드 처리
            FindObjectOfType<spawnertetris>().Hold();
            this.enabled = false;
            return;
        }
        
    }

    // 한 칸 아래로 이동 처리
    void MoveDown()
    {
        transform.position += Vector3.down;
        if (!ValidMove())
        {
            transform.position -= Vector3.down;
            LockTetromino();
        }
    }

    // 하드 드롭: 바닥까지 즉시 이동
    void HardDrop()
    {
        // 가능한 최대 거리만큼 아래로 이동
        while (true)
        {
            transform.position += Vector3.down;
            if (!ValidMove())
            {
                transform.position -= Vector3.down;
                break;
            }
        }
        LockTetromino();
    }

    // 블록 잠그고 새 블록 소환, 라인 체크
    void LockTetromino()
    {
        AddToGrid();                                     // 그리드에 추가
        this.enabled = false;                            // 이 블록 비활성화
        FindObjectOfType<spawnertetris>().NewTetris();   // 새 블록 소환
        CheckForLines();                                 // 라인 체크 및 제거
    }

    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;
            if (grid[x, y] != null)
                return false;
        }
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            grid[x, y] = child;
        }
    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                i++; // 한 줄 내린 뒤 같은 i 인덱스 재검사
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
            if (grid[j, i] == null) return false;
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height - 1; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y + 1] != null)
                {
                    grid[j, y] = grid[j, y + 1];
                    grid[j, y + 1] = null;
                    grid[j, y].position -= Vector3.up;
                }
            }
        }
    }
}
