using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    Vector2 mouseLook;
    Vector2 smoothness;

    public float sensibility = 5.0f;
    public float smooth = 2.0f;



    void Start()
    {
        //transform.SetParent(player.transform);
        //transform.position = player.transform.position;
    }

    void LateUpdate()
    {
        if (!PauseManager.pauseOn && transform.parent!=null)
        {
             var mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

             mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensibility * smooth, sensibility * smooth));

             smoothness.x = Mathf.Lerp(smoothness.x, mouseDirection.x, 1f / smooth);
             smoothness.y = Mathf.Lerp(smoothness.y, mouseDirection.y, 1f / smooth);
             mouseLook += smoothness;

             mouseLook.y = Mathf.Clamp(mouseLook.y, -90.0f, 90.0f);

             transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
             transform.parent.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.parent.transform.up);
        }
    }
}
