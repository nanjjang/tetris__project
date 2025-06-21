using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board i;
    bool[,] blockArr = new bool[22, 10];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        i = this;
    }

    public bool BoardCheckX(Vector2[] p, int x)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (Mathf.RoundToInt(p[i].x) + x < 0 || Mathf.RoundToInt(p[i].x) + x > blockArr.GetLength(1) - 1 || blockArr[22 - Mathf.RoundToInt(p[i].y), Mathf.RoundToInt(p[i].x) + x] == true)
            {
                return false;
            }
        }
        return true;
    }

    public bool BoardCheckY(Vector2[] p)
    {
        for (int i = 0; i < p.Length; i++)
        {

            if (22 - Mathf.RoundToInt(p[i].y) >= blockArr.GetLength(0) - 1 || blockArr[22 - (Mathf.RoundToInt(p[i].y - 1)), Mathf.RoundToInt(p[i].x)] == true)
            {
                for (int j = 0; j < p.Length; j++)
                {
                    blockArr[22 - (Mathf.RoundToInt(p[i].y)), Mathf.RoundToInt(p[j].x)] = true;
                }

                return false;
            }
        }
        return true;
    }

    
}
