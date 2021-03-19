using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D MyRigid;
    void Awake()
    {
        MyRigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = MyRigid.position;
        position.x = position.x + 50f * horizontal * Time.deltaTime;
        position.y = position.y + 50f * vertical * Time.deltaTime;

        MyRigid.MovePosition(position);
    }
}
