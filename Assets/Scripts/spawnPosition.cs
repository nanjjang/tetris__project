using UnityEngine;

public class spawnPosition : MonoBehaviour
{
    public GameObject[] Tetris;
    Board board;

    void Start()
    {
        board = FindObjectOfType<Board>();
        NewTetris();
    }

    public void NewTetris()
    {
        Vector2 spawn = board.spawnPosition;            // Board에서 가져온다
        Instantiate(
            Tetris[Random.Range(0, Tetris.Length)],
            new Vector3(spawn.x, spawn.y, 0f),          // Z는 0으로
            Quaternion.identity
        );
    }
}

