using TMPro;
using UnityEngine;

public class PushWall : MonoBehaviour
{
    public Transform pushWallTrigger;
    public TextMeshPro text;
    private float startDistance;

    private bool stopCalculating = false;

    private float zWidth;
    private int percentage = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
        text.text = "0%";
        zWidth = GetComponent<Collider>().bounds.size.z/2;
        startDistance = GetZDistance();
        text.transform.parent = this.transform;
    }

    private float GetZDistance()
    {
        return Mathf.Abs(pushWallTrigger.position.z - (this.transform.position.z + zWidth));
    }
    
    // Update is called once per frame
    void Update()
    {
        var newPercentage = Mathf.RoundToInt(((startDistance - GetZDistance()) / startDistance)*100);
            
   
        if(newPercentage != percentage && !stopCalculating)
        {
            percentage = newPercentage;
            text.text = percentage.ToString() + "%";
        }
        if (newPercentage == 100)
        {
            stopCalculating = true;
        }
    }
}
