using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostScreen : MonoBehaviour
{
    public void playButton(){
        SceneManager.LoadScene("OptionMenu");
    }
}
