using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameMandager : MonoBehaviour {

    bool isGamePaused = false;
    bool isLastHoldedBallFollowed = false;

    public GameObject lastHoldedBallEditorGameObject;
    [SerializeField] GameObject cameraControlGameObject;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform ballsContainer;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] TrajectoryRenderer trajectoryRenderer;
    [SerializeField] Camera camera;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image pauseButtonSpriteRenderer;

    [SerializeField] Sprite pauseButtonSprite;
    [SerializeField] Sprite resumeButtonSprite;
    
    [SerializeField] Text ToggleBallsCollisionsButtonText;
    [SerializeField] Text ToggleBordersButtonText;
    [SerializeField] Text ToggleFollowByLastHoldedBallButtonText;

    private void Start() {
        FitToSize();
    }
    public void FitToSize() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        lastHoldedBallEditorGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth, screenHeight);
        cameraControlGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth, screenHeight);
    }

    public void ToggleGamePause() {
        isGamePaused = !isGamePaused;

        if(isGamePaused) {
            PauseGame();
        } else if(!isGamePaused) {
            ResumeGame();
        }
    }

    public void PauseGame() {
        isGamePaused = true;

        Time.timeScale = 0;

        pauseMenu.SetActive(isGamePaused);
        pauseButtonSpriteRenderer.sprite = resumeButtonSprite;
    }

    public void ResumeGame() {
        isGamePaused = false;

        Time.timeScale = 1;

        pauseMenu.SetActive(isGamePaused);
        pauseButtonSpriteRenderer.sprite = pauseButtonSprite;
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        ResumeGame();
    }

    public void GoToMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public GameObject AddBall() {
        GameObject ball = Instantiate(ballPrefab, ballsContainer);

        ball.transform.SetParent(ballsContainer);
        ball.GetComponent<Ball>().SetValues(camera, trajectoryRenderer, joystick);

        Vector3 camPos = camera.GetComponent<Transform>().position;
        ball.GetComponent<Transform>().position = new Vector3(camPos.x, camPos.y, 0);

        return ball;
    }

    public GameObject AddRandomBall() {
        GameObject ball = AddBall();

        ball.GetComponent<Ball>().SetRandomValues(true, "by mass", true, true);
        return ball;
    }

    public void AddRandomBallVoid() {
        AddRandomBall();
    }

    public void ToggleBounds() {
        SettingsManager.ToggleLayerCollision(8, 9);

        if(SettingsManager.GetLayerCollision(8,9)) {
            ToggleBordersButtonText.text = "Disable Borders";
        } else {
            ToggleBordersButtonText.text = "Enable Borders (beta)";
        }
    }

    public void ToggleBallsCollisions() {
        SettingsManager.ToggleLayerCollision(9, 9);

        if(SettingsManager.GetLayerCollision(9, 9)) {
            ToggleBallsCollisionsButtonText.text = "Disable Ball Collisions";
        } else {
            ToggleBallsCollisionsButtonText.text = "Enable Ball Collisions";
        }
    }
    
    
}