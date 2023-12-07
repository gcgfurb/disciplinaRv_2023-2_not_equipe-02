using UnityEngine;

public class MagneticTransform : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Transform m_Transform;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AjustarRotacao(float targetAngleX, float targetAngleY, float targetAngleZ)
    {
        // Atualizar a rotação do objeto para o ângulo alvo
        m_Transform.eulerAngles = new Vector3(targetAngleX, targetAngleY, targetAngleZ);
    }

    public void TrasnformComponent()
    {
        float currentAngleX = Mathf.Abs(m_Transform.eulerAngles.x);
        float currentAngleY = Mathf.Abs(m_Transform.eulerAngles.y);
        float currentAngleZ = Mathf.Abs(m_Transform.eulerAngles.z);

        // Debug.Log(currentAngleY);

        #region rotationY
        if (currentAngleY < 45 || currentAngleY >= 315)
        {
            currentAngleY = 0.0f;
        }
        else if (currentAngleY >= 45 && currentAngleY < 135)
        {
            currentAngleY = 90.0f;
        }
        else if (currentAngleY >= 135 && currentAngleY < 225)
        {
            currentAngleY = 180.0f;
        }
        else if (currentAngleY >= 225 && currentAngleY < 315)
        {
            currentAngleY = -90.0f;
        }
        #endregion

        #region rotationX
        if (currentAngleX < 45 || currentAngleX >= 315)
        {
            currentAngleX = 0.0f;
        }
        else if (currentAngleX >= 45 && currentAngleX < 135)
        {
            currentAngleX = 90.0f;
        }
        else if (currentAngleX >= 135 && currentAngleX < 225)
        {
            currentAngleX = 180.0f;
        }
        else if (currentAngleX >= 225 && currentAngleX < 315)
        {
            currentAngleX = -90.0f;
        }
        #endregion

        #region rotationZ
        if (currentAngleZ < 45 || currentAngleZ >= 315)
        {
            currentAngleZ = 0.0f;
        }
        else if (currentAngleZ >= 45 && currentAngleZ < 135)
        {
            currentAngleZ = 90.0f;
        }
        else if (currentAngleZ >= 135 && currentAngleZ < 225)
        {
            currentAngleZ = 180.0f;
        }
        else if (currentAngleZ >= 225 && currentAngleZ < 315)
        {
            currentAngleZ = -90.0f;
        }
        #endregion

        AjustarRotacao(currentAngleX, currentAngleY, currentAngleZ);

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
