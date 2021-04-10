using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField]
    private Transform rotatingParts;
    [SerializeField]
    private float rotateSpeed;

    public void Awake()
    {
        if (rotateSpeed == 0)
            Debug.LogError("Rotate speed too low");
        if (rotatingParts == null)
            Debug.LogError("Missing rotating parts");
    }
    public void RotateToObject(Transform target)
    {
        Vector3 dir = target.position - rotatingParts.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = Quaternion.Lerp(rotatingParts.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        //rotatingParts.rotation = Quaternion.Euler(0f, rotation.y + 1f, 0f);
        rotatingParts.rotation = Quaternion.Slerp(rotatingParts.rotation, lookRotation, rotateSpeed * Time.deltaTime);
    }
}