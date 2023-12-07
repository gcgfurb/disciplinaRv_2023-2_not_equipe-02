using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int lines = 6;
    public int columns = 6;
    public int dimensions = 6;
    public GameObject[,,] _cubesMatrix;
    public GameObject cube;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        StartMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        ScanMatrix();
    }

    void StartMatrix()
    {
        _cubesMatrix = new GameObject[lines, columns, dimensions];

        float posY = 0.3f;

        for (int x = 0; x < lines; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                for (int z = 0; z < dimensions; z++)
                {
                    float posX = Mathf.Lerp(-0.5f, 0.5f, (float)x / (float)(lines - 1));
                    float posZ = Mathf.Lerp(-0.5f, 0.5f, (float)z / (float)(columns - 1));

                    _cubesMatrix[x, y, z] = Instantiate(cube);
                    _cubesMatrix[x, y, z].transform.position = new Vector3(posX, posY+0.2f, posZ);

                    _cubesMatrix[x, y, z].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                    _cubesMatrix[x, y, z].transform.parent = this.transform;
                }

                posY += 0.2f;
            }
            posY = 0.3f;
        }
    }

    void ScanMatrix()
    {
        // Lines
        for (int y = 0; y < 6; y++)
        {
            for (int z = 0; z < 6; z++)
            {
                if (VerifyCompletedLine(y, z))
                {
                    Debug.Log("Linha completa em y = " + y + ", z = " + z);
                    RemoveLineObjects(y, z);
                }
            }
        }

        // Cols
        for (int x = 0; x < 6; x++)
        {
            for (int z = 0; z < 6; z++)
            {
                if (VerifyCompletedColumn(x, z))
                {
                    Debug.Log("Coluna completa em x = " + x + ", z = " + z);
                    RemoveColumnObjects(x, z);
                }
            }
        }

        // Depth
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (VerifyCompletedDepth(x, y))
                {
                    Debug.Log("Profundidade completa em x = " + x + ", y = " + y);
                    RemoveDepthObjects(x, y);
                }
            }
        }
    }

    #region Verify grid
    bool VerifyCompletedLine(int y, int z)
    {
        for (int x = 0; x < 6; x++)
        {
            if (_cubesMatrix[x, y, z] == null || _cubesMatrix[x, y, z].tag != "Piece")
            {
                return false;
            }
        }
        return true;
    }

    bool VerifyCompletedColumn(int x, int z)
    {
        for (int y = 0; y < 6; y++)
        {
            if (_cubesMatrix[x, y, z] == null || _cubesMatrix[x, y, z].tag != "Piece")
            {
                return false;
            }
        }
        return true;
    }

    bool VerifyCompletedDepth(int x, int y)
    {
        for (int z = 0; z < 6; z++)
        {
            if (_cubesMatrix[x, y, z] == null || _cubesMatrix[x, y, z].tag != "Piece")
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    
    #region Remove objects

    void RemoveLineObjects(int y, int z)
    {
        for (int x = 0; x < 6; x++)
        {
            // Destruir o objeto na posição [x, y, z]
            GameObject piece = _cubesMatrix[x, y, z].GameObject();
            DestroyAndReplaceCubes(piece, x, y, z);
        }
    }

    void RemoveColumnObjects(int x, int z)
    {
        for (int y = 0; y < 6; y++)
        {
            // Destruir o objeto na posição [x, y, z]
            GameObject piece = _cubesMatrix[x, y, z].GameObject();
            DestroyAndReplaceCubes(piece, x, y, z);
        }
    }

    void RemoveDepthObjects(int x, int y)
    {
        for (int z = 0; z < 6; z++)
        {
            // Destruir o objeto na posição [x, y, z]
            GameObject piece = _cubesMatrix[x, y, z].GameObject();
            DestroyAndReplaceCubes(piece, x, y, z);
        }
    }

    void DestroyAndReplaceCubes(GameObject piece, int x, int y, int z)
    {
        ScoreManager.instance.AddPoint();
        
        Transform _parent = piece.transform.parent;
        _parent.GetComponent<BoxCollider>().enabled = false;

        foreach(Transform cube in _parent.transform.parent)
        {
            if (cube.name == ""+x+y+z) {
                cube.name = "Cube(Clone)";
                _cubesMatrix[x, y, z] = cube.GameObject();
            }
        }

        Destroy(piece.gameObject);

        StartCoroutine(getNearestComponentAfterRemove(_parent));
    }

    // FIXME: This function is calculating the distance for nearestObject earlier than expected.
    // Pegar o cubo antes de removed os outros filhos
    IEnumerator getNearestComponentAfterRemove(Transform parent)
    {
        yield return new WaitForSeconds(2.0f);

        parent.GetComponent<DraggableItem>().StopDrag();
    }
    #endregion

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou na zona de gatilho
        if (other.CompareTag("Part"))
        {
            DraggableItem draggableItem = other.GetComponent<DraggableItem>();

            draggableItem.m_parentAfterDrag = transform;
        }
    }
}
