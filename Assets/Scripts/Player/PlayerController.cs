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
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");
    if (Input.GetKey(KeyCode.LeftShift))
    {
      currentMovementType = MovementType.SNEAK;
    }
    else if (Input.GetKey(KeyCode.Space))
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
    Vector2 move = new Vector2(horizontal, vertical);
    if (move.magnitude > 1)
    {
      move = move.normalized;
    }
    move = move * movementSpeed[currentMovementType];
    body.velocity = move;
  }
}
