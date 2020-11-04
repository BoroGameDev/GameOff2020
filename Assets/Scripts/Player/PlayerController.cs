using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  Rigidbody2D body;

  float horizontal;
  float vertical;

  enum MovementType
  {
    SNEAK = 1,
    WALK = 2,
    RUN = 3
  };

  MovementType currentMovementType = MovementType.WALK;
  Dictionary<MovementType, float> movementSpeed = new Dictionary<MovementType, float>()
    {
        {MovementType.SNEAK, 5.0f},
        {MovementType.WALK, 10.0f},
        {MovementType.RUN, 20.0f},
    };

  // Start is called before the first frame update
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    horizontal = Input.GetAxisRaw("Horizontal");
    vertical = Input.GetAxisRaw("Vertical");

    if (Input.GetButton("Sneak"))
    {
      currentMovementType = MovementType.SNEAK;
    }
    else if (Input.GetButton("Run"))
    {
      currentMovementType = MovementType.RUN;
    }
    else
    {
      currentMovementType = MovementType.WALK;
    }
  }

  void FixedUpdate()
  {
    Vector3 move = new Vector3(horizontal, vertical, 0);
    move = move.normalized * movementSpeed[currentMovementType];
    body.MovePosition(transform.position + move * Time.deltaTime);
  }
}
