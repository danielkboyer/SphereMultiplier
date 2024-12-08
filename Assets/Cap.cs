using UnityEngine;

public class Cap : MonoBehaviour
{

    public GameObject EndCap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(EndCap != null && other.gameObject.tag == "Ball")
        {
            other.gameObject.transform.position = EndCap.transform.position;
        }
    }
}
