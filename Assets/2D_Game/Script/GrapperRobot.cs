using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GrapperRobot : MonoBehaviour
{
    public Transform proceduralTarget;
    public Transform targetTransform;
    public float viewRange = 1f;
    public float bouncePower = 1f;
    public float moveSpeed = 1f;

    private FastIK ik;
    private Vector2 proceduralInitialPosition;
    private CircleCollider2D col;
    private Coroutine moveRoutine;

    private void Start()
    {
        proceduralInitialPosition = proceduralTarget.position;
        ik = GetComponentInChildren<FastIK>();
        col = GetComponent<CircleCollider2D>();
        col.radius = viewRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            SetTarget(collision.transform);
            TargetCatched(collision.transform);
            MoveToTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            SetTarget(null);
            proceduralTarget.transform.position = proceduralInitialPosition;
            StopCoroutine(moveRoutine);
        }
    }

    private void TargetCatched(Transform catchedTarget)
    {
        if (proceduralTarget == null || catchedTarget == null) return;

        // 0,0 - 1,1 = -1,-1
        // 위치적 방향이자 힘의 방향 = 목적지 - 자신
        // bouncePower는 가속도
        // nomalize를 사용하지 않음으로써 거리로 인한 가속도 추가
        var bounceDirection = transform.position - proceduralTarget.position;
        catchedTarget.GetComponent<Rigidbody2D>().velocity = bounceDirection * bouncePower;
    }

    private void SetTarget(Transform target)
    {
        targetTransform = target;
    }

    private void MoveToTarget()
    {
        if(proceduralTarget == null || targetTransform == null)
            return;

        if(moveRoutine != null)
            StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine("MoveRoutine");
    }

    private IEnumerator MoveRoutine()
    {
        while(true)
        {
            yield return null;
            
            proceduralTarget.position = Vector2.Lerp(proceduralTarget.position, targetTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
