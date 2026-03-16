using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDamage : MonoBehaviour
{
    public static bool canKill = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canKill)
            {
                //If the spikes can kill the player show the death menu
                DeathMenu.Instance.ShowDeathMenu();
            }
        }
    }  
}
