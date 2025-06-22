using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 10;
    public int height = 20;
    public Vector2 spawnPosition = new Vector2(5, 19);
    public static Transform[,] grid;

    void Awake()
    {
        grid = new Transform[width, height];
    }

    // (선택) 씬 뷰에 그리드 선 보기
    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                Gizmos.DrawWireCube(new Vector3(x, y, 0), Vector3.one);
    }
}