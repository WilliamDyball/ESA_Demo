using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformStore : MonoBehaviour {
    
    public Transform transform;

    private void Start() {
        transform = GetComponent<Transform>().transform;
    }

    private void OnEnable() {
        transform = GetComponent<Transform>().transform;
    }

    public void StoreTransform() {
        transform = GetComponent<Transform>().transform;
    }

    public void RestoreTransform() {
        GetComponent<Transform>().transform.rotation = transform.rotation;
    }
}
