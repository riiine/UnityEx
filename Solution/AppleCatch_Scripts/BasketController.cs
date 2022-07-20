using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;
    GameDirector GDirect;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        this.aud = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");
        GDirect = director.GetComponent<GameDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GDirect != null && GDirect.time <= 0.0f)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0.0f, z);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            //Debug.Log("Tag=Apple");
            this.director.GetComponent<GameDirector>().GetApple();
            this.aud.PlayOneShot(this.appleSE);

            var main = GetComponent<ParticleSystem>().main;
            main.startColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            //Debug.Log("Tag=Bomb");
            this.director.GetComponent<GameDirector>().GetBomb();
            this.aud.PlayOneShot(this.bombSE);

            var main = GetComponent<ParticleSystem>().main;
            main.startColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        }

        this.GetComponent<ParticleSystem>().Play();

        Destroy(other.gameObject);
    }

}
