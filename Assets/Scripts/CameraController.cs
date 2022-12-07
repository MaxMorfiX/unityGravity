using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public float moveSpeed = 1;
    public float zoomSpeed = 0.1f;


    [SerializeField] FixedJoystick joystick;
    [SerializeField] GameObject boundsGameObject;
    [SerializeField] GameObject followedBall;
    [SerializeField] Text ToggleFollowByLastHoldedBallButtonText;


    private Transform transform;
    private Transform boundsTransform;
    private Camera camera;
    private LastHoldedBallEditor lastHoldedBallEditor;
    private float currZoomChangeSpeed = 0f;
    public float zoom { get { return camera.orthographicSize; } set { camera.orthographicSize = value; } }
    private bool isLastHoldedBallFollowed = false;

    private void Start() {
        transform = GetComponent<Transform>();
        camera = GetComponent<Camera>();
        boundsTransform = boundsGameObject.GetComponent<Transform>();
        lastHoldedBallEditor = GetComponent<MainGameMandager>().lastHoldedBallEditorGameObject.GetComponent<LastHoldedBallEditor>();
    }

    private void Update() {
        Vector3 moveVec = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
        moveVec.x += Input.GetAxis("Horizontal");
        moveVec.y += Input.GetAxis("Vertical");

        moveVec *= Time.deltaTime*moveSpeed*zoom;
        transform.position += moveVec;
        boundsTransform.position += moveVec;




        float currZoom = zoom;

        if(Input.GetKeyDown("z")) OnZoomButtonDown("+");
        if(Input.GetKeyDown("x")) OnZoomButtonDown("-");
        if(Input.GetKeyUp("z")) OnZoomButtonUp("+");
        if(Input.GetKeyUp("x")) OnZoomButtonUp("-");

        zoom += currZoomChangeSpeed*currZoom;




        if(Input.GetKeyDown("f")) ToggleFollowByLastHoldedBall();
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

    public void SetFollowByLastHoldedBall(bool val) {
        isLastHoldedBallFollowed = val;
        followedBall = lastHoldedBallEditor.LastHoldedBallNameGameObject;

        if(val) {
            transform.SetParent(lastHoldedBallEditor.currBall.transform);
            transform.localPosition = new Vector3(0, 0, -10);

        } else transform.SetParent(null);
    }

    public void ToggleFollowByLastHoldedBall() {
        SetFollowByLastHoldedBall(!isLastHoldedBallFollowed);

        if(isLastHoldedBallFollowed) {
            ToggleFollowByLastHoldedBallButtonText.text = "Unfollow";
        } else {
            ToggleFollowByLastHoldedBallButtonText.text = "Follow";
        }
    }
}
