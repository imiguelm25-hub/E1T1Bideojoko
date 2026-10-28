using UnityEngine;

public class KameraJarraitu : MonoBehaviour
{
    [Header("Helburua / Objetivo")]
    public Transform jokalaria;

    [Header("Kameraren Ezarpenak / Ajustes")]
    public float leuntasuna = 5f;

    [Header("Maparen Mugak / Límites del Mapa")]
    public Vector2 mugaMin;
    public Vector2 mugaMax; 

    void LateUpdate()
    {
        if (jokalaria != null)
        {
            float xHelburua = jokalaria.position.x;
            float yHelburua = jokalaria.position.y;

            float xMugatuta = Mathf.Clamp(xHelburua, mugaMin.x, mugaMax.x);
            float yMugatuta = Mathf.Clamp(yHelburua, mugaMin.y, mugaMax.y);

            Vector3 posizioBerria = new Vector3(xMugatuta, yMugatuta, transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position, posizioBerria, leuntasuna * Time.deltaTime);
        }
    }
}