using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    bool vertical;
    [SerializeField]
    float changeTime = 3f;

    Rigidbody2D myRigid;
    float timer;
    int direction = 1;
    void Awake()
    {
        myRigid = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = myRigid.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;

        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        myRigid.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
