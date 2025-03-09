using UnityEngine;
#nullable enable
public class Arrow : MonoBehaviour
{

    public float Speed;

    public float Damage;

    public Attackable? Attackable;
    public bool IsInitialized = false;

    public void Init(float damage, Attackable attackable)
    {
        this.Damage = damage;
        this.Attackable = attackable;
        IsInitialized = true;
    }


    void FixedUpdate()
    {

        if (!IsInitialized)
        {
            return;
        }
        if (Attackable == null)
        {
            Destroy(gameObject);
            return;
        }
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Attackable.transform.position, Speed * Time.deltaTime);
        transform.LookAt(Attackable.transform, Vector3.forward);
        if (Vector3.Distance(transform.position, Attackable.transform.position) < 1.0f)
        {
            Attackable.TakeDamage(Damage);
            Destroy(gameObject);
        }

    }
}
