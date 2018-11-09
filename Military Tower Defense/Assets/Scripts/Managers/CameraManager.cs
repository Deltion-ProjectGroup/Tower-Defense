using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    ScrollPositions maxHorizontalPos;
    ScrollPositions maxVerticalPos;
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
    public LayerMask targettableForInfo;
    public GameObject terrain;
	// Use this for initialization
	void Awake () {
        //EventManager.OnLeftClick += ShowStats;
        ShowStats();
        Vector3 maxBound;
        Vector3 minBound;
        maxBound = terrain.GetComponent<MeshCollider>().bounds.max; //105 thinge
        minBound = terrain.GetComponent<MeshCollider>().bounds.min;
        maxHorizontalPos.upperBorder = (int)maxBound.x;
        maxVerticalPos.upperBorder = (int)maxBound.z;
        maxHorizontalPos.underBorder = (int)minBound.x;
        maxVerticalPos.underBorder = (int)minBound.z;

    }

    // Update is called once per frame
    void Update () {
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
        if(Time.timeScale < 1)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
        }
        movement *= movementModifier;
        if (Input.GetButton("Shift"))
        {
            movement *= 4;
        }
        transform.Translate(movement * Time.unscaledDeltaTime);
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(newPos.x, maxHorizontalPos.underBorder, maxHorizontalPos.upperBorder);
        newPos.z = Mathf.Clamp(newPos.z, maxVerticalPos.underBorder, maxVerticalPos.upperBorder);
        transform.position = newPos;
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
            rotateAmount.y = Input.GetAxis("Mouse X") * cameraRotateModifier;
            transform.Rotate(rotateAmount * Time.unscaledDeltaTime);
        }
        if (Input.GetButtonUp("Fire3"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ShowStats()
    {
        if (Physics.Raycast(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hitTarget, 100, targettableForInfo, QueryTriggerInteraction.Ignore))
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().ShowStats(hitTarget.transform.gameObject));
        }
        else
        {
            StartCoroutine(GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().ShowStats(null));
        }
    }
}
