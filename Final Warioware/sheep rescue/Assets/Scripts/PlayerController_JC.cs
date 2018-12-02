using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_JC : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public Text timerText;
    public Text countText;
    private float timer;
    public Text winText;
    private int count;
    private object endText;
    private IEnumerator coroutine;
    public AudioClip pickup;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        // Destroy(gameObject, 2);
        count = 0;
        timer = 0;
        SetCountText();
        winText.text = "";
            rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        // = GetComponent<AudioSource>();
        //yield return new WaitForSeconds(time);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "flower_JC")
            audioSource.PlayOneShot(pickup, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);
        timer = timer + Time.deltaTime;
        timerText.text = "Timer: " + timer.ToString("N2");
        if (timer >= 10)
        {
            //endText.text = "You Lose! :(";
            StartCoroutine(ByeAfterDelay(2));

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.gameObject.CompareTag("flower_JC"))
        {
            audioSource.PlayOneShot(pickup, 0.7f);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 11)
            winText.text = "You win!";
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameLoader.gameOn = false;
    }
}

//Use spacebar and arrows to control the player!!! It works on the arcade box