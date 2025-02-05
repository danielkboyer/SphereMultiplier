using System.Collections;
using UnityEngine;

public class MoveObjectTo : MonoBehaviour
{

    public Transform MoveTo;

    private bool DoMoveObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DoMoveObject)
        {
            transform.position = Vector3.Lerp(transform.position, MoveTo.position, Time.deltaTime);
        }
    }

    public void MoveIt()
    {
        DoMoveObject = true;
     
    }
}
