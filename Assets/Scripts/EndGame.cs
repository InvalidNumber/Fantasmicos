using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//para travajar la interfaz
using UnityEngine.SceneManagement;//libreria para cargar las escenas

public class EndGame : MonoBehaviour
{
    public float fadeDuration = 1;//duracion del fade de la imagen(poner el cana alfa al maximo)
    public float displayImageDuration = 1;//tiempo que va a estar la imagen en pantalla
    public GameObject player;
    public AudioClip caughtClip;
    
    public AudioClip wonClip;
    public Image caughtImage;
    public Image wonImage;

    AudioSource audioSource;
    bool isPlayerAtExit;//booleana para saber si ha llegado a la salida
    bool isPlayerCaught;//booleana para si el player fue atarapado
    float timer;//contador de tiempo
    bool hasAudioPlayed;//si el audio ya esta en play

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player) isPlayerAtExit = true;//si el player entra en el clloider pongo a true la booleana

    }

    public void CaughtPlayer()//funcion publica que llamaremos desde otro script para poner la booleana a true
    {
        isPlayerCaught = true;
    }

    void Update()
    {
        if(isPlayerAtExit)
        {
            EndLevel(wonImage, false, wonClip);
        }
        else if(isPlayerCaught)
        {
            EndLevel(caughtImage, true, caughtClip);
        }
    }


    void EndLevel(Image _image, bool doRestart, AudioClip _clip)
    {
        if(!hasAudioPlayed)//si el audio no esta en play
        {
            audioSource.clip = _clip;//le digo al audiosource que clip tiene que coger
            audioSource.Play();
            hasAudioPlayed = true;//esto lo hago para darle al play una sola vez
        }

        timer += Time.deltaTime;//CONTADOR DE TIEMPO
        //new color(r g b a) siendo a el canal alpha
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, timer / fadeDuration);

        if(timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)//si quiero reiniciar el nivel
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }

    }

}
