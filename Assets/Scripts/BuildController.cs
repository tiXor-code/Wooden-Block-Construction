using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// This class does all the Building logic
/// 
/// Teodor Lutoiu, v0.4, 22/04/2022
/// </summary>
public class BuildController : MonoBehaviour
{
    // Stores the Prefab for the first Object
    [SerializeField]
    private GameObject constructionPrefab_01;

    // Stores the Prefab for the second Object
    [SerializeField]
    private GameObject constructionPrefab_02;

    // Stores the Hotkey used to summon the First Object
    [SerializeField]
    private KeyCode newConstructionHotkey_01 = KeyCode.Alpha1;

    // Stores the Hotkey used to summon the Second Object
    [SerializeField]
    private KeyCode newConstructionHotkey_02 = KeyCode.Alpha2;

    // Stores the Hotkey used to Rotate
    [SerializeField]
    private KeyCode rotateLeftHotkey = KeyCode.Q;

    // Stores the Hotkey used to summon the First Object
    [SerializeField]
    private KeyCode rotateRightHotkey = KeyCode.E;

    // To be upgraded to Dictionary
    //private Dictionary<GameObject, KeyCode> ConstructionDictionary = new Dictionary<GameObject, KeyCode>;

    // Maximum distance for snapping
    public float snapDistance = 2f; 

    private GameObject currentConstruction;

    // Maximum distance for raycast
    [SerializeField]
    private float maxDistance = 10f;

    private void Start()
    {
        // Ignore collisions between layer 8 (construction objects) and layer 10 (magnets)
        Physics.IgnoreLayerCollision(8, 10);
    }

    // Update is called once per frame
    void Update()
    {
        HandleNewObjectHotkey();

        if (currentConstruction != null)
        {
            MoveCurrentConstructionToMouse();
            RotateConstruction();
            ReleaseIfClicked();
        }
    }
    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newConstructionHotkey_01))
        {
            // Instantiate the selected construction
            if (currentConstruction == null)
            {
                currentConstruction = Instantiate(constructionPrefab_01);
            }
            else
            {
                // If the construction already exists, destroy it
                Destroy(currentConstruction);
            }
        }
        else if (Input.GetKeyDown(newConstructionHotkey_02))
        {
            if (currentConstruction == null)
            {
                currentConstruction = Instantiate(constructionPrefab_02);
            }
            else
            {
                Destroy(currentConstruction);
            }
        }
    }

    /// <summary>
    /// This method makes the construction follow the mouse
    /// </summary>
    private void MoveCurrentConstructionToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            // Move the current construction to the mouse position
            currentConstruction.transform.position = hitInfo.point;
            
            // Declare a local variable to store the GameObject hit by the RayCast
            GameObject g = hitInfo.collider.gameObject;

            // If the handheld item is a Cube, use the Cube Method
            if (currentConstruction.name == "Cube(Clone)") PlaceCube(g, hitInfo);

            // If the handheld item is a Cylinder, use the Cylinder Method
            if (currentConstruction.name == "Cylinder(Clone)") PlaceCylinder(g, hitInfo);
            
        }
    }

    /// <summary>
    /// This method places the Cube based on what it is placing on
    /// </summary>
    /// <param name="g">GameObject hit by the Raycast</param>
    /// <param name="hitInfo">Raycast info based on what it hit</param>
    private void PlaceCube(GameObject g, RaycastHit hitInfo)
    {
        if (g.name == "Magnet" && g.transform.parent.name == "Cube(Clone)")
        {
            if (g.transform.position.x > g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x + 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.x < g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x - 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.y > g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y + 1,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.y < g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y - 1,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.z > g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z + 1);
            }
            else if (g.transform.position.z < g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z - 1);
            }
        }
        else if (g.name == "Magnet" && g.transform.parent.name == "Cylinder(Clone)")
        {
            if (g.transform.position.x > g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x + 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.x < g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x - 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.y > g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y + 1,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.y < g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y - 1,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.z > g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z + 1);
            }
            else if (g.transform.position.z < g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z - 1);
            }
        }
        else
        {
            currentConstruction.transform.position = new Vector3(hitInfo.point.x,
                hitInfo.point.y + transform.localScale.y / 2,
                hitInfo.point.z);
        }
    }
    /// <summary>
    /// This method places the Cylinder based on what it is placing on
    /// </summary>
    /// <param name="g">GameObject hit by the Raycast</param>
    /// <param name="hitInfo">Raycast info based on what it hit</param>
    private void PlaceCylinder(GameObject g, RaycastHit hitInfo)
    {

        if (g.name == "Magnet" && g.transform.parent.name == "Cube(Clone)")
        {
            if (currentConstruction.transform.localRotation.y == 0 || currentConstruction.transform.localRotation.y == 180)
            {
                if (g.transform.position.y > g.transform.parent.position.y)
                {
                    currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                         g.transform.parent.position.y + 1.5f * transform.localScale.y,
                                                                         g.transform.parent.position.z);
                }
                else if (g.transform.position.y < g.transform.parent.position.y)
                {
                    currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                         g.transform.parent.position.y - 1.5f * transform.localScale.y,
                                                                         g.transform.parent.position.z);
                }
            }
            else if (currentConstruction.transform.localRotation.x == 0 || currentConstruction.transform.localRotation.x == 180)
            {                 
                if (g.transform.position.x > g.transform.parent.position.x)
                {
                    currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                         g.transform.parent.position.y,
                                                                         g.transform.parent.position.z);
                }
                else if (g.transform.position.x < g.transform.parent.position.x)
                {
                    currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                         g.transform.parent.position.y,
                                                                         g.transform.parent.position.z);
                }
            }

            if (g.transform.position.z > g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z + 1);
            }
            else if (g.transform.position.z < g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z - 1);
            }
        }
        else if (g.name == "Magnet" && g.transform.parent.name == "Cylinder(Clone)")
        {
            if (g.transform.position.x > g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x + 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.x < g.transform.parent.position.x)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x - 1,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.y > g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z);
            }
            else if (g.transform.position.y < g.transform.parent.position.y)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y - 1,
                                                                     g.transform.parent.position.z);
            }

            if (g.transform.position.z > g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z + 1);
            }
            else if (g.transform.position.z < g.transform.parent.position.z)
            {
                currentConstruction.transform.position = new Vector3(g.transform.parent.position.x,
                                                                     g.transform.parent.position.y,
                                                                     g.transform.parent.position.z - 1);
            }
        }
        else
        {
            currentConstruction.transform.position = new Vector3(hitInfo.point.x,
                hitInfo.point.y + transform.localScale.y,
                hitInfo.point.z);
        }
    }

    /// <summary>
    /// This method does the rotation logic
    /// </summary>
    private void RotateConstruction()
    {
        if(Input.GetKeyDown(rotateLeftHotkey))
        {
            currentConstruction.transform.Rotate(Vector3.right, 90f);
        }
        if (Input.GetKeyDown(rotateRightHotkey))
        {
            currentConstruction.transform.Rotate(Vector3.forward, 90f);
        }

    }

    /// <summary>
    /// This method gets the item placed
    /// </summary>
    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeLayersRecursively(currentConstruction.transform, LayerMask.NameToLayer("BuildingColliders"));

            currentConstruction = null;
        }
    }

    /// <summary>
    /// This Method changes the layer of the final object before being placed
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="layerName"></param>
    private void ChangeLayersRecursively(Transform trans, LayerMask layerName)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.layer = layerName;
            ChangeLayersRecursively(child, layerName);
        }
    }

}