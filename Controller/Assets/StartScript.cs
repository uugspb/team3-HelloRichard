using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{

    public InputField InputField;


    public void OnGo()
    {
        PlayerPrefs.SetString("IP",InputField.text);        
        PlayerPrefs.Save();

        SceneManager.LoadScene("Controller");
    }
}
