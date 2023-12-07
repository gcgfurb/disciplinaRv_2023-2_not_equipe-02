using UnityEngine;

public class Detector : MonoBehaviour
{
    Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou na zona de gatilho é o objeto desejado
        if (other.CompareTag("Part"))
        {
            // Debug.Log("O objeto desejado está dentro da zona de gatilho!");
        }
    }
}
