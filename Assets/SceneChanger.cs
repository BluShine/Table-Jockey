using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

	public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
