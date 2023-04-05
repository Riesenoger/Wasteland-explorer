using UnityEngine;
using UnityEngine.InputSystem;

public class floatingvehicle : MonoBehaviour
{
    // [SerializeField] private float speed = 10.0f;
    // [SerializeField] private float rotationSpeed = 100.0f;
    // [SerializeField] private float hoverForce = 12.0f;
    // [SerializeField] private float hoverHeight = 3.5f;

    private Rigidbody rb;
    private float movementInputValue;
    private float rotationInputValue;
    private float thrustInputValue;


    public float speed = 10.0f; //Geschwindigkeit des Fahrzeugs
    public float tiltAngle = 20.0f; //Winkel des Neigens des Fahrzeugs
    public float hoverForce = 10.0f; //Schwebe-Kraft
    public float hoverHeight = 3.5f; //Schwebehöhe
    public GameObject[] hoverPoints; //Array von Objekten, an denen das Fahrzeug schweben soll


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // // Get input values using the new input system
        // movementInputValue = InputSystem.GetAxis("Vertical");
        // rotationInputValue = InputSystem.GetAxis("Horizontal");
        // thrustInputValue = InputSystem.GetAxis("Thrust");
    }

    private void FixedUpdate()
    {
        // // Move the car forward and backward
        // Vector3 movement = transform.forward * movementInputValue * speed * Time.fixedDeltaTime;
        // rb.AddForce(movement, ForceMode.VelocityChange);

        // // Rotate the car left and right
        // float rotation = rotationInputValue * rotationSpeed * Time.fixedDeltaTime;
        // Quaternion rotationDelta = Quaternion.Euler(0.0f, rotation, 0.0f);
        // rb.MoveRotation(rb.rotation * rotationDelta);

        // Apply hover force to the car
        // RaycastHit hit;
        // for (int i = 0; i < 4; i++)
        // {
        //     Vector3 hoverPoint = transform.TransformPoint(Vector3.up * hoverHeight);
        //     if (Physics.Raycast(hoverPoint, Vector3.down, out hit, hoverHeight))
        //     {
        //         float distance = hoverHeight - hit.distance;
        //         // Debug.Log(distance);
        //         float force = distance * hoverForce;
        //         rb.AddForceAtPosition(force * Vector3.up, hit.point, ForceMode.Force);
        //     }
        // }

        foreach (GameObject hoverPoint in hoverPoints) //Iteriere über alle Objekte, an denen das Fahrzeug schweben soll
        {
            Ray ray = new Ray(hoverPoint.transform.position, -hoverPoint.transform.up); //Erstelle einen Strahl von oben nach unten
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, hoverHeight)) //Überprüfe, ob der Strahl ein Objekt trifft
            {
                float distance = hoverHeight - hit.distance; //Berechne den Abstand zum Boden
                Vector3 hoverForce = Vector3.up * distance * this.hoverForce; //Berechne die Schwebekraft
                rb.AddForceAtPosition(hoverForce, hoverPoint.transform.position); //Füge dem Fahrzeug eine Kraft an der Position des Objekts hinzu
            }
        }

        // // Apply thrust to the car
        // if (thrustInputValue > 0.0f)
        // {
        //     rb.AddRelativeForce(Vector3.forward * thrustInputValue * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        // }
    }
}
