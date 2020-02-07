using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public Transform center;
    public Vector3 axis;
    private Vector3 desiredPosition;
    public float radius = 2.0f;
    public float radiusSpeed = 1f;
    public float rotationSpeed = 80.0f;

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