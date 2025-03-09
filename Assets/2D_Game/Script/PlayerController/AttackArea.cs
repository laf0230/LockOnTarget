using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public IDamageable damageableObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<IDamageable>(out damageableObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        damageableObject = null;
    }
}
