using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class spawnertetris : MonoBehaviour
{
    [Header("블록 프리팹 목록")]
    public GameObject[] tetrominoes;
    [Header("소환 위치")]
    public Transform spawnPoint;

    // 보관용
    private GameObject holdPrefab = null;
    private bool canHold = true;

    // 현재 블록
    private GameObject currentPrefab;
    private GameObject currentInstance;

    // 7-베그 버거(7-bag) 큐
    private Queue<int> bagQueue = new Queue<int>();

    void Start()
    {
        RefillBag();
        SpawnNext();
    }

    // 7-bag 채우기
    void RefillBag()
    {
        var list = Enumerable.Range(0, tetrominoes.Length).ToList();
        // 셔플
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
        foreach (var idx in list)
            bagQueue.Enqueue(idx);
    }

    // 다음 블록 소환
    void SpawnNext()
    {
        if (bagQueue.Count == 0) RefillBag();

        int idx = bagQueue.Dequeue();
        currentPrefab = tetrominoes[idx];
        currentInstance = Instantiate(currentPrefab, spawnPoint.position, Quaternion.identity);
        currentInstance.GetComponent<TetrominoMovement>().enabled = true;

        canHold = true; // 새로운 블록이 소환되면 다시 홀드 가능
    }

    // 하드드롭이나 라인이 지워진 뒤 호출됨
    public void NewTetris()
    {
        SpawnNext();
    }

    // 홀드 동작
    public void Hold()
    {
        if (!canHold) return;    // 이미 홀드 사용했으면 무시
        canHold = false;

        // 첫 홀드라면 보관통에 저장 후 새 블록 소환
        if (holdPrefab == null)
        {
            holdPrefab = currentPrefab;
            Destroy(currentInstance);
            SpawnNext();
        }
        else
        {
            // 보관되어 있던 블록과 현재 블록 교체
            GameObject temp = holdPrefab;
            holdPrefab = currentPrefab;

            Destroy(currentInstance);

            currentPrefab = temp;
            currentInstance = Instantiate(currentPrefab, spawnPoint.position, Quaternion.identity);
            currentInstance.GetComponent<TetrominoMovement>().enabled = true;
        }

        // (선택) UI 보관 슬롯 갱신
        UpdateHoldDisplay();
    }

    void UpdateHoldDisplay()
    {
        // 보관 UI(이미지) 갱신 로직 넣기
    }
}

    // 홀드 동작
    public void Hold()
    {
        if (!canHold) return;    // 이미 홀드 사용했으면 무시
        canHold = false;

        // 첫 홀드라면 보관통에 저장 후 새 블록 소환
        if (holdPrefab == null)
        {
            holdPrefab = currentPrefab;
            Destroy(currentInstance);
            SpawnNext();
        }
        else
        {
            // 보관되어 있던 블록과 현재 블록 교체
            GameObject temp = holdPrefab;
            holdPrefab = currentPrefab;

            Destroy(currentInstance);

            currentPrefab = temp;
            currentInstance = Instantiate(currentPrefab, spawnPoint.position, Quaternion.identity);
            currentInstance.GetComponent<TetrominoMovement>().enabled = true;
        }

        // (선택) UI 보관 슬롯 갱신
        UpdateHoldDisplay();
    }


