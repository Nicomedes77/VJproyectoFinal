using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 500f;
    public float rotateSpeed = 4f;
    public int life = 4;
    public float tiempoContactoEnemigo = 3f;
    public float tiempoRestanteEnemigo;
    public float tiempoInactivo = 5f;
    public float tiempoRestanteInactivo;    
    public GameObject camUno,camDos, camTres;    // camUno = camara 3ra persona || camDos = primera persona ascendente || camTres = primera persona descendente
    public Animator anim;
    public Light luzDolor;
    public Rigidbody rb;

    //variables para raycast
    public float range = 100f;
    public Camera fpsCamera;

    //public PartycleSystem flashEffect;
    //public GameObject impactEffect;

    void Start()
    {
        timesInit();    //inicializa los tiempos para temporizador de advertencia sonora "Lets Go!" y para temporizador de daño a jugador
        camInit();      //inicializa camaras
        lightInit();    //inicializa luz indicadora de nivel de daño
    }

    void Update()
    {
        Movement();                 //funcion que gestiona movimiento
        Skill();                    //funcion que gestiona habilidades
        ToggleCamera();             //funcion que cambia camara por presionar B
        DeathCondition(life);       //funcion que gestiona si el jugador esta vivo o muerto
    }

    void timesInit()
    {
        tiempoRestanteEnemigo = tiempoContactoEnemigo;
        tiempoRestanteInactivo = tiempoInactivo;
    }

    void Golpear()  //gestiona raycast para enfrentamiento con enemigo
    {
        RaycastHit hit;
        //fpsCamera.transform.position -> de donde sale el rayo
        //fpsCamera.transform.forward -> indico que el rayo salga para adelante
        //out hit -> guarda en la variable hit los datos del impacto
        if(Physics.Raycast(fpsCamera.transform.position , fpsCamera.transform.forward , out hit , range))
        {
            if((hit.transform.tag == "Cazador") || (hit.transform.tag == "Marciano"))   ManagerSonido.Sonido_punch();
        }
    }

    void Saludar()  //gestiona raycast para enfrentamiento con vecino
    {
        RaycastHit hit;
        //fpsCamera.transform.position -> de donde sale el rayo
        //fpsCamera.transform.forward -> indico que el rayo salga para adelante
        //out hit -> guarda en la variable hit los datos del impacto
        if(Physics.Raycast(fpsCamera.transform.position , fpsCamera.transform.forward , out hit , range))
        {
            if(hit.transform.tag == "Vecino")
            {
              RecibeEnergia(life);  
              ManagerSonido.Sonido_yeah();
            }
        }        
    }

    void lightInit()
    {
        luzDolor.enabled = false;
    }

    void camInit()
    {
        camUno.SetActive(true);
        camDos.SetActive(false);
        camTres.SetActive(false);
    }

    void Movement()
    {
        float ver = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float hor = Input.GetAxis("Horizontal") * speed * rotateSpeed * Time.deltaTime;
        //float ver = Input.GetAxis("Vertical");
        //float hor = Input.GetAxis("Horizontal");
        Vector3 inputPlayer = new Vector3(0 , 0 , ver);
        //Vector3 inputPlayer = new Vector3(hor , 0 , ver);

        transform.Rotate(0, hor, 0);
        transform.Translate(0, 0, ver);
        //rb.AddForce(inputPlayer * speed * Time.deltaTime);

        if(inputPlayer != Vector3.zero)
        {
            anim.SetBool("Corriendo", true);
            tiempoRestanteInactivo = tiempoInactivo;
            ChangeCamera();
        }
        else
        {
            anim.SetBool("Corriendo", false);
            TiempoParado();
        }
    }

    void TiempoParado()
    {
            tiempoRestanteInactivo -= Time.deltaTime;
            if(tiempoRestanteInactivo <= 0)
            {
                ManagerSonido.Sonido_LetsGo();
                tiempoRestanteInactivo = tiempoInactivo;
            }
    }

    void Skill()
    {
        if (Input.GetKey(KeyCode.Z))    anim.SetBool("Saltando", true);
        else                            anim.SetBool("Saltando", false);  

        if (Input.GetKey(KeyCode.X))    anim.SetBool("AgarrandoAlgo", true);
        else                            anim.SetBool("AgarrandoAlgo", false);

        if (Input.GetKey(KeyCode.C))    anim.SetBool("Pegando", true);
        else                            anim.SetBool("Pegando", false);

        if (Input.GetKey(KeyCode.V))    anim.SetBool("Saludando", true);
        else                            anim.SetBool("Saludando", false);
    }

    void ToggleCamera()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(camUno.activeInHierarchy)
            {
                camUno.SetActive(false);
                camDos.SetActive(true);
                camTres.SetActive(true);
            }
            else if(camDos.activeInHierarchy)
            {
                camUno.SetActive(false);
                camDos.SetActive(false);
                camTres.SetActive(true);                
            }
            else
            {
                camUno.SetActive(true);
                camDos.SetActive(false);
                camTres.SetActive(false);
            }
        }
    }

    void ChangeCamera()
    {
        if(camDos.activeInHierarchy)
        {
            camUno.SetActive(true);
            camDos.SetActive(false);  
            camTres.SetActive(false);           
        }
    }

    
    void OnTriggerStay(Collider col)
    {
        camUno.SetActive(false);
        camDos.SetActive(true);

        if ((col.transform.gameObject.tag == "Cazador") || (col.transform.gameObject.tag == "Marciano"))
        {
            camUno.SetActive(false);
            camDos.SetActive(true);
            camTres.SetActive(false);

            tiempoRestanteEnemigo -= Time.deltaTime;
            if(tiempoRestanteEnemigo <= 0)
            {              
                RecibeDaño(life);
                NivelDaño(life);
                tiempoRestanteEnemigo = tiempoContactoEnemigo;
            }

            if (Input.GetKey(KeyCode.C))
            {
                tiempoRestanteEnemigo = tiempoContactoEnemigo;
                Golpear();
            }
        }    

        if (col.transform.gameObject.tag == "Portal")
        {
            Debug.Log("Avanza siguiente nivel");
        }

        if (col.transform.gameObject.tag == "Vecino")
        {
            camUno.SetActive(false);
            camDos.SetActive(true);
            camTres.SetActive(false);

            if (Input.GetKey(KeyCode.V))    Saludar();
        }       

        if (col.transform.gameObject.tag == "Alimento")
        {
            camUno.SetActive(false);
            camDos.SetActive(false);
            camTres.SetActive(true);

            if (Input.GetKey(KeyCode.X))
            {
                ManagerSonido.Sonido_Alimento();
                RecibeEnergia(life);
            }    
        }                 
    }

    void OnTriggerExit(Collider col)
    {
            camUno.SetActive(true);
            camDos.SetActive(false);
            camTres.SetActive(false);            
    }

    void RecibeDaño(int life)
    {
        ManagerSonido.Sonido_HeridaPlayer();
        life--;
    }

    void RecibeEnergia(int life)
    {
        life++;
    }

    void NivelDaño(int life)
    {
        switch (life)
        {
            case 3:
                luzDolor.enabled = false;
                luzDolor.intensity = 0;
            break;

            case 2:
                luzDolor.enabled = true;
                luzDolor.intensity = 3;
            break;

            case 1:
                luzDolor.enabled = true;
                luzDolor.intensity = 6;
            break;

            case 0:
                luzDolor.enabled = true;
                luzDolor.intensity = 8;
            break;
            default: break;
        }
    }

    void DeathCondition(int life)
    {
        if(life <= 0)
        {
            anim.SetBool("Muriendo", true);
            ManagerSonido.Sonido_MuertePlayer();
        }
    }       
}
