using System.Collections.Generic;
using UnityEngine;
public class plano
{

    public Vector2 pos;

    public Vector2 _normal;

    public float Offcet;


    public plano()
    {
        pos = Vector2.zero;
        _normal = new Vector2(0, 1);
        Offcet = 0;
    }

    public plano(Vector2 pos, Vector2 normal, float display)
    {
        this.pos = pos;
        _normal = normal.normalized;
        Vector2 ofcetNegatica = _normal * pos;
        Offcet = display;
    }
}
public class ColideTriangle : MonoBehaviour
{

    private Vector2 posicioActual;
    private Vector2 AngleVector;



    private float CurrentTime;
    public float TotalTime = 10;
    public float deltaTime = 0.01f;


    [Header("ColisionElemento")]
    [SerializeField] private float Epsilon;
    private List<plano> _plano;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        posicioActual = new Vector2(1, 1);
        AngleVector = new Vector2(3, 4);
        
        _plano = new List<plano>();

        plano tempPlano1 = new plano();
        _plano.Add(tempPlano1);
        plano tempPlano2 = new plano(new Vector2(5f, 0), new Vector2(-5, -2.5f), 25);
        _plano.Add(tempPlano2);
        plano tempPlano3 = new plano(new Vector2(2.5f, 5), new Vector2(5, -2.5f), 0);
        _plano.Add(tempPlano3);
    }

    private void CalculoAngularEnElTemps()
    {
        Vector2 oldPos = posicioActual;


        posicioActual = oldPos + AngleVector * deltaTime;

        //Acceleration += (-ResistenciaDelAire / masa);

        CurrentTime += deltaTime;

    }

    private void BouncePlane(plano plano)
    {
        float oldDot = Vector2.Dot((Vector2)transform.position, plano._normal);
        float newDot = Vector2.Dot((Vector2)posicioActual, plano._normal);

        float oldValue = oldDot * plano.Offcet;
        float newValue = newDot + plano.Offcet;

        if (oldValue * newValue < 0)
        {
            float velocutuDot = Vector2.Dot(plano._normal, AngleVector);
            Vector2 reflectionVelocty = AngleVector - (1 + Epsilon) * velocutuDot * plano._normal;

            float positionDot = -newValue;
            Vector2 reflectionPosition = posicioActual + (1 + Epsilon) * positionDot * plano._normal;
            reflectionPosition += 0.01f * plano._normal;

            AngleVector = reflectionVelocty;
            posicioActual = reflectionPosition;
        }

    }
    // Update is called once per frame
    void Update()
    {
        CalculoAngularEnElTemps();


        for (int i = 0; i < _plano.Count; i++)
        {

            BouncePlane(_plano[i]);
        }
        transform.position = posicioActual;

    }
}
