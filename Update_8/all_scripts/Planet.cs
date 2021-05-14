using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(100, 2000)]
    public int res = 10;

    public bool autoUpdate = true;
    public enum FaceRenderMask {All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;
    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;
  /*
    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
        GeneratePlanet();
    }
    */
    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for(int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
               // meshObj.AddComponent<MeshCollider>();
                //meshObj.AddComponent<Rigidbody>();
                //meshObj.GetComponent<Rigidbody>().useGravity = false;
                //meshObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY; 
                //meshObj.GetComponent<MeshCollider>().convex = true;

                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, res, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    private void Start()

    {

        GeneratePlanet();

    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
        
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMesh()
    {
        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i].gameObject.activeSelf)
            terrainFaces[i].ConstructMesh();
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {

        colorGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
                terrainFaces[i].UpdateUVs(colorGenerator);
        }
    }
}
