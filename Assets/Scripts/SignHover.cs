using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignHover : MonoBehaviour
{
    public GameObject signCanvas;
    [TextArea(2, 10)]
    public string signText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signCanvas.transform.position = transform.position;
            signCanvas.SetActive(true);
            signCanvas.transform.Find("Sign Text").transform.Find("Speech Text").gameObject.GetComponent<TextMeshProUGUI>().text = signText;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signCanvas.SetActive(false);
        }
    }
}
