using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI textoObjetos;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoMensaje;

    public float tiempoInicial = 60f;

    private float tiempoActual;
    private int objetosAgarrados = 0;
    private int totalObjetos;
    private bool juegoTerminado = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tiempoActual = tiempoInicial;

        totalObjetos = FindObjectsOfType<Pickup>().Length;

        if (textoMensaje != null)
        {
            textoMensaje.text = "";
        }

        ActualizarUI();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && juegoTerminado)
        {
            ReiniciarEscena();
        }

        if (juegoTerminado == true)
        {
            return;
        }

        tiempoActual = tiempoActual - Time.deltaTime;

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            Perder();
        }

        ActualizarUI();
    }

    public void AgarrarObjeto()
    {
        if (juegoTerminado == true)
        {
            return;
        }

        objetosAgarrados++;

        if (objetosAgarrados >= totalObjetos)
        {
            Ganar();
        }

        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (textoObjetos != null)
        {
            textoObjetos.text = "Objetos: " + objetosAgarrados + " / " + totalObjetos;
        }

        if (textoTiempo != null)
        {
            textoTiempo.text = "Tiempo: " + FormatearTiempo(tiempoActual);
        }
    }

    private string FormatearTiempo(float tiempo)
    {
        int segundos = Mathf.FloorToInt(tiempo);
        int centesimas = Mathf.FloorToInt((tiempo - segundos) * 100);

        return segundos.ToString("00") + ":" + centesimas.ToString("00");
    }

    private void Ganar()
    {
        juegoTerminado = true;

        if (textoMensaje != null)
        {
            textoMensaje.text = "¡Ganaste!\nPresiona R para reiniciar";
        }
    }

    private void Perder()
    {
        juegoTerminado = true;

        if (textoMensaje != null)
        {
            textoMensaje.text = "Perdiste\nPresiona R para reiniciar";
        }
    }

    private void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}