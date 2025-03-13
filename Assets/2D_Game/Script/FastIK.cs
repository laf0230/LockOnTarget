using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FastIK : MonoBehaviour
{
    public int ChainLength = 0;
    public Transform Target;
    public Transform Pole;
    [Header("Solver Parameters")]
    public int Iteration;
    public float Delta;
    [Range(0,1)]
    public float SnapBackStrength;

    protected float[] BonesLength;
    protected float CompleteLength;
    protected Transform[] Bones;
    protected Vector3[] Positions;
    protected Vector3[] StartDirectionsSucc;
    protected Quaternion[] StartRotationBone;
    protected Quaternion StartRotationTarget;
    protected Quaternion StartRotationRoot;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        // initial array
        Bones = new Transform[ChainLength + 1];
        Positions = new Vector3[ChainLength + 1];
        BonesLength = new float[ChainLength];
        StartDirectionsSucc = new Vector3[ChainLength + 1];
        StartRotationBone = new Quaternion[ChainLength + 1];

        CompleteLength = 0;

        // init fields
        if(Target == null)
        {
            Target = new GameObject(gameObject.name + " Target").transform;
            Target.position = transform.position;
        }
        StartRotationTarget = Target.rotation;

        // initial data
        var current = transform;
        for (int i = Bones.Length -1; i >= 0 ; i--)
        {
            Bones[i] = current;
            StartRotationBone[i] = current.rotation;

            if(i == Bones.Length-1)
            {
                //leaf
                StartDirectionsSucc[i] = Target.position - current.position;
            }
            else
            {
                //mid bone
                StartDirectionsSucc[i] = Bones[i + 1].position - current.position;
                BonesLength[i] = (Bones[i + 1].position - current.position).magnitude;
                CompleteLength += BonesLength[i];
            }

            current = current.parent;
        }

        if (Bones[0] == null)
            throw new UnityException("The chain value is longer than ancentor chain!");
    }

    private void LateUpdate()
    {
        DrawLine();
        ResolveIK();
    }

    private void ResolveIK()
    {
        if (Target == null)
            return;

        if (BonesLength.Length != ChainLength)
            Init();

        //Fabric

        // (bone) (bonelen 0) (bone1) (bonelen 1) (bone2)...
        // x-----------------------x----------------------x----...

        // get position
        for (int i = 0; i < Bones.Length; i++)
            Positions[i] = Bones[i].position;

        var RootRot = (Bones[0].parent != null) ? Bones[0].parent.rotation : Quaternion.identity;
        var RootRotDiff = RootRot * Quaternion.Inverse(StartRotationRoot);

        // Caculation // 1st is possible to reach?
        if ((Target.position - Bones[0].position).sqrMagnitude >= CompleteLength * CompleteLength)
        {
            // just strech it
            var direction = (Target.position - Positions[0]).normalized;
            // set everything after root
            for (int i = 1; i < Positions.Length; i++)
                Positions[i] = Positions[i - 1] + direction * BonesLength[i - 1];
        }
        else
        {
            for (int i = 0; i < Positions.Length - 1; i++)
                Positions[i + 1] = Vector3.Lerp(Positions[i + 1], Positions[i] + RootRotDiff * StartDirectionsSucc[i], SnapBackStrength);

            for(int iteration = 0; iteration < Iteration; iteration++)
            {
                // back
                for(int i = Positions.Length - 1; i > 0; i--)
                {
                    if (i == Positions.Length - 1)
                        Positions[i] = Target.position; // set it to target
                    else
                        Positions[i] = Positions[i + 1] + (Positions[i] - Positions[i + 1]).normalized * BonesLength[i];// set in line on distance
                }

                // forward
                for ( int i = 1; i < Positions.Length; i++)
                    Positions[i] = Positions[i -1] + (Positions[i] - Positions[i-1]).normalized * BonesLength[i-1]; 

                // close enough?
                if ((Positions[Positions.Length - 1] - Target.position).sqrMagnitude < Delta * Delta)
                    break;
            }
        }

        //move towards pole
        if(Pole != null)
        {
            for(int i = 1; i < Positions.Length - 1; i++)
            {
                var plane = new Plane(Positions[i + 1] - Positions[i - 1], Positions[i - 1]);
                var projectedPole = plane.ClosestPointOnPlane(Pole.position);
                var projectedBone = plane.ClosestPointOnPlane(Positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - Positions[i - 1], projectedPole - Positions[i - 1], plane.normal);
                Positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (Positions[i] - Positions[i - 1]) + Positions[i - 1];
            }
        }

        // set position & rotation
        for (int i = 0; i < Positions.Length; i++)
        {
            if (i == Positions.Length - 1)
                Bones[i].rotation = Target.rotation * Quaternion.Inverse(StartRotationTarget) * StartRotationBone[i];
            else
                Bones[i].rotation = Quaternion.FromToRotation(StartDirectionsSucc[i], Positions[i + 1] - Positions[i]) * StartRotationBone[i];
                Bones[i].position = Positions[i];
        }
    }

    private void OnDrawGizmos()
    {
        var current = this.transform;
        for (int i = 0; i < ChainLength && current != null && current.parent != null; i++)
        {
            var scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position), new Vector3(scale, Vector3.Distance(current.parent.position, current.position), scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
        }
    }

    private void DrawLine()
    {
        if (GetComponent<LineRenderer>() != null)
        {
            var line = gameObject.GetComponent<LineRenderer>();
            Vector3[] dots = new Vector3[Bones.Length];
            for (int i = 0; i < Bones.Length; i++)
            {
                dots[i] = Bones[i].position;
            }
            line.positionCount = dots.Length;
            line.SetPositions(dots);
            line.enabled = true;
        }
        else if (GetComponent<LineRenderer>() == null)
        {
            var line = gameObject.AddComponent<LineRenderer>();
            Vector3[] dots = new Vector3[Bones.Length];
            for (int i = 0; i < Bones.Length; i++)
            {
                dots[i] = Bones[i].position;
            }
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.positionCount = dots.Length;
            line.SetPositions(dots);
            line.startWidth = 0.2f;
            line.numCornerVertices = 50;
            line.numCapVertices = 50;
            line.enabled = true;
        }
    }
}
