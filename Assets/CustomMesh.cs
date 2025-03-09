using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMesh : MonoBehaviour
{
    public int[] ints = null;
    public string shaderName = "Standard";
    public Texture2D texture;

    public int segment = 50;
    public float radious;

    public void Awake()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = segment +1;

        float x;
        float y;

        var angle = 20f;
        var points = new Vector3[segment + 1];

        for(int i = 0; i < segment; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radious;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radious;

            points[i] = new Vector3(x, 0.1f, y);

            angle += (360 / segment);
        }
    }

    public void CreateTringleMesh()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        renderer.material = new Material(Shader.Find(shaderName));
        renderer.material.mainTexture = texture;

        Mesh mesh = new();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (-1f, -1f, 0),
            new Vector3 (-1f, 1f, 0),
            new Vector3 (1f, 1f, 0),
            new Vector3 (1f, -1f, 0),
        };

        Vector2[] uvs = new Vector2[]
        {
            new Vector2 (0, 0),
            new Vector2 (0, 1),
            new Vector2 (1, 1),
            new Vector2 (1, 1),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 3, 0,
        };

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        collider.sharedMesh = mesh;
        collider.convex = true;

        meshFilter.mesh = mesh;
    }
}
