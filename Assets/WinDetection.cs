using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour {

    [HideInInspector]
    public List<Furniture> furnitureList;
    public GameObject winText;

    public string nextScene;
    float winTimer = 0;
    bool win = false;

	// Use this for initialization
	void Awake () {
        furnitureList = new List<Furniture>();
	}
	
	// Update is called once per frame
	void Update () {
		if(win)
        {
            winTimer += Time.deltaTime;
            if(winTimer > 3)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
	}

    public void CheckWin()
    {
        bool pass = true;
        foreach(Furniture f in furnitureList)
        {
            if(!f.pass)
            {
                pass = false;
            }
        }
        if(pass)
        {
            winText.gameObject.SetActive(true);
            win = true;
        }
    }
}
