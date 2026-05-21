using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            GameManager.instance.AgarrarObjeto();
            Destroy(gameObject);
        }
    }
}