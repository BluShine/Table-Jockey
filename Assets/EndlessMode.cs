using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndlessMode : MonoBehaviour {

    public float spawnRadius = 1;

    static Furniture.FurnitureType[] objectBag = {
        Furniture.FurnitureType.chair, Furniture.FurnitureType.chair, Furniture.FurnitureType.chair,
        Furniture.FurnitureType.table, Furniture.FurnitureType.table, Furniture.FurnitureType.bed };

    public ChairSpawner chair;
    public TableSpawner table;
    public BedSpawner bed;

    List<Furniture.FurnitureType> currentBag;

    public Text scoreText;
    public Text timeText;
    public GameObject gameoverText;
    public RawImage failMeter;

    float spawnTime = 0;
    static float SPAWNINTERVAL = 15;

    int furnitureCount = 3;
    int failingFurniture = 0;
    bool failing = false;
    float failTimer = 0;
    static float MAXFAIL = 10;

    public List<Furniture> furnitureList;

    void Awake()
    {
        furnitureList = new List<Furniture>();
    }

	// Use this for initialization
	void Start ()
    {
        shuffleBag();
        spawnTime = SPAWNINTERVAL;
        gameoverText.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //spawn new furniture
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0)
        {
            spawnObject();
            furnitureCount++;
            spawnTime += SPAWNINTERVAL;
        }
        timeText.text = Mathf.FloorToInt(spawnTime + 1).ToString();
        //detect failure
        failingFurniture = 0;
        foreach(Furniture f in furnitureList)
        {
            if(!f.pass)
            {
                failingFurniture++;
            }
        }
        //show failure text
        string arrangement = "nice";
        failing = false;
        if (failingFurniture > 5)
        {
            arrangement = "BAD!";
            failing = true;
        } else if (failingFurniture > 3)
        {
            arrangement = "not good";
        } else if (failingFurniture > 1)
        {
            arrangement = "mediocre";
        } else if (failingFurniture > 0)
        {
            arrangement = "almost nice";
        }
        scoreText.text = "Furniture: " + furnitureCount + "\nArrangement: " + arrangement;
        //failure meter
        if(failing)
        {
            failTimer += Time.deltaTime;
        } else if(failTimer != MAXFAIL)
        {
            failTimer -= Time.deltaTime;
        }
        failTimer = Mathf.Max(0, failTimer);
        failTimer = Mathf.Min(MAXFAIL, failTimer);
        //display meter
        failMeter.rectTransform.localScale = new Vector3(failTimer / MAXFAIL, 1, 1);
        if (Random.value < .333f)
            failMeter.color = Color.red;
        else if (Random.value < .5f)
            failMeter.color = Color.white;
        else
            failMeter.color = Color.yellow;
        //gameover
        if(failTimer == MAXFAIL)
        {
            gameoverText.SetActive(true);
            spawnTime = 1;
            if(Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("title");
            }
        }
	}

    void shuffleBag()
    {
        currentBag = new List<Furniture.FurnitureType>();
        foreach(Furniture.FurnitureType t in objectBag)
        {
            currentBag.Insert(Random.Range((int)0, currentBag.Count), t);
        }
    }

    void spawnObject()
    {
        GameObject createdFurniture = null;
        switch (currentBag[0])
        {
            case Furniture.FurnitureType.chair:
                createdFurniture = chair.spawnChair(transform.position +
                    new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)),
                    Quaternion.Euler(0, Random.value * 360f, 0));
                break;
            case Furniture.FurnitureType.table:
                createdFurniture = table.spawnTable(transform.position +
                    new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)),
                    Quaternion.Euler(0, Random.value * 360f, 0));
                break;
            case Furniture.FurnitureType.bed:
                createdFurniture = bed.spawnBed(transform.position +
                    new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)),
                    Quaternion.Euler(0, Random.value * 360f, 0));
                break;
        }
        //furnitureList.Add(createdFurniture.GetComponentInChildren<Furniture>());
        currentBag.RemoveAt(0);
        if (currentBag.Count == 0)
        {
            shuffleBag();
        }
    }
}
