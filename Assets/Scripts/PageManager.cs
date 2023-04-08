using UnityEngine;

public class PageManager : MonoBehaviour
{
    const string PLAYER = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER)
            GameManager.Instance.pageCollision = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == PLAYER)
            GameManager.Instance.pageCollision = false;
    }
}
