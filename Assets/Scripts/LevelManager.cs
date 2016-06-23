using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public Transform mainMenu, optionMenu, characterMenu, rankingMenu;
    public Transform viewMenu, musicMenu;
    public Transform musicOn, musicOff;
    public InputField userInputField;

    public AudioSource audio;

    public string username;

    public void NameClarify() {
        //userInputField.text = "Enter PlayerName Here...";
        username = userInputField.text.ToString();
        PlayerPrefs.SetString("Name", username);
        //print(username);
        print(PlayerPrefs.GetString("Name"));
    }

    public void LoadScene(string name) {
        DontDestroyOnLoad(audio);
        Application.LoadLevel(name);
    }

    public void QuitGame()  {
        Application.Quit();
    }

    public void OptionMenu(bool clicked) {
        if (clicked == true) {
            optionMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else {
            optionMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void character(bool clicked) {
        if (clicked == true) {
            optionMenu.gameObject.SetActive(clicked);
            characterMenu.gameObject.SetActive(false);
        }
        else {
            optionMenu.gameObject.SetActive(clicked);
            characterMenu.gameObject.SetActive(true);
        }
    }

    public void rankMenu(bool clicked) {
        if (clicked == true) {
            rankingMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(false);
        }
        else {
            rankingMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(true);
        }
    }

    public void ViewMenu(bool clicked) {
        if (clicked == true) {
            viewMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(false);
        }
        else {
            viewMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(true);
        }
    }

    public void MusicMenu(bool clicked) {
        if (clicked == true) {
            musicMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(false);
        }
        else {
            musicMenu.gameObject.SetActive(clicked);
            optionMenu.gameObject.SetActive(true);
        }
    }

    public void MusicOn(bool clicked) {
        //if (clicked == true) {
            audio.Play();
        //}
    }

    public void MusicOff(bool clicked) {
        //if (clicked == true) {
            audio.Pause();
        //}
    }
}
