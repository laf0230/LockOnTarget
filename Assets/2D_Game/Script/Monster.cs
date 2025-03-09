using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Monster : MonoBehaviour, IDamageable
{
    // Todo: Level of Detection
    public float health;
    public float damage;
    public float speed;
    Rigidbody2D rigid;
    List<Transform> targets = new List<Transform>();
    Coroutine movementCoroutine;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("FindTargetwithDelay", 1f);
    }

    public void Damaged(MonoBehaviour attacker, float damageAmount)
    {
        if(health <= 0)
        {
            Died();
        }
        else
        {
            health -= damageAmount;
        }

        var attackerTransform = attacker.transform.position;
        Vector3 force = new Vector3((transform.position.x - attackerTransform.x * 1.5f), 0);
        Vector3 selfForce = new Vector3(attackerTransform.x - transform.position.x * 1.2f, 0);
        rigid.AddForce(force, ForceMode2D.Impulse);
        attacker.GetComponent<Rigidbody2D>().AddForce(selfForce, ForceMode2D.Impulse);
    }

    public void MoveToTarget()
    {
        // Move to Target
        if(movementCoroutine != null)
            StopCoroutine(movementCoroutine);
        movementCoroutine = StartCoroutine("SmoothMove");
    }

    public IEnumerator SmoothMove()
    {
        while(true)
        {
            yield return null;
            // Todo: Movement Smoothly
            if(targets.Count > 0
               //  && Vector3.Distance(transform.position, targets[0].position) > 0.01f
                )
            {
                transform.position = Vector2.Lerp(transform.position, targets[0].position, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator FindTargetwithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);

            MoveToTarget();
            targets = Search();
        }
    }

    public List<Transform> Search()
    {
        var visibleObjects = GetComponent<FieldOfView>().visibleTargets;
        return visibleObjects;
    }

    public void Died()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Player"))
        {
            collision.GetComponent<Collider2D>().GetComponent<IDamageable>().Damaged(this, damage);
        }
    }
}
