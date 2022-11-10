using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLookCamera_Manager : MonoBehaviour
{
    [SerializeField] private Camera gameplayCamera;
    [SerializeField] private float PointPosInitX;
    [SerializeField] private Transform playerPos;

    float ValXAtAngle;
    float ValZAtAngle;

    // Start is called before the first frame update
    void Start()
    {
        ValXAtAngle = PointPosInitX / 90;
        ValZAtAngle = PointPosInitX / 90;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(playerPos.localPosition.x + ValueX(), playerPos.localPosition.y, playerPos.localPosition.z + ValueZ());
    }

    float ValueX()
    {
        float CameraAngle = gameplayCamera.transform.eulerAngles.y;
        float ValXOnGrade = CameraAngle * ValXAtAngle;
        float ValActualX = PointPosInitX - ValXOnGrade;

        if (ValActualX > 0 && CameraAngle > 90 && CameraAngle <= 180)
        {
            ValActualX *= -1;
        }
        else if (CameraAngle > 180 && CameraAngle <= 360)
        {
            float difference = ValActualX + PointPosInitX;
            ValActualX = (difference * -1) - PointPosInitX;
        }

        return ValActualX;
    }

    float ValueZ()
    {
        float CameraAngle = gameplayCamera.transform.eulerAngles.y;
        float ValActualZ = CameraAngle * ValZAtAngle;

        if(CameraAngle <= 90)
        {
            ValActualZ *= -1;
        }
        if (CameraAngle > 90 && CameraAngle <= 270)
        {
            ValActualZ -= PointPosInitX * 2;
        }
        else if (CameraAngle > 270 && CameraAngle <= 360)
        {
            float difference = ValActualZ - (PointPosInitX * 3);
            ValActualZ = PointPosInitX - difference;
        }

        return ValActualZ;
    }
}
