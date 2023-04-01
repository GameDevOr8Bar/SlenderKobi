using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    Light lightSource;

    // Affects properties
    public bool nearEnemy;
    public int pagesCollected;
    public float gameTimePassedInSeconds;

    // How much is the flashlight more likely to flicker after a minute has passed, in percentage, every second
    const float minuteFlickeringProbability = 1.2f;
    // The probability increase when player is near an enemy
    const float enemyNearFlickeringProbability = 15f;
    // How much does the flicker last per added minute
    const float minuteFlickerDuration = 0.1f;
    // How much does the flicker duration increase when close to an enemy
    const int enemyNearFlickeringDuration = 1;
    // How long is the flicker duration at the start of the game
    const float startingFlickeringDuration = 0.5f;
    // How much each page increases flickering probability
    const float pageFlickeringProbability = 1f;

    const int secondsInMinute = 60;

    bool flashlightActive;
    // How likely is the flashlight to flicker
    float flickeringProbability;
    // How long does the flashlight flicker
    float flickeringTime;
    // Is the flashlight currently flickering
    bool currFlickering;
    // How long has passed since starting to flicker
    float currFlickeringTime;
    // How many seconds has passed from last flicker
    float timeFromLastFlicker;
    // Time counter to know when should a flicker be checked again
    float flickerCheck;

    // The flashlight's flickering probability
    float lightIntensity;

    // Function to calculate the flickering probability
    float FlickeringProbability(float timeInSeconds, int pagesCollected, bool nearEnemy)
    {
        // Calculate the flicker probability based on time
        float baseProbability = (Mathf.Log(timeInSeconds, 1.2f) / secondsInMinute * minuteFlickeringProbability);
        // Inrease the probability if the player is near an enemy
        if (nearEnemy)
        {
            baseProbability += enemyNearFlickeringProbability;
        }
        // Increase the probability by each page collected
        baseProbability += pagesCollected * pageFlickeringProbability;
        return baseProbability;
    }

    // Function to calculate the flickering duration
    float FlickeringDuration(float timeInSeconds, bool nearEnemy)
    {
        float baseDuration = startingFlickeringDuration;
        baseDuration += gameTimePassedInSeconds / secondsInMinute * minuteFlickerDuration;
        if (nearEnemy)
        {
            baseDuration += enemyNearFlickeringDuration;
        }
        return baseDuration;
    }

    void Awake()
    {
        lightSource = gameObject.GetComponentInChildren<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Default all values
        flashlightActive = false;
        flickeringProbability = 0;
        currFlickering = false;
        currFlickeringTime = 0;
        flickeringTime = startingFlickeringDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // Update flickering probability. Affecting variables are:
        //      Time passed from start of game: the longer the game has played, the more likely the flicker to occur (logarithmic)
        //      Is the player near an enemy: when the player is near an enemy, increases the flicker chance by a large amount
        //      How many pages have been collected: each page increases flicker probability by a small amount
        flickeringProbability = FlickeringProbability(gameTimePassedInSeconds, pagesCollected, nearEnemy);
        // Update flickering max duration. Affected by the same variables as the probability.
        flickeringTime = FlickeringDuration(gameTimePassedInSeconds, nearEnemy);
        // Enables/disables the flashlight. If flashlight is flickering, stop it.
        if (Input.GetMouseButtonDown(1))
        {
            flashlightActive = !flashlightActive;
            // When enabling/disabling the flashlight, the flicker counter resets
            timeFromLastFlicker = 0;
            // If the flashlight was flickering, disabling it resets its status (allows the player to turn off, turn on the light for the flashlight to work correctly for a while)
            if (currFlickering)
            {
                flashlightActive = false;
                currFlickering = false;
            }
        }
        // While flashlight is not flickering, randomize if it should
        if (flashlightActive && !currFlickering)
        {
            // Calculates from the last time a flicker has occured
            timeFromLastFlicker += Time.deltaTime;
            // Flicker chance counter, when it reaches a second it selects whether the flashlight should flicker or not.
            flickerCheck += Time.deltaTime;
            if (flickerCheck >= 1f)
            {
                // The flickering status is dependant on the flickering proabability and the time that has passed from the last flickering.
                currFlickering = Random.Range(1, 100) < flickeringProbability * Mathf.Log(timeFromLastFlicker, 1.2f);
                flickerCheck = 0;
            }
        }
        // Handle current flickering
        if (currFlickering)
        {
            timeFromLastFlicker = 0;
            // The chance for the flashlight to be on during a flicker depends on how much time has the game been active, the longer the less likely the flashlight to turn on.
            flashlightActive = Random.Range(0, gameTimePassedInSeconds) < 100;
            currFlickeringTime += Time.deltaTime;
            // Stop the flickering after the duration
            if (currFlickeringTime >= flickeringTime)
            {
                currFlickeringTime = 0f;
                currFlickering = false;
                flashlightActive = true;
            }
        }
        lightSource.enabled = flashlightActive;
    }
}
