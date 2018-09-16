using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public ScrollPositions maxHorizontalPos;
    public ScrollPositions maxVerticalPos;
    public int maxScrollWidth;
    public float keepScrollModifier;
    public float zoomSpeed;
    ScrollPositions up;
    ScrollPositions down;
    ScrollPositions right;
    ScrollPositions left;
    Vector3 movement;
    Vector3 scrollMovement;
    public float minSpeed;
    public float maxSpeed;
    float movementModifier;
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
        movement = Vector3.zero;
        //up
        print(Input.mousePosition);
		if(Input.mousePosition.y < up.upperBorder && Input.mousePosition.y > up.underBorder)
        {
            movement.z = 1;
        }
        //down
        if (Input.mousePosition.y < down.upperBorder && Input.mousePosition.y > down.underBorder)
        {
            movement.z = -1;
        }
        //left
        if (Input.mousePosition.x < left.upperBorder && Input.mousePosition.x > left.underBorder)
        {
            movement.x = -1;
        }
        //right
        if (Input.mousePosition.x > right.upperBorder && Input.mousePosition.y < right.underBorder)
        {
            movement.x = 1;
        }
        if(movement != Vector3.zero)
        {
            movementModifier += keepScrollModifier;
            movementModifier = Mathf.Clamp(movementModifier, minSpeed, maxSpeed);
        }
        else
        {
            movementModifier = minSpeed;
        }
        transform.Translate(movement * movementModifier * Time.deltaTime);
        scrollMovement.y = -Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(scrollMovement * zoomSpeed * Time.deltaTime);
    }
    [System.Serializable]
    public struct ScrollPositions
    {
        public int upperBorder;
        public int underBorder;
    }
}
