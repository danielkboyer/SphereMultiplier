using UnityEngine;

public class PushWallTrigger : MonoBehaviour
{
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
        var pushWall = other.tag;
        if(pushWall == "PushWall")
        {
            Destroy(other.GetComponent<Collider>());
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Destroy(other.gameObject, 5);
        }
    }
}
