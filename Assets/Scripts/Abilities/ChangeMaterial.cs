using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] Material transparentMaterial;
    [SerializeField] Material defaultMaterial;

    [SerializeField] PlayerMovement player;
    [SerializeField] int indexToChange = 1;

    public bool shouldBeTransparent = false;

    MeshCollider meshCollider;
    MeshRenderer meshRenderer;

    Material[] materials;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        materials = meshRenderer.materials;
        materials[indexToChange] = defaultMaterial;
        meshRenderer.materials = materials;
    }

    // Update is called once per frame
    void Update()
    {
        // if(player.echoLocation)
        if(player.echoLocation)
        {
            // meshCollider.enabled = false;
            materials = meshRenderer.materials;
            materials[indexToChange] = transparentMaterial;
            meshRenderer.materials = materials;
        }
        else
        {
            // meshCollider.enabled = true;
            materials = meshRenderer.materials;
            materials[indexToChange] = defaultMaterial;
            meshRenderer.materials = materials;
        }
    }
}