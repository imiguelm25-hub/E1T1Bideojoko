
using UnityEngine;
using TMPro;

public class PuntuInformazioa : MonoBehaviour
{
    [Header("Gunearen informazioa")]
    public string gunearenIzena = "Jatetxea";

    [TextArea(3, 8)]
    public string azalpena = "Ongi etorri jatetxera!";

    [Header("Elkarrizketaren interfazea")]
    public GameObject elkarrizketaPanela;
    public TMP_Text izenburukoTestua;
    public TMP_Text azalpenTestua;

    private void OnTriggerEnter2D(Collider2D besteObjektua)
    {
        if (!besteObjektua.CompareTag("Player"))
            return;

        elkarrizketaPanela.SetActive(true);
        izenburukoTestua.text = gunearenIzena;
        azalpenTestua.text = azalpena;
    }

    private void OnTriggerExit2D(Collider2D besteObjektua)
    {
        if (besteObjektua.CompareTag("Player"))
        {
            elkarrizketaPanela.SetActive(false);
        }
    }
}
