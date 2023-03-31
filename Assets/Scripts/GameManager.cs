using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get; private set; }

    public bool pageCollected;
    public bool gameOver;
    public int pageCounter;

    int lastPage = 8;

    [SerializeField]
    GameObject PagePrefab;

    [SerializeField]
    int spawnLocationsCount;

    [SerializeField]
    GameObject[] spawnLocations;

    [SerializeField]
    TextMeshProUGUI pagesCollectedText;

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

            if (pageCounter == lastPage)
            {
                gameOver = true;
                return;
            }    

            DestroyCurrentPage();
            SpawnPage();                       
            SetText();

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
