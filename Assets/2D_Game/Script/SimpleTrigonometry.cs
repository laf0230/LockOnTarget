using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class SimpleTrigonometry : MonoBehaviour
{
    // 삼각함수를 이용해서 sin혹은 cos혹은 원을 그리는 움직임을 그리고 싶음
    // 조건: Lerp를 사용할 것

    // 원의 움직임을 먼저 구현함

    [SerializeField] float xAxis;
    [SerializeField] float yAxis;
    [Range(1f, 100f)]
    [SerializeField] float speed = 1f;
    [Range(1f, 100f)]
    [SerializeField] float scale = 1f;
    [SerializeField] Vector2 radian;

    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;
    [SerializeField] Transform moveTarget;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        ResolveMovement();
    }

    void ResolveMovement()
    {
        Vector2 startPoint = startTransform.position;
        Vector2 endPoint = endTransform.position;
        var xValue = transform.position.x + Time.time * speed;
        var yValue = transform.position.y + Time.time * speed;
        radian = new Vector2(xAxis * Mathf.Cos(xValue), yAxis * Mathf.Sin(yValue));

        moveTarget.position = radian * scale;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, scale);
    }
}
