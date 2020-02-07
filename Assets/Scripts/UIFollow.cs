using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollow : MonoBehaviour {
    public GameObject goTarget;
    public Camera cam;

    public Collider collider;

    public bool bCol;

    private void Awake() {
        bCol = false;
    }

    // Update is called once per frame
    void Update() {
        if (goTarget == null) {
            return;
        }
        if (bCol) {
            this.transform.position = cam.WorldToScreenPoint(collider.transform.position);
        } else {
            this.transform.position = cam.WorldToScreenPoint(goTarget.transform.position);
        }
    }
}
