using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public ScrollPositions maxHorizontalPos;
    public ScrollPositions maxVerticalPos;
    public ScrollPositions scrollLimits;
    public int maxScrollWidth;
    public float keepScrollModifier;
    public float cameraRotateModifier;
    public float zoomSpeed;
    public bool canMove = true;
    ScrollPositions up, down, right, left;
    Vector3 movement;
    Vector3 scrollMovement;
    Vector3 rotateAmount;
    float rotateSpeed;
    public float minSpeed;
    public float maxSpeed;
    float movementModifier;
    RaycastHit hitTarget;
	// Use this for initialization
	void Start () {
        up.upperBorder = Screen.height;
        up.underBorder = Screen.height - maxScrollWidth;
        down.upperBorder = maxScrollWidth;
        down.underBorder = 0;
        left.upperBorder = maxScrollWidth;
        left.underBorder = 0;
        right.upperBorder = Screen.width - maxScrollWidth;
        right.underBorder = Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hitTarget))
            {
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().ShowStats(hitTarget.transform.gameObject);
            }
        }
        if (canMove)
        {
            CameraMovement();
        }
        CameraScrolling();
        CameraRotation();
    }
    [System.Serializable]
    public struct ScrollPositions
    {
        public int upperBorder;
        public int underBorder;
    }
    public void CameraMovement()
    {
        movement = Vector3.zero;
        //up
        if (Input.mousePosition.y < up.upperBorder && Input.mousePosition.y > up.underBorder || Input.GetButton("Up"))
        {
            movement.z = 1;
        }
        //down
        if (Input.mousePosition.y < down.upperBorder && Input.mousePosition.y > down.underBorder || Input.GetButton("Down"))
        {
            if (movement.z == 1)
            {
                movement.z = 0;
            }
            else
            {
                movement.z = -1;
            }
        }
        //left
        if (Input.mousePosition.x < left.upperBorder && Input.mousePosition.x > left.underBorder || Input.GetButton("Left"))
        {
            movement.x = -1;
        }
        //right
        if (Input.mousePosition.x > right.upperBorder && Input.mousePosition.y < right.underBorder || Input.GetButton("Right"))
        {
            if (movement.x == -1)
            {
                movement.x = 0;
            }
            else
            {
                movement.x = 1;
            }
        }
        if (movement != Vector3.zero)
        {
            movementModifier += keepScrollModifier;
            movementModifier = Mathf.Clamp(movementModifier, minSpeed, maxSpeed);
        }
        else
        {
            movementModifier = minSpeed;
        }
        transform.Translate(movement * movementModifier * Time.deltaTime);
    }
    public void CameraScrolling()
    {
        float newPos = new float();
        scrollMovement.y = -Input.GetAxis("Mouse ScrollWheel");
        newPos = Mathf.Clamp(newPos, scrollLimits.upperBorder, scrollLimits.underBorder);
        scrollMovement.y = newPos;
        transform.position = scrollMovement * zoomSpeed * Time.deltaTime;
        transform.Translate(scrollMovement * zoomSpeed * Time.deltaTime);
    }
    public void CameraRotation()
    {
        if(Input.GetButton("Fire3"))
        {
            canMove = false;
            Cursor.lockState = CursorLockMode.Locked;
            rotateAmount.y = Input.GetAxis("Mouse X") * cameraRotateModifier * Time.deltaTime;
            transform.Rotate(rotateAmount);
        }
        if (Input.GetButtonUp("Fire3"))
        {
            canMove = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
