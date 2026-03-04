using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    public plano(Vector2 pos, Vector2 normal)
    {
        this.pos = pos;
        _normal = normal;
        Vector2 ofcetNegatica = _normal * pos;
        Offcet = -1 * ofcetNegatica;
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
     private List<plano> _plano;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AngleVector = AngleInVector(angle);

       // Acceleration = new Vector2(0, -G);
        posicioActual = new Vector2(0,0);
        calculaFriccioEspai();
        _plano = new List<plano>();

        plano tempPlano1 = new plano();
        _plano.Add(tempPlano1);
        plano tempPlano2 = new plano(new Vector2(5,0), new Vector2(-1,-1));
        _plano.Add(tempPlano2);
        plano tempPlano3 = new plano(new Vector2(2.5f, 5), new Vector2(1, -1));
        _plano.Add(tempPlano3);
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
        float oldDot = Vector2.Dot((Vector2)transform.position - plano.Offcet, plano._normal);
        float newDot = Vector2.Dot((Vector2)posicioActual - plano.Offcet, plano._normal);


        if (oldDot * newDot < 0) {
            float velocutuDot = Vector2.Dot(AngleVector, plano._normal);
            Vector2 reflectionVelocty = AngleVector - (1 + Elasticitat) * velocutuDot * plano._normal;

            float positionDot = Vector2.Dot(posicioActual - plano.Offcet, plano._normal);
            Vector2 reflectionPosition = posicioActual - (1 + Elasticitat) * positionDot * plano._normal;
            reflectionPosition += 0.01f * plano._normal;

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

            for (int i = 0; i < _plano.Count; i++)
            {

                BouncePlane(_plano[i]);
            }

            transform.position = posicioActual;
        }
    }


}
