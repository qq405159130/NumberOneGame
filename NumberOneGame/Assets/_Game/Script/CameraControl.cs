using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	private Transform m_trans;
	// Use this for initialization
	void Start () {
		m_trans = transform;
        m_camXMin  += transform.position.x;
        m_camXMax  += transform.position.x;
        m_camZMin  += transform.position.z;
        m_camZMax  += transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        Control ();
	}
	private float m_moveSpeed = 3f;

	private Vector3 m_cameraInitPos;
	private Vector3 m_mouse2InitPos;
	private bool m_isMouse2Move;


    private int MouseWheelSensitivity = 10;
    private int maxCamFov = 20;
    private int minCamFov = 5;

    private float m_camXMin = -10;
    private float m_camXMax = 10;
    private float m_camZMin = -5;
    private float m_camZMax = 5;

    void Control()
    {
        //float x = 0,z = 0;
        //x = Input.GetAxis("Horizontal");
        //z = Input.GetAxis ("Vertical");
        //m_trans.Translate(new Vector3(x,0,z) * m_moveSpeed);

		if (Input.GetMouseButtonDown (2)) {
			if (!m_isMouse2Move) {
				m_isMouse2Move = true;
				m_mouse2InitPos = Input.mousePosition;
				m_cameraInitPos = m_trans.position;
			}
		} else if (Input.GetMouseButtonUp (2)) {
			m_isMouse2Move = false;
		}
		if (Input.GetMouseButton (2)) {
			Vector3 offset = Input.mousePosition - m_mouse2InitPos;
            Vector3 pos =m_cameraInitPos - new Vector3(offset.x, 0 ,offset.y) / 100;

		    pos = new Vector3(Mathf.Clamp(pos.x, m_camXMin, m_camXMax), pos.y, Mathf.Clamp(pos.z, m_camZMin, m_camZMax));

		    m_trans.position = pos;
		}

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * MouseWheelSensitivity;
            fov = Mathf.Clamp(fov, minCamFov, maxCamFov);
            Camera.main.fieldOfView = fov;
        }
    }


    private static int maxX = 1334 / 100;
    private static int maxZ = 750 / 100;
    private int minX;
    private int minZ;

    private void initPosition()
    {
        minX = -Screen.width / 2 / 100;
        minZ = -Screen.height / 2 / 100;
        Vector3 position = new Vector3(minX, transform.position.y, minZ);
        transform.position = position;
    }
    private void moveView()
    {
        //镜头向左
        if (Input.mousePosition.x < 20 && transform.position.x > minX)
        {
            transform.Translate(Vector3.left * 10 * Time.deltaTime);
        }
        //镜头向右
        if (Input.mousePosition.x > Screen.width - 20 && transform.position.x < maxX)
        {
            transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }
        //镜头向下
        if (Input.mousePosition.y < 20 && transform.position.z > minZ)
        {
            transform.Translate(Vector3.back * 10 * Time.deltaTime);
        }
        //镜头向上
        if (Input.mousePosition.y > Screen.height - 20 && transform.position.z < maxZ)
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }
    }
}
