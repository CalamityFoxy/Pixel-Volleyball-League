using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarColliderPlayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    private CircleCollider2D hitCollider;

    public float PosicionNetX;
    public GameObject ball;

    void Start()
    {
        hitCollider = GetComponent<CircleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.x > PosicionNetX)
        {
            hitCollider.enabled = false;
        }
        else
        {
            hitCollider.enabled = true;

        }

    }
}

