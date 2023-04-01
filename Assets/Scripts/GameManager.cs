using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool pageCollected;
    public bool lastPageCollected;
    public bool gameOver;
    public bool pageCollision;
    public int pageCounter;

    int lastPage = 3;

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

    public bool IsLastPage()
    {
        return pageCounter == lastPage - 1;
    }
    

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
        SpawnPage();
        SetText();

    }

    void Update()
    {  
        if (gameOver)
        {
            EndGame();
        }

        if (pageCollected)
        {            
            pageCounter += 1;
       
            DestroyCurrentPage();                          
            SetText();

            if (!lastPageCollected)
                SpawnPage();            
            
            pageCollected = false;                                    
        }

        if (lastPageCollected)
        {
            pagesCollectedText.text = "Collcted All Pages";
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

        Destroy(currentPage);
        currentPage = null;
    }

    void SpawnPage()
    {
        currentSpawnIndex = ChooseSpawnLocation();

       if (currentSpawnIndex >= spawnLocations.Length || currentSpawnIndex < 0)
       {
            currentSpawnIndex = 0;
       }

        currentPage = Instantiate(PagePrefab, spawnLocations[currentSpawnIndex].transform);
        currentPage.GetComponent<MeshFilter>().mesh = pagesMeshes[pageCounter];
    }

    void SetText()
    {
        pagesCollectedText.text = "Pages Collected: " + pageCounter;
    }

    void EndGame()
    {
        // add end game logic
        return;
    }
}
