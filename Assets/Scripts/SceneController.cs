using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class SceneController : MonoBehaviour {

    public UIFollow follow;
    public UILineRenderer lr;

    public Text txtName;
    public Text txtInfo;

    public GameObject goEarth;
    public GameObject goSelected;
    public bool bSelected = false;
    public bool bReady = true;
    bool bDrawingSelectable = false;
    bool bDrawingPOI = false;

    public Image image1;
    public Image image2;

    //Singleton
    public static SceneController instance;

    private void Awake() {
        if (SceneController.instance == null) {
            SceneController.instance = this;
        } else {
            if (SceneController.instance != this) {
                Destroy(SceneController.instance.gameObject);
                SceneController.instance = this;
            }
        }
        txtName.text = "";
        txtName.enabled = false;
        txtInfo.text = "";
        txtInfo.enabled = false;
    }

    private void FixedUpdate() {
        if (bSelected) {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2)) {
                if (hit.collider.CompareTag("POI")) {
                    Debug.Log("POI found!");
                    float dist = Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position);
                    Debug.Log(dist);
                    if (dist < 1.0f) {
                        txtName.text = hit.collider.GetComponent<InformationContainer>().strName;
                        txtInfo.text = hit.collider.GetComponent<InformationContainer>().strInfo;
                        Debug.Log("Target pos: " + hit.collider.transform.position);
                        follow.collider = hit.collider;
                        follow.bCol = true;
                    } else if (bDrawingPOI && !bDrawingSelectable) {
                        txtName.text = goSelected.GetComponent<InformationContainer>().strName;
                        txtInfo.text = goSelected.GetComponent<InformationContainer>().strInfo;
                        follow.collider = null;
                        follow.bCol = false;
                    }
                } else if (hit.collider.CompareTag("Planet") && bDrawingPOI && !bDrawingSelectable) {
                    txtName.text = goSelected.GetComponent<InformationContainer>().strName;
                    txtInfo.text = goSelected.GetComponent<InformationContainer>().strInfo;
                    follow.collider = null;
                    follow.bCol = false;
                }
            }
        }
    }

    public void OnSelected(GameObject _goSelected) {
        if (!bSelected) {
            goSelected = _goSelected;
            txtName.enabled = true;
            txtName.text = _goSelected.GetComponent<InformationContainer>().strName;
            txtInfo.enabled = true;
            txtInfo.text = _goSelected.GetComponent<InformationContainer>().strInfo;
            follow.goTarget = _goSelected;
            lr.enabled = true;
            bSelected = true;
            follow.collider = null;
            follow.bCol = false;
            image1.enabled = true;
            image2.enabled = true;
        }
    }

    public void OnDeselected() {
        txtName.text = "";
        txtName.enabled = false;
        txtInfo.text = "";
        txtInfo.enabled = false;
        lr.enabled = false;
        goSelected = null;
        bSelected = false;
        image1.enabled = false;
        image2.enabled = false;
    }

}
