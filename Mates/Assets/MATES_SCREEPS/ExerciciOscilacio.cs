using UnityEngine;
using UnityEngine.UIElements;

public class ExerciciOscilacio : MonoBehaviour
{
    [Header("Particle")]

    private Vector2 posicioActual;
    private Vector2 Velocitat;
    private Vector2 Acceleration;
    public float Massa = 1;


    [Header("dinamic Setup")]
    private Vector2 posicioInicial = new Vector2(2, 0);
    private Vector2 VelocitatInicial = new Vector2(0, 0);
    private float CurrentTime;
    public float TotalTime = 5*Mathf.PI;
    public float deltaTime = 0.001f;
    public float springConstreint = 4;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicioActual = posicioInicial;
        Velocitat = VelocitatInicial;
        Acceleration = CalculateAcceleration(posicioActual);

        transform.position = posicioActual;

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime < TotalTime) {
            RungeKutta4r();

            Acceleration = CalculateAcceleration(posicioActual);


            transform.position = posicioActual;


        }

    }


    private void RungeKutta4r()
    {

        Vector2 K1V, K1P, K2V, K2P, K3V, K3P, K4V, K4P;


       

        K1P = Velocitat;
        K1V = CalculateAcceleration(posicioActual);      //resoltat de K1 dividit en 2 parts velocitat i posicio


        K2P = Velocitat + 0.5f * deltaTime * K1V;
        K2V = CalculateAcceleration(posicioActual + 0.5f*deltaTime*K1P);      //resoltat de K2 dividit en 2 parts velocitat i posicio

        K3P = Velocitat + 0.5f * deltaTime * K2V;

        K3V = CalculateAcceleration(posicioActual + 0.5f * deltaTime * K2P);    //resoltat de K3 dividit en 2 parts velocitat i posicio  

        K4P = Velocitat + deltaTime * K3V;
        K4V = CalculateAcceleration(posicioActual + deltaTime * K3P);  //resoltat de K4 dividit en 2 parts velocitat i posicio



        posicioActual += deltaTime/6* (K1P + 2*K2P +2*K3P+ K4P);
        Velocitat += deltaTime/6 * (K1V + 2 * K2V + 2 * K3V + K4V);

        CurrentTime += deltaTime;
    }
    private Vector2 CalculateAcceleration(Vector2 position)
    {
        Vector2 newAcceleration;


        Vector2 Forçe = new Vector2(0, -springConstreint * posicioActual.y);
        Vector2 Gravity = new Vector2(0, -9.8f);

        newAcceleration = (Forçe + Gravity) / Massa;
        return newAcceleration;


    }


}
