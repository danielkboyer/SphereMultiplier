using UnityEngine;

public class LoseLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log("Game over");
            FindAnyObjectByType<Assets.GameManager>().StartYouLose();
        }
    }
}
