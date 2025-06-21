using UnityEngine;

public class g : MonoBehaviour
{
    void Update()
    {
        float gridSize = 1.0f; 
        float roundedX = Mathf.Round(transform.position.x / gridSize) * gridSize;
        float roundedY = Mathf.Round(transform.position.y / gridSize) * gridSize;
        transform.position = new Vector3(roundedX, roundedY, transform.position.z);
    }
}
