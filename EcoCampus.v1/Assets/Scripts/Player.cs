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
    private PlayerItems playerItems;

    //atributos:

    private float inicialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    private Vector2 _direction;


    private int handleObj;

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

    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }




    //metodos:
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        inicialSpeed = Speed;
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handleObj = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handleObj = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            handleObj = 2;
        }



        onInput();

       onRun();

       onRolling();

       onCutting();

       onDig();

       onWatering();

    }

    private void FixedUpdate()
    {
        onMove();


    }

    // #region server para organizar um bloco de codigo
    #region Movement 

    void onWatering()
    {
        // permite que o player regue quando tiver agua no regador e decresce a quantidade de agua ao clicar
        if (handleObj == 2)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {

                isWatering = true;
                Speed = 0f;
            }
            if (Input.GetMouseButtonUp(0) || playerItems.currentWater < 0)
            {
                isWatering = false;
                Speed = inicialSpeed;
            }
            if(isWatering)
            {
                 playerItems.currentWater -= 0.01f;
            }
        }

    }


    void onDig()
    {

        if (handleObj == 1) { 
             if (Input.GetMouseButtonDown(0))
             {
                 isDigging = true;
                 Speed = 0f;
             }
             if (Input.GetMouseButtonUp(0))
             {
                 isDigging = false;
                 Speed = inicialSpeed;
             }
        }

    }

    void onCutting()
    {
        if (handleObj == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                Speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                Speed = inicialSpeed;
            }
        } 

    }
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
            Speed = runSpeed;
            _isRolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Speed = inicialSpeed;
            _isRolling = false;
        }

    }

    #endregion
}
