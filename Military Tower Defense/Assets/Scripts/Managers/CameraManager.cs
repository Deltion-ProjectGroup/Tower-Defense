using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public ScrollPositions maxHorizontalPos;
    public ScrollPositions maxVerticalPos;
    public ScrollPositions scrollLimits;
    public int moveBorderWidth;
    public float keepScrollModifier;
    public float cameraRotateModifier;
    public float zoomSpeed;
    public bool canMove = true;
    ScrollPositions up, down, right, left;
    Vector3 movement;
    Vector3 scrollMovement;
    Vector3 rotateAmount;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    float movementModifier;
    RaycastHit hitTarget;
	// Use this for initialization
	void Start () {
        up.upperBorder = Screen.height;
        up.underBorder = Screen.height - moveBorderWidth;
        down.upperBorder = moveBorderWidth;
        down.underBorder = 0;
        left.upperBorder = moveBorderWidth;
        left.underBorder = 0;
        right.upperBorder = Screen.width - moveBorderWidth;
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
            movement += transform.forward;
        }
        //down
        if (Input.mousePosition.y < down.upperBorder && Input.mousePosition.y > down.underBorder || Input.GetButton("Down"))
        {
            movement += -transform.forward;
        }
        //left
        if (Input.mousePosition.x < left.upperBorder && Input.mousePosition.x > left.underBorder || Input.GetButton("Left"))
        {
            movement += -transform.right;
        }
        //right
        if (Input.mousePosition.x > right.upperBorder && Input.mousePosition.y < right.underBorder || Input.GetButton("Right"))
        {
            movement += transform.right;
        }
        if (movement != Vector3.zero)
        {
            movementModifier += keepScrollModifier;
            movementModifier = Mathf.Clamp(movementModifier, minMoveSpeed, maxMoveSpeed);
        }
        else
        {
            movementModifier = minMoveSpeed;
        }
        movement *= movementModifier * Time.deltaTime;
        if (Input.GetButton("Shift"))
        {
            movement *= 4;
        }
        movement += transform.position;
        movement.x = Mathf.Clamp(movement.x, maxHorizontalPos.underBorder, maxHorizontalPos.upperBorder);
        movement.z = Mathf.Clamp(movement.z, maxVerticalPos.underBorder, maxVerticalPos.upperBorder);
        transform.position = movement;
    }
    public void CameraScrolling()
    {
        scrollMovement = transform.position;
        scrollMovement.y += -Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        scrollMovement.y = Mathf.Clamp(scrollMovement.y, scrollLimits.upperBorder, scrollLimits.underBorder);
        transform.position = scrollMovement;
    }
    public void CameraRotation()
    {
        if(Input.GetButton("Fire3"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotateAmount.y = Input.GetAxis("Mouse X") * cameraRotateModifier * Time.deltaTime;
            transform.Rotate(rotateAmount);
        }
        if (Input.GetButtonUp("Fire3"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
