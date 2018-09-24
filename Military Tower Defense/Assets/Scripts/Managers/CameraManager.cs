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
    Vector3 movement;
    Vector3 scrollMovement;
    Vector3 rotateAmount;
    public float movementModifier;
    RaycastHit hitTarget;
	// Use this for initialization
	void Start () {

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
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement *= movementModifier * Time.deltaTime;
        if (Input.GetButton("Shift"))
        {
            movement *= 4;
        }
        movement += transform.position;
        movement.x = Mathf.Clamp(movement.x, maxHorizontalPos.underBorder, maxHorizontalPos.upperBorder);
        movement.z = Mathf.Clamp(movement.z, maxVerticalPos.underBorder, maxVerticalPos.upperBorder);
        movement -= transform.position;
        transform.Translate(movement);
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
