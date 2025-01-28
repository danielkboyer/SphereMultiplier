using TMPro;
using UnityEngine;

public class CountWall : MonoBehaviour
{
    public TextMeshPro text;
    public int amountNeeded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = amountNeeded.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            Destroy(other.gameObject);
            amountNeeded--;
            text.text = amountNeeded.ToString();
            if (amountNeeded == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
