using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    void Start()
    {
        Vector3 cameraPosition = new Vector3(9.5f, 9.5f, -10);
        transform.position = cameraPosition;
    }

    public void PositionCamera(int x, int y)
    {
        Vector3 cameraPosition = new Vector3(x, y, -10);
        transform.position = cameraPosition;
    }
}
