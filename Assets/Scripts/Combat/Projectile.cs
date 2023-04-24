using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    [SerializeField] bool isHoming = true;

    Health target = null;
    float damage = 0f;

    private void Start()
    {
        transform.LookAt(GetAimLocation());

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        if (isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }

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
        if (target.IsDead()) return;


        target.TakeDamage((int)damage);
        Destroy(gameObject);

    }
}
