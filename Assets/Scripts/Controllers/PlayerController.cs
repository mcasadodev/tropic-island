// Miguel Casado (c) 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    [HideInInspector]
    public float velocity;
    public float orbitH = 25;
    public float orbitV = 25;
    public float gravity = 20;
    //public float JumpForce = 10;
    //float jumpGravity = 20;
    //public bool isInWater;
    //public float speedWaterMultiplier = 0.1f;

    Vector3 horizontalMove;
    Vector3 verticalMove;
    //Vector3 jumpMove = Vector3.zero;

    CharacterController characterC;

    private float yawRotaY;
    private float pitchRotaX;

    void Awake()
    {
        characterC = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Pause.P.blockControl)
        {
            return;
        }

        // CAMBIAR POR SNAPTURN PARA VR
        //LookRotate();
    }

    void FixedUpdate()
    {
        //if (SceneController.SC.blockAction) {
        //    return;
        //}

        MoveJump();
        Correr();
    }

    void Correr()
    {
        if (Input.GetAxis("primary2DAxis_X_L") != 0 || Input.GetAxis("primary2DAxis_Y_L") != 0)
        {
            //Corre
            if (Input.GetAxis("trigger_L") > 0.3f && characterC.isGrounded == true)
            {
                velocity = 1;
            }
            else //no corre
            {
                velocity = 0.5f;
            }
        }
        else
        {
            velocity = 0;
        }
    }

    void MoveJump()
    {
        //Calcular movimiento
        horizontalMove = Camera.main.transform.right * Input.GetAxis("primary2DAxis_X_L") * speed * velocity;
        verticalMove = Camera.main.transform.forward * Input.GetAxis("primary2DAxis_Y_L") * speed * velocity;
        verticalMove.y = 0;

        //Calcular Salto
        /*
        if (characterC.isGrounded == true && Input.GetButtonDown("Jump"))
        {
            AudioManager.AM.PlaySound(AudioManager.AM.library.soundGroups[7].group[1], transform.position);
            jumpMove.y = JumpForce;
        }
        jumpMove.y -= jumpGravity * Time.fixedDeltaTime;
        */

        //Aplicar movimiento y salto
        characterC.Move((horizontalMove + verticalMove - Vector3.up * gravity /* + jumpMove */) * Time.fixedDeltaTime);
    }

    // DESACTIVADO EN Update()
    void LookRotate()
    {
        //Carcular rotaciones con ratón
        yawRotaY += orbitH * Input.GetAxis("primary2DAxis_X_R") * Time.deltaTime;
        pitchRotaX -= orbitV * Input.GetAxis("primary2DAxis_Y_R") * Time.deltaTime;

        pitchRotaX = Mathf.Clamp(pitchRotaX, -70, 45);

        //Aplicar rotaciones a la cámara
        Camera.main.transform.localRotation = Quaternion.Euler(pitchRotaX, 0, 0);

        //Rotar el personaje en la Y
        Vector3 newRota = new Vector3(transform.rotation.x, yawRotaY, transform.rotation.z);
        this.transform.rotation = Quaternion.Euler(newRota);
    }
}