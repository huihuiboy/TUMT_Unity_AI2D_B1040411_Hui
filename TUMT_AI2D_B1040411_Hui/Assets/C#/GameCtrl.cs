using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void End()
    {
        Application.Quit();
    }
}
