using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class SimpleTrigonometry : MonoBehaviour
{
    // �ﰢ�Լ��� �̿��ؼ� sinȤ�� cosȤ�� ���� �׸��� �������� �׸��� ����
    // ����: Lerp�� ����� ��

    // ���� �������� ���� ������

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
