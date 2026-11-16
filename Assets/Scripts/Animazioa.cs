
using UnityEngine;
using TMPro;

public class Animazioa : MonoBehaviour
{
    [Header("Mugimendua")]
    public float abiadura = 2f;

    [Header("Bizitzak")]
    public int bizitzak = 3;

    [Header("Denboraren kontrola")]
    public float informazioDenbora = 5f;

    private Vector3 hasierakoPosizioa;

    [Header("Animazioen izenak")]
    public string animBehera = "DownAnimation";
    public string animGora = "UpAnimation";
    public string animEzker = "LeftAnimation";
    public string animEskuin = "RightAnimation";

    [Header("Ibilbide osoa")]
    public Transform[] ibilbidea;

    [Header("Informazio puntuak")]
    public Transform[] informazioPuntuak;

    [Header("Informazioaren edukia")]
    public string[] informazioIzenburuak;

    [TextArea(3, 6)]
    public string[] informazioAzalpenak;

    [Header("Informazio panela")]
    public GameObject informazioPanela;
    public TMP_Text izenburuaTestua;
    public TMP_Text azalpenaTestua;

    private Rigidbody2D rb;
    private Animator animatzailea;

    private int unekoPuntua = 0;

    private bool informazioaItxaroten = false;
    private bool ibilbideaAmaituta = false;

    private string unekoAnimazioa = "";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatzailea = GetComponent<Animator>();

        hasierakoPosizioa = transform.position;

        if (informazioPanela != null)
            informazioPanela.SetActive(false);

        Debug.Log("Hasierako bizitzak: " + bizitzak);

        if (rb == null)
            Debug.LogError("Rigidbody2D falta da!");

        if (animatzailea == null)
            Debug.LogError("Animator falta da!");
    }

    void FixedUpdate()
    {
        if (rb == null || informazioaItxaroten || ibilbideaAmaituta)
        {
            AnimazioaGelditu();
            return;
        }

        if (unekoPuntua >= ibilbidea.Length)
        {
            ibilbideaAmaituta = true;
            AnimazioaGelditu();

            Debug.Log("Ibilbidea amaitu da!");
            return;
        }

        Transform helmuga = ibilbidea[unekoPuntua];

        Vector2 norabidea =
            ((Vector2)helmuga.position - rb.position).normalized;

        AnimazioakEguneratu(norabidea);

        Vector2 posizioBerria = Vector2.MoveTowards(
            rb.position,
            helmuga.position,
            abiadura * Time.fixedDeltaTime
        );

        rb.MovePosition(posizioBerria);

        if (Vector2.Distance(rb.position, helmuga.position) < 0.05f)
        {
            rb.MovePosition(helmuga.position);

            AnimazioaGelditu();

            int indizea = InformazioIndizea(helmuga);

            if (indizea != -1)
            {
                InformazioaErakutsi(indizea);
            }
            else
            {
                unekoPuntua++;
            }
        }
    }

    // Mugimenduaren arabera animazioa aukeratu.
    void AnimazioakEguneratu(Vector2 norabidea)
    {
        if (animatzailea == null)
            return;

        if (norabidea == Vector2.zero)
        {
            AnimazioaGelditu();
            return;
        }

        animatzailea.speed = 1f;

        string animBerria;

        if (Mathf.Abs(norabidea.x) > Mathf.Abs(norabidea.y))
        {
            animBerria = norabidea.x > 0 ? animEskuin : animEzker;
        }
        else
        {
            animBerria = norabidea.y > 0 ? animGora : animBehera;
        }

        if (unekoAnimazioa != animBerria)
        {
            animatzailea.Play(animBerria);
            unekoAnimazioa = animBerria;
        }
    }

    // Animazioa gelditu.
    void AnimazioaGelditu()
    {
        if (animatzailea != null)
            animatzailea.speed = 0f;
    }

    // Informazio puntuaren indizea bilatu.
    int InformazioIndizea(Transform puntua)
    {
        for (int i = 0; i < informazioPuntuak.Length; i++)
        {
            if (informazioPuntuak[i] == puntua)
                return i;
        }

        return -1;
    }

    // Informazioa erakutsi.
    void InformazioaErakutsi(int indizea)
    {
        informazioaItxaroten = true;

        izenburuaTestua.text = informazioIzenburuak[indizea];
        azalpenaTestua.text = informazioAzalpenak[indizea];

        informazioPanela.SetActive(true);

        Debug.Log("Informazioa erakusten: " +
                  informazioIzenburuak[indizea]);

        // Informazioa erakusteko denbora hasi.
        Invoke(nameof(InformazioaItxiEtaJarraitu), informazioDenbora);
    }

    // Denbora amaitzean panela itxi eta jarraitu.
    void InformazioaItxiEtaJarraitu()
    {
        informazioPanela.SetActive(false);

        informazioaItxaroten = false;
        unekoPuntua++;

        Debug.Log("Hurrengo puntura mugitzen...");
    }

  
}
