using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavaTile : MonoBehaviour
{
    public GameObject lavaCanvas;
    public float lengthAbleToStand;
    public int damage;

    private float timeOfDamage;
    private bool standingOnLava;
    private float knockbackLength;

    private float timeOfContact;

    private void Awake()
    {
        knockbackLength = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().knockBackLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (!standingOnLava)
        {
            timeOfDamage = Time.time + lengthAbleToStand;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Whole"))
        {
            timeOfContact = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Whole"))
        {
            lavaCanvas.transform.Find("Thermometer Image").GetComponent<Image>().fillAmount = (Time.time - timeOfContact) / lengthAbleToStand;
            lavaCanvas.transform.position = new Vector3(other.transform.position.x + 1, other.transform.position.y);
            lavaCanvas.transform.Find("Thermometer Image").GetComponent<Image>().color = new Color32(255, GetGreen(), 0, 255);
            lavaCanvas.SetActive(true);

            standingOnLava = true;
            if (Time.time >= timeOfDamage)
            {
                other.GetComponentInParent<CharacterController2D>().knockBackLength = 0;
                other.GetComponentInParent<Player>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Whole"))
        {
            other.GetComponentInParent<CharacterController2D>().knockBackLength = knockbackLength;
            standingOnLava = false;

            lavaCanvas.SetActive(false);
            lavaCanvas.transform.Find("Thermometer Image").GetComponent<Image>().fillAmount = 0f;
            lavaCanvas.transform.Find("Thermometer Image").GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
    }

    private byte GetGreen()
    {
        float fraction = (Time.time - timeOfContact) / lengthAbleToStand;
        fraction = fraction > 1 ? 1 : fraction;
        byte colour = (byte)(255 - (255 * fraction));

        return colour;
    }
}
