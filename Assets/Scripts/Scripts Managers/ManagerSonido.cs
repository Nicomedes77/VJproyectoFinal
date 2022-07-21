using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSonido : MonoBehaviour
{
    public static ManagerSonido unicaInstancia;
    public AudioSource _audioSPlayerAlimento;
    public AudioSource _audioSLetsGo;
    public AudioSource _audioSMuerteCazador;
    public AudioSource _audioSMuertePlayer;
    public AudioSource _audioSHeridaPlayer;    
    public AudioSource _audioSMuerteMarciano;
    public AudioSource _audioSFondo;
    public AudioSource _audioSPlayerPunch;
    public AudioSource _audioSPlayerYeah;
    
    void Awake()
    {
        if(ManagerSonido.unicaInstancia == null)
        {
            //primera instancia
            ManagerSonido.unicaInstancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public static void Sonido_Alimento()
    {
        unicaInstancia._audioSPlayerAlimento.Play();
    }

    public static void Sonido_LetsGo()
    {
        unicaInstancia._audioSLetsGo.Play();
    }

    public static void Sonido_MuerteCazador()
    {
        unicaInstancia._audioSMuerteCazador.Play();       
    }

    public static void Sonido_MuerteMarciano()
    {
        unicaInstancia._audioSMuerteMarciano.Play();     
    }

    public static void Sonido_MuertePlayer()
    {
        unicaInstancia._audioSMuertePlayer.Play();     
    }

    public static void Sonido_HeridaPlayer()
    {
        unicaInstancia._audioSHeridaPlayer.Play();       
    }

    public static void Sonido_Fondo()
    {
        unicaInstancia._audioSFondo.Play();       
    }  

    public static void Sonido_punch()
    {
        unicaInstancia._audioSPlayerPunch.Play();        
    }

    public static void Sonido_yeah()
    {
        unicaInstancia._audioSPlayerYeah.Play();
    }
}
