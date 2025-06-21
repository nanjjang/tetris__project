using UnityEngine;

public class spawnertetris : MonoBehaviour
{
    [Tooltip("테트로미노 프리팹 배열")]
    public GameObject[] TetrisPrefabs;

    [Tooltip("Hierarchy에 만든 SpawnPoint 오브젝트")]
    public Transform spawnPoint;

    void Start()
    {
        NewTetris();
    }

    public void NewTetris()
    {
        Vector3 worldPos = new Vector3(5f, 21f, 0f);
        Instantiate(
            TetrisPrefabs[Random.Range(0, TetrisPrefabs.Length)],
            worldPos,
            Quaternion.identity
        );
    }
}






