using UnityEngine;

public class plano
{
   
   public Vector2 pos;

   public Vector2 _normal;

   public Vector2 Offcet;


    public plano()
    {
        pos = Vector2.zero;
        _normal = new Vector2(0,1);
        Offcet = Vector2.zero;
    }

    public plano(Vector2 pos, Vector2 normal, Vector2 offcet   )
    {
        this.pos = pos;
        _normal = normal;
        Offcet = offcet;
    }
}


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
    [SerializeField] private float VelicotatInicial;
    public Vector2 ResistenciaDelAire;
    public float masa;

    public float Friccio;




    [Header("ColisionElemento")]
    [SerializeField] private float Elasticitat;
     private plano _plano;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AngleVector = AngleInVector(angle);

       // Acceleration = new Vector2(0, -G);
        posicioActual = new Vector2(0,0);
        calculaFriccioEspai();

        _plano = new plano();
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
        Acceleration.y = -G  - Friccio * AngleVector.y;
    }


 

    private void BouncePlane(plano plano)
    {
        float oldDot = Vector2.Dot(transform.position, plano._normal);
        float newDot = Vector2.Dot(posicioActual, plano._normal);


        if (oldDot * newDot < 0) {
            float velocutuDot = Vector2.Dot(AngleVector, plano._normal);
            Vector2 reflectionVelocty = AngleVector - (1 + Elasticitat) * velocutuDot * _plano._normal;

            float positionDot = Vector2.Dot(posicioActual, plano._normal);
            Vector2 reflectionPosition = posicioActual - (1 + Elasticitat) * positionDot * _plano._normal;
            reflectionPosition += 0.01f * _plano._normal;

            AngleVector = reflectionVelocty;
            posicioActual = reflectionPosition;
        }

    }

    private void Update()
    {
        if (CurrentTime < TotalTime)
        {
            CalculoAngularEnElTemps();
            calculaFriccioEspai();
            //(posicioActual, AngleVector, CurrentTime) = EulerMethod(posicioActual, AngleVector, CurrentTime);

            BouncePlane(_plano);

            transform.position = posicioActual;
        }
    }


}
