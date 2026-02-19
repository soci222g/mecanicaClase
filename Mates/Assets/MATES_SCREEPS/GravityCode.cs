using NUnit.Framework.Constraints;
using UnityEngine;

public class GravityCode : MonoBehaviour
{
    [Header("Particle")]
    private Vector2 posicioActual;
    private Vector2 Velocitat;
    private Vector2 Acceleration;


    public float Radi;
    private float GM = 4 * Mathf.Pow(Mathf.PI,2);

    private float CurrentTime;
    public float TotalTime = 100;
    public float deltaTime = 0.001f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicioActual = new Vector2 (1,0);
        Velocitat = new Vector2(0, 2 * Mathf.PI);
            Acceleration = CalculateAcceleration(posicioActual);

        transform.position = posicioActual;


    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTime < TotalTime)
        {
            //RungeKutta4r();
            Acceleration = CalculateAcceleration(posicioActual);

            (posicioActual, Velocitat) = EulerMethod(posicioActual, Velocitat);
            transform.position = posicioActual;

        }



    }


    private Vector2 CalculateAcceleration(Vector2 position)
    {
        Vector2 newAcceleration;

        float distanciaAlCuadrat = posicioActual.magnitude * posicioActual.magnitude;
        Vector2 VectorUnitari = posicioActual.normalized;

        newAcceleration = -(GM / distanciaAlCuadrat) * VectorUnitari;                                                                                                                     //R es la posicio de la terra


   

        return newAcceleration;



    }


    private void RungeKutta4r()
    {

        Vector2 K1V, K1P, K2V, K2P, K3V, K3P, K4V, K4P;




        K1P = Velocitat;
        K1V = CalculateAcceleration(posicioActual);      //resoltat de K1 dividit en 2 parts velocitat i posicio


        K2P = Velocitat + 0.5f * deltaTime * K1V;
        K2V = CalculateAcceleration(posicioActual + 0.5f * deltaTime * K1P);      //resoltat de K2 dividit en 2 parts velocitat i posicio

        K3P = Velocitat + 0.5f * deltaTime * K2V;

        K3V = CalculateAcceleration(posicioActual + 0.5f * deltaTime * K2P);    //resoltat de K3 dividit en 2 parts velocitat i posicio  

        K4P = Velocitat + deltaTime * K3V;
        K4V = CalculateAcceleration(posicioActual + deltaTime * K3P);  //resoltat de K4 dividit en 2 parts velocitat i posicio



        posicioActual += deltaTime / 6 * (K1P + 2 * K2P + 2 * K3P + K4P);
        Velocitat += deltaTime / 6 * (K1V + 2 * K2V + 2 * K3V + K4V);

        CurrentTime += deltaTime;
    }
    private (Vector2, Vector2) EulerMethod(Vector2 positon, Vector2 Velocity)
    {
        Vector2 newPosition = positon + Velocity * deltaTime;
        Vector2 newVelocity = Velocity + Acceleration * deltaTime;
        CurrentTime += deltaTime;

        return (newPosition, newVelocity);
    }
}
