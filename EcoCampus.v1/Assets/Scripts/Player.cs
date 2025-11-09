using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    // vou usar o SerializeField para expor a variavel Speed no inspetor do Unity e dexala privada por razões de encapsulamento
    [SerializeField] private float Speed;
    [SerializeField] private float runSpeed;
    // componentes: 
    private Rigidbody2D rig;

    //atributos:

    private float inicialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private Vector2 _direction;


    //construtores:
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }



    //metodos:
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        inicialSpeed = Speed;
    }

    
    private void Update()
    {
       onInput();

       onRun();

       onRolling();

    }

    private void FixedUpdate()
    {
        onMove();


    }

    // #region server para organizar um bloco de codigo
    #region Movement 

    void onInput()
    {
        // entrada do player
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }

        void onMove()
        {
        // movimentação do player
        rig.MovePosition(rig.position + _direction * Speed * Time.fixedDeltaTime);

    }

    void onRun()
    {
        // corrida do player
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = runSpeed;
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = inicialSpeed;
            isRunning = false;

        }

    }
    void onRolling()
    {
        // rolar do player
        if (Input.GetMouseButtonDown(1)) //valor de 1 para o botao direito do mouse e valor 0 para o botao esquerdo

        {
            _isRolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isRolling = false;
        }

    }

    #endregion
}
