using UnityEngine;

public class Teleporter : MonoBehaviour
{
    string PLAYER = "Player";  

    public Transform player;     // the Object the player is controlling  
    public float distanceToPlayer = 5; // how close the enemy has to be to the player to play music

    private bool nearPlayer = false; // use this to stop the teleporting if near the player
    //private float nextTeleport = 0.0f; // will keep track of when we to teleport next

    float initalSpawnRate = 20f;
    float lastSpawnTime;

    float behindScale = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // fining player's transform
        player = GameObject.FindGameObjectWithTag(PLAYER).transform;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.lastPageCollected)
        {
            Destroy(gameObject);
        }

        if (!nearPlayer)
        {
            if (Time.time - lastSpawnTime > GetSpawnRate())
            {
                Spawn();
            }
        }

        nearPlayer = Vector3.Distance(transform.position, player.position) <= distanceToPlayer;             
    }

    int GetPageCounter()
    {
        return GameManager.Instance.pageCounter;
    }

    float GetSpawnRate()
    {
        return initalSpawnRate - (GetPageCounter() * 1.5f);
    }    

    bool IsBehindPlayer(Vector3 pos)
    {
        Vector3 dir = pos - player.transform.position;
        dir.Normalize();
        
        return Vector3.Dot(dir, player.forward) < 0;
    }
    
    float RadiusScale(Vector3 pos)
    {
        return IsBehindPlayer(pos) ? behindScale : 1f;
    }

    float GetSpawnRadius()
    {
        Vector3 flatSlendermanPosistion = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 flatPlayerPosistion = new Vector3(player.position.x, transform.position.y, player.position.z);

        Vector3 lerp = Vector3.Lerp(flatSlendermanPosistion, flatPlayerPosistion, (0.12f * GetPageCounter() - 0.06f));

        return Vector3.Distance(lerp, player.transform.position) * RadiusScale(lerp);
    }

    Vector3 GetSpawnPosition()
    {        
        float angle;

        angle = Mathf.Sin(Time.time);

        Vector3 distance = new Vector3(GetSpawnRadius() * Mathf.Cos(angle), transform.position.y, GetSpawnRadius() * Mathf.Sin(angle));
        
        return distance + player.transform.position;
    }

    void Spawn()
    {
        Vector3 spawnPosition = GetSpawnPosition();

        transform.LookAt(player.position);
        transform.position = spawnPosition;

        lastSpawnTime = Time.time;
    }
}
