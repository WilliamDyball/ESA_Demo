using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public Transform center;
    public Vector3 axis;
    private Vector3 desiredPosition;
    private float radius = 0.0f;
    private float radiusSpeed = 0.0f;
    public float rotationSpeed;

    private void OnEnable() {
        axis = center.up;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
    }

    void Update() {
        if (SceneController.instance.bSelected || !SceneController.instance.bReady) {
            return;
        }
        transform.RotateAround(center.position, axis, -rotationSpeed * Time.deltaTime);
        desiredPosition = (transform.position - center.position).normalized * radius + center.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }
}