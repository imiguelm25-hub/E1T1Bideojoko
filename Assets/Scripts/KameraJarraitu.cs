using UnityEngine;

public class KameraJarraitu : MonoBehaviour
{
    [Header("Helburua / Objetivo")]
    public Transform jokalaria;

    [Header("Kameraren Ezarpenak / Ajustes")]
    public float leuntasuna = 5f;

    void LateUpdate()
    {
        if (jokalaria != null)
        {
            Vector3 posizioBerria = new Vector3(jokalaria.position.x, jokalaria.position.y, transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position, posizioBerria, leuntasuna * Time.deltaTime);
        }
    }
}