using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DraggableItem : MonoBehaviour
{
    public Transform m_parentAfterDrag;
    public bool b_isInsideGrid = false;
    public float timeToFreeze = 2.0f;

    void Update()
    {
    }

    public void StopDrag()
    {
        transform.SetParent(m_parentAfterDrag);
        
        Transform nearestObject = FindNearestObject(transform);

        if (!b_isInsideGrid) 
        {
            transform.SetParent(null);
        }

        if (nearestObject != null && transform.parent != null)
        {
            transform.localPosition = nearestObject.transform.localPosition;
            
            StartCoroutine(freezeComponent());

            // Replace cubes in matrix
            ReplaceObjectInGrid();
        }

        TetrominoSpawn.instance.canSpawn = true;
    }

    IEnumerator freezeComponent()
    {
        yield return new WaitForSeconds(timeToFreeze);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<XRGrabInteractable>().enabled = false;
    }

    void ReplaceObjectInGrid()
    {
        Transform parent = transform.parent;

        foreach (Transform piece in transform.GetComponentsInChildren<Transform>())
        {
            if (!piece.name.Contains("Ray") && piece.GetComponent<Collider>().CompareTag("Piece")) {
                
                Transform nearestObject = FindNearestObject(piece);

                for (int x = 0; x < 6; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        for (int z = 0; z < 6; z++)
                        {
                            if (parent.GetComponent<Grid>()._cubesMatrix[x, y, z] == nearestObject.GameObject())
                            {
                                // nearestObject.GetComponent<MeshRenderer>().material.color = new Color(0, 204, 102);

                                parent.GetComponent<Grid>()._cubesMatrix[x, y, z].GameObject().name = ""+x+y+z;

                                parent.GetComponent<Grid>()._cubesMatrix[x, y, z] = piece.GameObject();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private Transform FindNearestObject(Transform transform)
    {
        Transform nearestObject = null;
        float minDistance = float.MaxValue;

        // Itera sobre todos os filhos do objeto pai
        foreach (Transform child in m_parentAfterDrag)
        {
            if (!child.GetComponent<Collider>().CompareTag("Part"))
            {
                // Calcula a distância entre o objeto atual e o filho
                float distance = Vector3.Distance(transform.position, child.position);

                // Verifica se a distância é menor que a distância mais próxima registrada
                if (distance < minDistance && child != transform)
                {
                    minDistance = distance;
                    nearestObject = child;
                }
            }
        }

        return nearestObject;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Grid"))
        {
            b_isInsideGrid = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Grid"))
        {
            b_isInsideGrid = false;
        }
    }
}