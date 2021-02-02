using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public List<GameObject> Hearts;
    public GameObject Fire;
    public GameObject SpawnPoint;
    public GameObject GameOverMenu;

    public float moveSpeed = 5f;
    float dirX;

    int Life;

    Rigidbody2D rb;
    Animator anim;

    public bool canJump = false;
    bool facingRight = true;
    bool canDie ;
    bool Sweet1Count1;
    bool Sweet1Count2;
    bool isDead = false;

    Vector3 localScale;
    

    //public List<GameObject> Hearts = new List<GameObject>();

    void Start()
    {
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        Sweet1Count1 = false;
        Sweet1Count2 = false;
        canDie = true;
        Life = 1;
    }

    void DoJump()
    {
        if (canJump = true)
        {
            anim.SetBool("isJump", true);
            rb.AddForce(new Vector2(0, 750), ForceMode2D.Force);
            canJump = false;
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (facingRight)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (!facingRight)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        if (dirX != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DoJump();
            
        }
        if (Life == 0 && !isDead)
        {
            StartCoroutine(Dead());
            StartCoroutine(RestartMenu());
            isDead = true;
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
    void LateUpdate()
    {
        CheckWhereToFace();
    }
    void OnTriggerEnter2D(Collider2D cd)
    {

        if (cd.gameObject.tag == "Enemy")
        {
            if(!Sweet1Count1||!Sweet1Count2 && Life > 0)
            {
                Life -= 1;
                StartCoroutine(ReDead());
            }
            else
            {
                transform.localScale = new Vector2(0.75f,0.75f);
                Destroy(cd.gameObject);
            }
        }
        if (cd.gameObject.tag == "Enemy1")
        {
            if (!Sweet1Count1 || !Sweet1Count2 && Life > 0)
            {
                Life -= 1; 
                StartCoroutine(ReDead());
            }
            else
            {
                transform.localScale = new Vector2(0.75f, 0.75f);
                Destroy(cd.gameObject);
            }
        }
        if (cd.gameObject.tag=="Sweet1")
        {
            Sweet1Count1 = true;
            transform.localScale = new Vector2(1, 1);
            Destroy(cd.gameObject);
        }
        if(cd.gameObject.tag=="Sweet2")
        {
            Sweet1Count2 = true;
            Attack();
            Destroy(cd.gameObject);
        }
        if(cd.gameObject.tag=="EndLevel")
        {
            SceneManager.LoadScene("Level1");
        }
        if (cd.gameObject.tag == "EndLevel2")
        {
            SceneManager.LoadScene("Level2");
        }
        if (cd.gameObject.tag == "EndLevel3")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(Fire, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        }
    }
    IEnumerator RestartMenu()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
    }

    IEnumerator Dead()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator ReDead()
    {
        Life -= 1;
        anim.SetBool("isDead", true);
        GetComponent<BoxCollider2D>().enabled = false;
        if(Hearts.Count>0)
        {
            Hearts[0].SetActive(false);
            Hearts.RemoveAt(0);
        }
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = true;
        anim.SetBool("isDead", false);
        transform.position = new Vector2(-15.5f, 3.7f);
    }
}


