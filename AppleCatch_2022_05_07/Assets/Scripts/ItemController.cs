using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public float dropSpeed = -0.03f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, this.dropSpeed, 0);

        if (transform.position.y < -1.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        this.GetComponent<Rigidbody>().isKinematic = true;

        if (other.gameObject.tag == "basket") {
            this.GetComponent<ParticleSystem>().Play();
        }


    }

    void OnTriggerEnter(Collider other) { // �浹 ���� ����
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        this.GetComponent<Rigidbody>().isKinematic = true;

        if (other.gameObject.tag == "basket")
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }

    void OnTriggerStay(Collider other) // �浹 ��
    {
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        this.GetComponent<Rigidbody>().isKinematic = true;

        if (other.gameObject.tag == "basket")
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }

    void OnTriggerExit(Collider other) // �浹 ���� ����
    {
        this.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        this.GetComponent<Rigidbody>().isKinematic = true;

        if (other.gameObject.tag == "basket")
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }
}
