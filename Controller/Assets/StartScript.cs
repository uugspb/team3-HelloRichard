using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{

    public InputField InputField;


    public void OnGo()
    {
        if (string.IsNullOrEmpty(InputField.text))
            PlayerPrefs.SetString("IP", "172.31.3.115");
        else
            PlayerPrefs.SetString("IP",InputField.text);        
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Controller");
    }
}
