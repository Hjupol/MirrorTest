using System.Collections;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkTransform))]
public class PlayerController : NetworkBehaviour
{
    private Rigidbody rb;
    public int speed;
    private int initialSpeed;
    public float jumpForce;
    private float initialJumpForce;
    private CapsuleCollider ballCollider;
    public LayerMask floorLayer;
    private bool onFloor;

    public static bool projectileOn;
    private Camera weapon;
    public GameObject projectile;

    public static string nameObject;
    //public int powerUpMultiplier;
    //public int powerUpDuration;

    private IEnumerator corroutine;
    private Color initialColor;


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        Camera.main.orthographic = false;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0f, 3f, -8f);
        Camera.main.transform.localEulerAngles = new Vector3(10f, 0f, 0f);
        weapon = Camera.main;
    }

    void OnDisable()
    {
        if (isLocalPlayer && Camera.main != null)
        {
            Camera.main.orthographic = true;
            Camera.main.transform.SetParent(null);
            Camera.main.transform.localPosition = new Vector3(0f, 70f, 0f);
            Camera.main.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballCollider = GetComponent<CapsuleCollider>();
        initialJumpForce = jumpForce;
        initialSpeed = speed;
        //initialColor = rb.gameObject.GetComponent<Renderer>().material.GetColor("Color_8AD3BAA6");
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        isOnFloor();
        if (onFloor && Input.GetKeyDown(KeyCode.Space) && !PauseManager.pauseOn)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        if (Input.GetMouseButtonDown(0) && !PauseManager.pauseOn)
        {
            Ray ray = weapon.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (projectileOn)
            {
                GameObject pro;
                pro = Instantiate(projectile, ray.origin, Quaternion.identity);
                Rigidbody rb = pro.GetComponent<Rigidbody>();
                rb.AddForce(weapon.transform.forward * 20, ForceMode.Impulse);
                pro.GetComponent<ProjectileController>().ExcecuteDisapear(3);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) != false)
                {
                    if (hit.distance < 6)
                    {
                        nameObject = hit.collider.name;
                    }
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (!PauseManager.pauseOn)
        {
            
             rb.freezeRotation = true;
             float frontBackwardsMovement = Input.GetAxis("Vertical");
             float leftRightMovement = Input.GetAxis("Horizontal");

             frontBackwardsMovement *= speed;
             leftRightMovement *= speed;

             frontBackwardsMovement *= Time.deltaTime;
             leftRightMovement *= Time.deltaTime;

             transform.Translate(leftRightMovement, 0, frontBackwardsMovement);
            
        }
    }

    private void isOnFloor()
    {
        Vector3 vector = new Vector3(ballCollider.bounds.center.x, ballCollider.bounds.min.y, ballCollider.bounds.center.z);
        onFloor = Physics.CheckCapsule(ballCollider.bounds.center, vector, ballCollider.radius * .9f, floorLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Velocity"))
        //{
        //    speed = speed * powerUpMultiplier;
        //    rb.gameObject.GetComponent<Renderer>().material.SetColor("Color_8AD3BAA6", Color.green);
        //    StartCoroutine("Wait", powerUpDuration);
        //}
        //if (collision.gameObject.CompareTag("Jump"))
        //{
        //    jumpForce = jumpForce * powerUpMultiplier;
        //    rb.gameObject.GetComponent<Renderer>().material.SetColor("Color_8AD3BAA6", Color.yellow);
        //    StartCoroutine("Wait", powerUpDuration);
        //}
        //if (collision.gameObject.CompareTag("Levitation"))
        //{
        //    rb.useGravity = false;
        //    rb.gameObject.GetComponent<Renderer>().material.SetColor("Color_8AD3BAA6", Color.cyan);
        //    StartCoroutine(Wait(powerUpDuration));
        //}
    }

    //IEnumerator Wait(int duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    if (rb.useGravity == false)
    //    {
    //        rb.useGravity = true;
    //    }
    //    else
    //    if (speed > initialSpeed)
    //    {
    //        speed = speed / powerUpMultiplier;
    //    }
    //    else
    //    if (jumpForce > initialJumpForce)
    //    {
    //        jumpForce = jumpForce / powerUpMultiplier;
    //    }
    //    rb.gameObject.GetComponent<Renderer>().material.SetColor("Color_8AD3BAA6", initialColor);
    //    Debug.Log("Corroutine worked");
    //}
}

