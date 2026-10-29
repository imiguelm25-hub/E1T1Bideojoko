
using UnityEngine;

public class JokalariKudeatzailea : MonoBehaviour
{
    [Header("Mugimendua")]
    public float abiadura = 4f;

    [Header("Bizitzak")]
    public int bizitzak = 3;
    private Vector3 hasierakoPosizioa;

    [Header("Animazioen izenak")]
    public string animBehera = "DownAnimation";
    public string animGora = "UpAnimation";
    public string animEzker = "LeftAnimation";
    public string animEskuin = "RightAnimation";

    private Rigidbody2D gorputzZurruna;
    private Animator animatzailea;
    private Vector2 norabidea;

    void Start()
    {
        gorputzZurruna = GetComponent<Rigidbody2D>();
        animatzailea = GetComponent<Animator>();

        // Jokalariaren hasierako posizioa gorde.
        hasierakoPosizioa = transform.position;

        Debug.Log("Hasierako bizitzak: " + bizitzak);
    }

    void Update()
    {
        // Teklatuaren bidezko mugimenduaren norabidea jaso.
        norabidea.x = Input.GetAxisRaw("Horizontal");
        norabidea.y = Input.GetAxisRaw("Vertical");

        // Mugimendu diagonala abiadura berean mantendu.
        norabidea = norabidea.normalized;

        AnimazioakEguneratu();
    }

    void FixedUpdate()
    {
        // Jokalaria mugitu.
        if (gorputzZurruna != null)
        {
            gorputzZurruna.MovePosition(
                gorputzZurruna.position +
                norabidea * abiadura * Time.fixedDeltaTime
            );
        }
    }

    void AnimazioakEguneratu()
    {
        if (animatzailea == null)
            return;

        if (norabidea != Vector2.zero)
        {
            animatzailea.speed = 1f;

            if (norabidea.x > 0)
                animatzailea.Play(animEskuin);
            else if (norabidea.x < 0)
                animatzailea.Play(animEzker);
            else if (norabidea.y > 0)
                animatzailea.Play(animGora);
            else if (norabidea.y < 0)
                animatzailea.Play(animBehera);
        }
        else
        {
            // Mugimendurik ez dagoenean, animazioa gelditu.
            animatzailea.speed = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D besteObjektua)
    {
        // Arrisku batekin talka egin den egiaztatu.
        if (besteObjektua.CompareTag("Arriskua"))
        {
            BizitzaGaldu();
        }
    }

    void BizitzaGaldu()
    {
        bizitzak--;

        Debug.Log(
            "Bizitza bat galdu duzu! Geratzen diren bizitzak: "
            + bizitzak
        );

        if (bizitzak > 0)
        {
            // Jokalaria hasierako posiziora itzuli.
            transform.position = hasierakoPosizioa;

            if (gorputzZurruna != null)
            {
                gorputzZurruna.position =
                    new Vector2(hasierakoPosizioa.x,
                                hasierakoPosizioa.y);
            }
        }
        else
        {
            // Bizitzarik gabe geratzean, jokoa amaitu.
            Debug.Log(
                "JOKOA AMAITU DA! Ez zaizu bizitzarik geratzen."
            );

            gameObject.SetActive(false);
        }
    }
}
