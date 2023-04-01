using UnityEngine;

public class PageManager : MonoBehaviour
{
    const string PLAYER = "Player";

    void OnTriggerEnter(Collider other)
    {   
        Debug.Log("enter: " +  other.tag);
        if (other.tag == PLAYER)
            GameManager.Instance.pageCollision = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("exit: " + other.tag);
        if (other.tag == PLAYER)
            GameManager.Instance.pageCollision = false;
    }
}
