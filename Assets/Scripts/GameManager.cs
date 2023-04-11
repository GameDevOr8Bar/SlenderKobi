using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool pageCollected;
    public bool lastPageCollected;
    public bool gameOver;
    public bool pageCollision;
    public int pageCounter;

    int lastPage = 8;

    [SerializeField]
    GameObject SlendermanPrefab;

    [SerializeField]
    GameObject PagePrefab;

    [SerializeField]
    int spawnLocationsCount;

    [SerializeField]
    GameObject[] spawnLocations;

    [SerializeField]
    TextMeshProUGUI pagesCollectedText;

    [SerializeField]
    Mesh[] pagesMeshes;

    int currentSpawnIndex;
    GameObject currentPage;

  
   void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {   
        lastPageCollected = false;
        gameOver = false;
        pageCounter = 0;
        pageCollected = false;
        currentSpawnIndex = 0;

        // Spawn the first Page
        SpawnPage(ref currentSpawnIndex, ref currentPage);
        SetText(ref pagesCollectedText);
    }

    void Update()
    {
        if (gameOver)
        {
            EndGame();
        }

        if (pageCollected)
        {
            if (pageCounter == 0)
            {
                InstantiateSlenderMan();
            }

            pageCounter += 1;

            DestroyCurrentPage();                          
            SetText(ref pagesCollectedText);

            if (!lastPageCollected)
                SpawnPage(ref currentSpawnIndex, ref currentPage);            

            pageCollected = false;                                    
        }      
    }

    int ChooseSpawnLocation()
    {
        int index = currentSpawnIndex;
        while (index == currentSpawnIndex)
        {
            index = Random.Range(0, spawnLocations.Length);
        }

        return index;
    }

    void DestroyCurrentPage()
    {
        if (currentPage == null)
            return;
        AudioSource audioSource = currentPage.GetComponentInParent<AudioSource>();
        audioSource.Play();
        Destroy(currentPage);
        currentPage = null;
    }

    void SpawnPage(ref int currentSpawnIndex, ref GameObject currentPage)
    {
        currentSpawnIndex = ChooseSpawnLocation();

       if (currentSpawnIndex >= spawnLocations.Length || currentSpawnIndex < 0)
       {
            currentSpawnIndex = 0;
       }

        currentPage = Instantiate(PagePrefab, spawnLocations[currentSpawnIndex].transform);
        currentPage.GetComponent<MeshFilter>().mesh = pagesMeshes[pageCounter];
    }

    void SetText(ref TextMeshProUGUI textObj)
    {
        if (lastPageCollected)
            pagesCollectedText.text = "Collcted All Pages";
        else        
            textObj.text = "Pages Collected: " + pageCounter;
    }

    void InstantiateSlenderMan()
    {
        Instantiate(SlendermanPrefab, Vector3.zero, Quaternion.identity);        
    }    

    void EndGame()
    {
        // add end game logic
        return;
    }

    public bool IsLastPage()
    {
        return pageCounter == lastPage - 1;
    }

}
