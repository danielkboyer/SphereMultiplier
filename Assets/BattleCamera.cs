using System.Linq;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var attackables = FindObjectsByType<Attackable>(FindObjectsSortMode.None).Where(attackable => !attackable.IsDead).Select(t=>t.transform);
        var averagePos = attackables.Aggregate(Vector3.zero, (acc, t) => acc + t.position) / attackables.Count();
        averagePos.y = this.transform.position.y;
        averagePos.z -= 10;
        this.transform.position = Vector3.Lerp(this.transform.position, averagePos, Time.deltaTime);
     
    }
}
