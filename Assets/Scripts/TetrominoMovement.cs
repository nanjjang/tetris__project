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

    void Awake()
    {
        //컴퍼넌트 초기화
    }

    void Update()
    {
        // 좌/우 이동
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        // 회전
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90);
        }

        // 아래로 자동 이동 (DownArrow 누르면 빠르게)
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();                                     // 그리드에 추가
                this.enabled = false;                            // 이 블록 비활성화
                FindObjectOfType<spawnertetris>().NewTetris();   // 새 블록 소환
                CheckForLines();                                 // 라인 체크 및 제거
            }

            previousTime = Time.time;
        }
    }

    // 유효한 위치인지 검사
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

    // 그리드에 블록 추가
    void AddToGrid()
    {
        foreach (Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
            grid[x, y] = child;
        }
    }

    // 꽉 찬 줄 검사 & 제거
    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
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
        GameManager.Instance.Plus();
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].position -= Vector3.up;
                }
            }
        }
    }
}