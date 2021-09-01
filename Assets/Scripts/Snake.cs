using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public GameObject go;

    public bool isDebug = true;

    private void Update()
    {
        if (isDebug)
        {
            MouseBehaviour();
        }
        else { TouchBehaviour(); };
    }

    public void MouseBehaviour()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Mouse.current.position.ReadValue().x < Screen.width / 2)
            {
                Debug.Log("Sinistra");
                go.GetComponent<Text>().text = "Sinistra";
            }
            else if (Mouse.current.position.ReadValue().x > Screen.width / 2)
            {
                Debug.Log("Destra");
                go.GetComponent<Text>().text = "Destra";
            }
        }
    }
    private void TouchBehaviour()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                Debug.Log("Sinistra");
                go.GetComponent<Text>().text = "Sinistra";
            }
            else if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                Debug.Log("Destra");
                go.GetComponent<Text>().text = "Destra";
            }
        }
    }

}
