using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonValidate : MonoBehaviour
{
    public void Validate(){
        SceneManager.LoadScene("SettingMenu");
    }
}
