using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.UI.Extensions;

public class UILine : MonoBehaviour {

    public GameObject goStage1;
    public GameObject goStage2;

    Transform stage1;
    Transform stage2;
    public float speed;

    public Transform target;
    public Camera cam;

    public UILineRenderer lr;

    public List<Vector2> v2Points;

    public bool bDraw;
    // Start is called before the first frame update
    void Start() {
        var point = new Vector2() { x = 0.0f, y = 0.0f };
        var pointlist = new List<Vector2>(lr.Points);
        pointlist.Add(point);
        pointlist.Add(point);
        lr.Points = pointlist.ToArray();
        bDraw = false;
    }

    public void DrawLine() {
        bDraw = true;
        lr.enabled = true;
    }

    public void StopDrawingLine() {
        bDraw = false;
        lr.enabled = false;
    }

    private void FixedUpdate() {
        if (!bDraw) {
            return;
        }
        stage1 = goStage1.transform;
        stage2 = goStage2.transform;
        Vector3 screenPos = cam.WorldToScreenPoint(stage2.position);
        lr.Points[1].x = screenPos.x - this.transform.position.x;
        lr.Points[1].y = screenPos.y - this.transform.position.y * 1.25f;
    }
}
