
using System.Collections;
using UnityEngine;
using TMPro;

public class JokalariMugitu : MonoBehaviour
{
    [Header("Jokalariaren ibilbidea")]
    public Transform[] ibilbidea;
    public float mugitzekoAbiadura = 2f;
    public float iristekoDistantzia = 0.15f;

    [Header("Elkarrizketaren interfazea")]
    public GameObject elkarrizketaPanela;
    public TMP_Text izenburukoTestua;
    public TMP_Text azalpenTestua;

    public float testuAbiadura = 0.035f;
    public float irakurtzekoDenbora = 4f;

    private int unekoPuntua = 0;
    private bool itxaroten = false;

    void Start()
    {
        // Elkarrizketa-panela hasieran ezkutatu.
        if (elkarrizketaPanela != null)
        {
            elkarrizketaPanela.SetActive(false);
        }
    }

    void Update()
    {
        // Ibilbidean punturik ez badago, ez mugitu.
        if (itxaroten || ibilbidea == null ||
            ibilbidea.Length == 0)
        {
            return;
        }

        if (unekoPuntua >= ibilbidea.Length ||
            ibilbidea[unekoPuntua] == null)
        {
            return;
        }

        Transform helmuga = ibilbidea[unekoPuntua];

        // Jokalaria hurrengo punturantz mugitu.
        transform.position = Vector2.MoveTowards(
            transform.position,
            helmuga.position,
            mugitzekoAbiadura * Time.deltaTime
        );

        // Helmugara iritsi den egiaztatu.
        if (Vector2.Distance(transform.position, helmuga.position)
            <= iristekoDistantzia)
        {
            transform.position = helmuga.position;
            StartCoroutine(HelmugaraIritsi(helmuga));
        }
    }

    IEnumerator HelmugaraIritsi(Transform helmuga)
    {
        itxaroten = true;

        // Puntuaren informazioa eskuratu.
        AnimazioaInfo informazioa =
            helmuga.GetComponent<AnimazioaInfo>();

        if (informazioa != null)
        {
            if (elkarrizketaPanela != null)
            {
                elkarrizketaPanela.SetActive(true);
            }

            if (izenburukoTestua != null)
            {
                izenburukoTestua.text =
                    informazioa.gunearenIzena;
            }

            // Azalpena letraz letra erakutsi.
            if (azalpenTestua != null)
            {
                azalpenTestua.text = "";

                foreach (char letra in informazioa.azalpena)
                {
                    azalpenTestua.text += letra;

                    yield return new WaitForSeconds(
                        testuAbiadura
                    );
                }
            }

            // Irakurtzeko denbora utzi.
            yield return new WaitForSeconds(
                irakurtzekoDenbora
            );

            if (elkarrizketaPanela != null)
            {
                elkarrizketaPanela.SetActive(false);
            }
        }

        // Hurrengo puntura pasatu.
        unekoPuntua++;

        // Ibilbidea amaitzean, hasierara itzuli.
        if (unekoPuntua >= ibilbidea.Length)
        {
            unekoPuntua = 0;
        }

        itxaroten = false;
    }
}
