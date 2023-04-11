using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int cooldown = 2;
    bool keyPressed = false;  

    // Update is called once per frame
    void Update()
    {
        HandlePageCollection();
    }    

    void HandlePageCollection()
    {
        // if the key is pressed, thortlling is over, 
        // and player collided with the page, then collect page
        if (!keyPressed && 
            Input.GetMouseButtonDown(0) && 
            GameManager.Instance.pageCollision)
        {

            GameManager.Instance.pageCollected = true;
            GameManager.Instance.pageCollision = false;

            if (GameManager.Instance.IsLastPage())
                GameManager.Instance.lastPageCollected = true;

            // using a coroutine to handle throtlling
            StartCoroutine(KeyPressCooldown());
        }
    }

    IEnumerator KeyPressCooldown()
    {
        keyPressed = true;
        yield return new WaitForSeconds(cooldown);
        keyPressed = false;
    }
}
