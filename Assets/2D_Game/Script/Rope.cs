using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    LineRenderer lineRenderer;
    Vector3[] positions = new Vector3[2];
    PlayerController player;
    [SerializeField] bool isWarpable;
    float maxCoolTime = 0;
    float coolTime = 0;
    public float interpulation = 0;
    CursorTracker cursorTracker;
    Coroutine moveRoutine;
        
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        cursorTracker = FindAnyObjectByType<CursorTracker>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = positions.Length;
        lineRenderer.loop = true;

        isWarpable = true;
    }

    private void Update()
    {
        positions[0] = (Vector2)transform.position;
        positions[1] = (Vector2)cursorTracker.transform.position;
    }

    public void SetRopeData(float maxCoolTime)
    {
        this.maxCoolTime = maxCoolTime;
    }

    public void RopeTracking()
    {
        lineRenderer.SetPositions(positions);
    }

    #region Warp

    public void WarpToMousePosition(MonoBehaviour target)
    {
        if(isWarpable)
        {
            if(moveRoutine != null) StopCoroutine(moveRoutine);
            moveRoutine = StartCoroutine("Move", positions[1]);
            // StartCoroutine("CoolDown");
        }
    }

    private IEnumerator CoolDown()
    {
        isWarpable = false;
        cursorTracker.PrograssUpdate(1);

        while(true)
        {
            yield return null;
            cursorTracker.PrograssUpdate(coolTime / maxCoolTime);
            coolTime -= Time.deltaTime;
            if( coolTime <= 0 ) break;
        }

        cursorTracker.PrograssUpdate(0);
        coolTime = maxCoolTime;
        isWarpable = true;
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        isWarpable = false;
        player.SetGravityScale(0);

        while(true)
        {
            yield return null;
            if (((Vector2)(player.transform.position - targetPosition)).magnitude < 0.5f)
            {
                break;
            }
            player.transform.position = Vector2.Lerp(player.transform.position, targetPosition, interpulation * Time.deltaTime);
        }

        isWarpable = true;
        player.SetRGVelocity(Vector2.zero);
        player.SetGravityScale(0.1f);
        yield return new WaitForSeconds(0.5f);
        player.SetGravityScale(1);
    }

    #endregion

    public Vector3 GetDirection()
    {
        return (positions[1] - player.transform.position).normalized;
    }
}
