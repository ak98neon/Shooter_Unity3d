/*
 * @author Artem Kudrya
 * @date 05.11.2016
 * @see Назначение: Основной скрипт который отвечает за передвижение игрока
 */
  
using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour {

    [SerializeField]
    private float spawnSpeed = 5.0f; //Скорость передвижения игрока при старте
    [SerializeField]
    private float speed; //Текущая скорость игрока
    [SerializeField]
    private float boostSpeed = 9.0f; //Скорость игрока с ускорением
    [SerializeField]
    private float forceJump = 10.0f; //Сила прыжка

    #region Animation
    private Animation anim; //Активация анмицаии на игроке
    public AnimationClip move; //Анимация ходьбы прямо
    #endregion

    #region MovePlayer_CharController
    private CharacterController _charController;
    Vector3 movement;
    private float gravity = 14.0f;
    #endregion

    void Start () {
        movement = new Vector3(0, 0, 0);

        anim = GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;

        _charController = GetComponent<CharacterController>();
        speed = spawnSpeed;
	}

    void Update() {
        Move();
        ForceSpeed();
	}

    public void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            charControllerMove(Vector3.forward);
            anim.Play("moveAnimation");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            charControllerMove(Vector3.back);
            anim.Play("moveAnimation");
        } else
        {
            anim.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            charControllerMove(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            charControllerMove(Vector3.right);
        } else
        {
            //anim.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCharacterController(forceJump * 30);
        } else
        {
            jumpCharacterController(-gravity);
        }
    }

    public void charControllerMove(Vector3 napravlenie)
    {
        movement = napravlenie;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= speed;
        movement.y = -gravity;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement * Time.deltaTime);
    }

    public void jumpCharacterController(float verticalVelocity)
    {
        Vector3 moveJump = new Vector3(0, verticalVelocity, 0);
        _charController.Move(moveJump * Time.deltaTime);
    }

    public void ForceSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = boostSpeed;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            speed = spawnSpeed;
        }
    }
}
