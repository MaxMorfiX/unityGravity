using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastHoldedBallEditor : MonoBehaviour {
    
    Text LastHoldedBallNameText;
    Text LastHoldedBallMassText;

    [SerializeField] GameObject LastHoldedBallNameGameObject;
    [SerializeField] GameObject LastHoldedBallMassGameObject;
    [SerializeField] GameObject AddMassButton;
    [SerializeField] GameObject ReduceMassButton;
    [SerializeField] GameObject TogglePosLockingButton;

    [SerializeField] Text TogglePosLockingButtonText;

    Ball currBall;
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

    }

    private void Start() {
        LastHoldedBallNameText = LastHoldedBallNameGameObject.GetComponent<Text>();   
        LastHoldedBallMassText = LastHoldedBallMassGameObject.GetComponent<Text>();  

        HideEditor();        
    }

    public void ShowEditor() {
        ReduceMassButton.SetActive(true);
        AddMassButton.SetActive(true);
        LastHoldedBallNameGameObject.SetActive(true);
        LastHoldedBallMassGameObject.SetActive(true);
        TogglePosLockingButton.SetActive(true);
    }

    public void HideEditor() {
        ReduceMassButton.SetActive(false);
        AddMassButton.SetActive(false);
        LastHoldedBallNameGameObject.SetActive(false);
        LastHoldedBallMassGameObject.SetActive(false);
        TogglePosLockingButton.SetActive(false);
    }

    public void AddMass(float val) {
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

}
