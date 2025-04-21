using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //controlador del personaje
    public CharacterController characterController;
    
    //velocidad del personaje
    public float speed = 10f;

    //gravedad del personaje
    private float gravity = -15f;

    //transformacion del personaje
    public Transform groundCheck;

    //objeto para detectar que el jugador esta tocando el suelo
    public float sphereRadius = 0.3f;


    public LayerMask groundMask;

    //para detectar si se esta tocando el suelo
    bool isGrounded;

    
    Vector3 velocity;

    public Animator animator;

    //altura de salto
    public float jumpHeight = 0.5f;

    void Update()
    {

        //checar posicion para saber si se esta tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        //si se esta tocando el suelo, reducir velocidad de caida a 0
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //caminar
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelZ", z);

        Vector3 move = transform.right * x + transform.forward * z;

        //poner una tecla para salto y detectar si el personaje esta en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
        {
            //formula de salto y caida
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        

    }
}
