using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class GameStateControllerScript : MonoBehaviour {
    public GameObject mainMenuCanvas;
    public GameObject playCanvas;
    public GameObject gameOverCanvas;

    public Text playScore;
    public Text gameOverScore;
    public Text topScore;
    public Text playerName;

    public int score, top;

    private GameObject currentCanvas;
    private string state;

    public string filename = "top.txt";

    public void Start() {
        currentCanvas = null;
        MainMenu();
    }

    public void Update() {
        if (state == "play") {
            topScore.text = PlayerPrefs.GetInt("Top").ToString();
            playScore.text = score.ToString();
            playerName.text = PlayerPrefs.GetString("Name");
        }
        else if (state == "mainmenu") {
            if (Input.GetButtonDown("Cancel")) {
                Application.Quit();
            }
            else if (Input.anyKeyDown) {
                Play();
            }
        }
        else if (state == "gameover") {
            if (Input.anyKeyDown) {
                Application.LoadLevel("Menu");
            }
        }
    }

    public void MainMenu() {
        CurrentCanvas = mainMenuCanvas;
        state = "mainmenu";

        GameObject.Find("LevelController").SendMessage("Reset");
        GameObject.FindGameObjectWithTag("Player").SendMessage("Reset");
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("Reset");

        File.SetAttributes(Application.dataPath + "/" + filename, FileAttributes.Normal);
        StreamReader sr = new StreamReader(Application.dataPath + "/" + filename);
        string fileContent = sr.ReadLine();

        sr.Close();

        topScore.text = fileContent;
    }

    public void Play() {
        CurrentCanvas = playCanvas;
        state = "play";
        score = 0;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>().canMove = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovementScript>().moving = true;
    }

    public void GameOver() {
        CurrentCanvas = gameOverCanvas;
        state = "gameover";

        gameOverScore.text = score.ToString();
        if (score > top) {
            PlayerPrefs.SetInt("Top", top);
            var sw = File.CreateText(filename);
            sw.Write(top);
            sw.Close();
        }
        
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovementScript>().moving = false;
    }

    private GameObject CurrentCanvas {
        get {
            return currentCanvas;
        }
        set {
            if (currentCanvas != null) {
                currentCanvas.SetActive(false);
            }
            currentCanvas = value;
            currentCanvas.SetActive(true);
        }
    }
}
