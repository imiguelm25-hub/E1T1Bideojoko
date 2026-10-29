
using UnityEngine;

public class AnimazioaInfo : MonoBehaviour
{
    [Header("Gunearen informazioa")]
    public string gunearenIzena = "Hasiera";

    [TextArea(3, 8)]
    public string azalpena = "Ongi etorri kanpinera!";

    [Header("Gunearen etiketa")]
    public GameObject etiketa;

    void Start()
    {
        // Etiketa aktibatu, baldin badago.
        if (etiketa != null)
        {
            etiketa.SetActive(true);
        }
    }
}
