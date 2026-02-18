using UnityEngine;

public class TiroParabolico : MonoBehaviour
{
    [Header("Particle")]

    private Vector2 posicioActual;
    private Vector2 AngleVector;
    private Vector2 Acceleration;


    [Header("dinamic Setup")]
    public float angle;
    private float CurrentTime;
    public float TotalTime = 10;
    public float deltaTime = 0.01f;
    private float G = 9.8f;
    private float VelicotatInicial;
    public Vector2 ResistenciaDelAire;
    public float masa;

    public float Friccio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VelicotatInicial = 10;
        angle = 60;
        AngleVector = AngleInVector(angle);

       // Acceleration = new Vector2(0, -G);
        posicioActual = new Vector2(0,0);
        calculaFriccioEspai();
    }

    private Vector2 AngleInVector(float angle)
    {                                               // pasem de grau a redian perque la convarsio sigi posible
        float x = VelicotatInicial * Mathf.Cos(angle * Mathf.PI / 180);
        float y = VelicotatInicial * Mathf.Sin(angle * Mathf.PI / 180);

        return new Vector2(x, y);
    }




    private void CalculoAngularEnElTemps()
    {
        Vector2 oldPos = posicioActual;

         posicioActual = new Vector2(oldPos.x + deltaTime * AngleVector.x, oldPos.y + deltaTime * AngleVector.y);

         //Acceleration += (-ResistenciaDelAire / masa);
            
  

         AngleVector += (deltaTime * Acceleration);


        CurrentTime += deltaTime;

    }
    private (Vector2, Vector2, float) EulerMethod(Vector2 positon, Vector2 Velocity, float time)
    {
        Vector2 newPosition = positon + Velocity * deltaTime;
        Vector2 newVelocity = Velocity + Acceleration * deltaTime;
        CurrentTime += deltaTime;

        return (newPosition, newVelocity, CurrentTime);
    }

    void calculaFriccioEspai()
    {
        Acceleration.x = -Friccio * AngleVector.x;
        Acceleration.y = -G * -Friccio * AngleVector.y;
    }

    private void Update()
    {
        if (CurrentTime < TotalTime)
        {
            CalculoAngularEnElTemps();
            calculaFriccioEspai();
            //(posicioActual, AngleVector, CurrentTime) = EulerMethod(posicioActual, AngleVector, CurrentTime);
            transform.position = posicioActual;
        }
    }


}
