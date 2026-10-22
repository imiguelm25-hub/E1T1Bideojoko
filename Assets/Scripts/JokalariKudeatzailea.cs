using UnityEngine;

public class JokalariKudeatzailea : MonoBehaviour
{
    [Header("Mugimendua / Movimiento")]
    public float abiadura = 4f;

    [Header("Bizitzak / Vidas")]
    public int bizitzak = 3;
    private Vector3 hasierakoPosizioa;

    [Header("Animazioen Izenak")]
    public string animBehera = "DownAnimation";
    public string animGora = "UpAnimation";
    public string animEzker = "LeftAnimation";
    public string animEskuin = "RightAnimation";

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 norabidea;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        hasierakoPosizioa = transform.position; 
        Debug.Log("Hasierako bizitzak (Vidas iniciales): " + bizitzak);
    }

    void Update()
    {
        norabidea.x = Input.GetAxisRaw("Horizontal");
        norabidea.y = Input.GetAxisRaw("Vertical");

        norabidea = norabidea.normalized;

        AnimazioakEguneratu();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + norabidea * abiadura * Time.fixedDeltaTime);
    }

    void AnimazioakEguneratu()
    {
        if (anim == null) return;

        if (norabidea != Vector2.zero)
        {
            anim.speed = 1f;

            if (norabidea.x > 0)
                anim.Play(animEskuin);
            else if (norabidea.x < 0)
                anim.Play(animEzker);
            else if (norabidea.y > 0)
                anim.Play(animGora);
            else if (norabidea.y < 0)
                anim.Play(animBehera);
        }
        else
        {
            anim.speed = 0f; 
        }
    }

    private void OnTriggerEnter2D(Collider2D besteBat)
    {
        if (besteBat.CompareTag("Arriskua"))
        {
            BizitzaGaldu();
        }
    }

    void BizitzaGaldu()
    {
        bizitzak--;
        Debug.Log("Bizitza bat galdu duzu! Geratzen diren bizitzak: " + bizitzak);

        if (bizitzak > 0)
        {
            transform.position = hasierakoPosizioa;
        }
        else
        {
            Debug.Log("GAME OVER! Ez zaizu bizitzarik geratzen.");
            gameObject.SetActive(false); 
        }
    }
}