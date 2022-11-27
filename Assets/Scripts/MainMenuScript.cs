using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    [SerializeField] Text gameVersionText;

    public void StartGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void ExitGame() {
        Application.Quit();
    }

    private void Start() {
        gameVersionText.text = "v" + Application.version;
    }
}
