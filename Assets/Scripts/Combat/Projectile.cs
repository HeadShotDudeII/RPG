using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 10f;

    Health target = null;
    float damage = 0f;


    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;

    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
        if (targetCollider == null) return target.transform.position;
        return target.transform.position + Vector3.up * (targetCollider.height / 2);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.GetComponent<Health>() != target)
        {
            return;
        }
        else
        {
            target.TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
