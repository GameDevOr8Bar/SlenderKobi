using UnityEngine;

public class PageManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.pageCollision = true;
    }

    void OnTriggerExit(Collider other)
    {
        GameManager.Instance.pageCollision = false;
    }
}
