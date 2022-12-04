using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed = 1;
    public float zoomSpeed = 0.1f;


    [SerializeField] FixedJoystick joystick;
    [SerializeField] GameObject boundsGameObject;
    private Transform transform;
    private Transform boundsTransform;
    private Camera camera;
    private float currZoomChangeSpeed = 0f;
    public float zoom { get { return camera.orthographicSize; } set { camera.orthographicSize = value; } }

    private void Start() {
        transform = GetComponent<Transform>();
        camera = GetComponent<Camera>();
        boundsTransform = boundsGameObject.GetComponent<Transform>();
    }

    private void Update() {
        Vector3 moveVec = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        moveVec *= Time.deltaTime*moveSpeed*zoom;
        transform.position += moveVec;
        boundsTransform.position += moveVec;

        zoom += currZoomChangeSpeed*zoom;
    }

    public void OnZoomButton(string buttonType, bool pressedDown) {
        float totalAddSpeed = zoomSpeed;

        if(buttonType == "-") totalAddSpeed = -totalAddSpeed;
        if(!pressedDown) totalAddSpeed = -totalAddSpeed;

        currZoomChangeSpeed -= totalAddSpeed;
    }

    public void OnZoomButtonDown(string buttonType) {
        OnZoomButton(buttonType, true);
    }
    public void OnZoomButtonUp(string buttonType) {
        OnZoomButton(buttonType, false);
    }
}
