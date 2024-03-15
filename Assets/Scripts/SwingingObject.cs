using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SwingingObject : MonoBehaviour
{

    [Header("General Settings")]
    [HideInInspector]
    public Vector3 dimensions;
    [HideInInspector]
    public Vector3 position;
    [SerializeField]
    public float timePeriod;
    [SerializeField]
    public float cooldown;
    [SerializeField]
    public float waitDelay;
    [SerializeField]
    public float radius;
    [SerializeField]
    public bool changeAxis;
    [SerializeField]
    public bool changeDirection;


    [SerializeField] MeshFilter unityMeshFilter;
    Mesh unityMesh;

    public void OnValidate()
    {

        dimensions = transform.localScale;
        position = transform.localPosition;

        if (unityMeshFilter) unityMesh = unityMeshFilter.sharedMesh;

    }
}
