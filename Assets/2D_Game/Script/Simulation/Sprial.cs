using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprial : MonoBehaviour
{
    [SerializeField] private int numPoints = 1000;
    [SerializeField] private float turnFraction = 45;
    [SerializeField] private Color defaultColor;

    private void Start()
    {
    }

    IEnumerator DrawSpiral()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < numPoints; i++)
            {
                float dst = 1 / (numPoints - 1f);
                float angle = 2 * Mathf.PI * turnFraction * i;

                float x = dst * Mathf.Cos(angle);
                float y = dst * Mathf.Sin(angle);

                PlotPoint(x, y, defaultColor);
            }
        }
    }

    private void PlotPoint(float x, float y, Color defaultColor)
    {
        Gizmos.color = defaultColor;
        Gizmos.DrawLine(new Vector3(x, y, 0f), new Vector3(x, y, 0f));
    }
}
