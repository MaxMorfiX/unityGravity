using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastHoldedBallEditor : MonoBehaviour {

    public float massChangeSpeed = 0.01f;
    private float currMassChangeSpeed = 0f;
    
    Text LastHoldedBallNameText;
    Text LastHoldedBallMassText;

    public GameObject LastHoldedBallNameGameObject;
    [SerializeField] GameObject AddHoldMassButton;
    [SerializeField] GameObject ToggleFollowByLastHoldedBallButton;
    [SerializeField] GameObject ReduceHoldMassButton;
    [SerializeField] GameObject LastHoldedBallMassGameObject;
    [SerializeField] GameObject AddMassButton;
    [SerializeField] GameObject ReduceMassButton;
    [SerializeField] GameObject TogglePosLockingButton;

    [SerializeField] Text TogglePosLockingButtonText;

    public Ball currBall;
    Ball recentFrameBall;

    private void Update() {
        recentFrameBall = currBall;
        currBall = Ball.holdedBall;

        if(currBall == null) {
            return;
        } else if(recentFrameBall == null) {
            ShowEditor();
        }

        if(recentFrameBall != currBall) {
            if(currBall.IsPosLocked()) TogglePosLockingButtonText.text = "Unlock pos";
            else if(!currBall.IsPosLocked()) TogglePosLockingButtonText.text = "Lock pos";
        }

        LastHoldedBallNameText.text = "" + currBall.name;
        LastHoldedBallMassText.text = "Mass: " + currBall.rb.mass;

        AddMass(currMassChangeSpeed*currBall.rb.mass);

    }

    private void Start() {
        LastHoldedBallNameText = LastHoldedBallNameGameObject.GetComponent<Text>();   
        LastHoldedBallMassText = LastHoldedBallMassGameObject.GetComponent<Text>();  

        HideEditor();        
    }

    public void ShowEditor() {
        ReduceMassButton.SetActive(true);
        ToggleFollowByLastHoldedBallButton.SetActive(true);
        AddMassButton.SetActive(true);
        ReduceHoldMassButton.SetActive(true);
        AddHoldMassButton.SetActive(true);
        LastHoldedBallNameGameObject.SetActive(true);
        LastHoldedBallMassGameObject.SetActive(true);
        TogglePosLockingButton.SetActive(true);
    }

    public void HideEditor() {
        ReduceMassButton.SetActive(false);
        ToggleFollowByLastHoldedBallButton.SetActive(false);
        AddMassButton.SetActive(false);
        ReduceHoldMassButton.SetActive(false);
        AddHoldMassButton.SetActive(false);
        LastHoldedBallNameGameObject.SetActive(false);
        LastHoldedBallMassGameObject.SetActive(false);
        TogglePosLockingButton.SetActive(false);
    }

    public void AddMass(float val) {
        if(currBall.rb.mass + val < 0.01) return;
        currBall.rb.mass += val;
    }

    public void TogglePosLocking() {
        currBall.TogglePosLocking();
        
        if(currBall.IsPosLocked()) TogglePosLockingButtonText.text = "Unlock pos";
        else if(!currBall.IsPosLocked()) TogglePosLockingButtonText.text = "Lock pos";
    }

    public void LockPos() {
        currBall.LockPos();

        TogglePosLockingButtonText.text = "Unlock pos";
    }
    public void UnlockPos() {
        currBall.UnlockPos();

        TogglePosLockingButtonText.text = "Lock pos";
    }

    public void OnMassButton(string buttonType, bool pressedDown) {
        float totalAddMass = massChangeSpeed;

        if(buttonType == "-") totalAddMass = -totalAddMass;
        if(!pressedDown) totalAddMass = -totalAddMass;

        currMassChangeSpeed += totalAddMass;
    }

    public void OnMassButtonDown(string buttonType) {
        OnMassButton(buttonType, true);
    }
    public void OnMassButtonUp(string buttonType) {
        OnMassButton(buttonType, false);
    }
}
