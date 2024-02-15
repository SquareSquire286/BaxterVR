using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class WallMovementModule : MonoBehaviour
{
    public GameObject leftWall, rightWall, backWall;

    private float leftWallMin, rightWallMin, leftWallMax, rightWallMax, backWallMin, backWallMax;

    // Start is called before the first frame update
    void Start()
    {
        leftWallMin = 6f;
        rightWallMin = -15f;
        leftWallMax = 15f;
        rightWallMax = -7.5f;
        backWallMin = -30f;
        backWallMax = -18f;
    }

    // Update is called once per frame
    void Update()
    {
        // OLD CODE THAT TESTED LOCAL TRANSFORM MANIPULATIONS FOR ALL THREE MOVABLE WALLS
        /* if (Input.GetKey(KeyCode.A))
            leftWall.transform.localPosition = new Vector3(Mathf.Clamp(leftWall.transform.localPosition.x - 0.05f, leftWallMin, leftWallMax), leftWall.transform.localPosition.y, leftWall.transform.localPosition.z);

        if (Input.GetKey(KeyCode.D))
            leftWall.transform.localPosition = new Vector3(Mathf.Clamp(leftWall.transform.localPosition.x + 0.05f, leftWallMin, leftWallMax), leftWall.transform.localPosition.y, leftWall.transform.localPosition.z);

        if (Input.GetKey(KeyCode.Q))
            rightWall.transform.localPosition = new Vector3(Mathf.Clamp(rightWall.transform.localPosition.x + 0.05f, rightWallMin, rightWallMax), rightWall.transform.localPosition.y, rightWall.transform.localPosition.z);

        if (Input.GetKey(KeyCode.E))
            rightWall.transform.localPosition = new Vector3(Mathf.Clamp(rightWall.transform.localPosition.x - 0.05f, rightWallMin, rightWallMax), rightWall.transform.localPosition.y, rightWall.transform.localPosition.z);

        if (Input.GetKey(KeyCode.W))
            backWall.transform.localPosition = new Vector3(backWall.transform.localPosition.x, backWall.transform.localPosition.y, Mathf.Clamp(backWall.transform.localPosition.z - 0.05f, backWallMin, backWallMax));

        if (Input.GetKey(KeyCode.S))
            backWall.transform.localPosition = new Vector3(backWall.transform.localPosition.x, backWall.transform.localPosition.y, Mathf.Clamp(backWall.transform.localPosition.z + 0.05f, backWallMin, backWallMax));*/
    }

    public void UpdateWall(GameObject wallToUpdate, float newPos, float minPos, float maxPos)
    {
        if (wallToUpdate == leftWall)
            leftWall.transform.localPosition = new Vector3((((newPos - minPos) / (maxPos - minPos)) * (leftWallMax - leftWallMin) + leftWallMin), leftWall.transform.localPosition.y, leftWall.transform.localPosition.z);

        else if (wallToUpdate == rightWall)
            rightWall.transform.localPosition = new Vector3((((newPos - minPos) / (maxPos - minPos)) * (rightWallMax - rightWallMin) + rightWallMin), rightWall.transform.localPosition.y, rightWall.transform.localPosition.z);

        else if (wallToUpdate == backWall)
            backWall.transform.localPosition = new Vector3(backWall.transform.localPosition.x, backWall.transform.localPosition.y, (((newPos - minPos) / (maxPos - minPos)) * (backWallMax - backWallMin) + backWallMin));

        else return;

    }
}
