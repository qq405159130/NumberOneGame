using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	private Transform m_trans;
	// Use this for initialization
	void Start () {
		m_trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Control ();
	}
	private float m_moveSpeed = 3f;

	private Vector3 m_cameraInitPos;
	private Vector3 m_mouse2InitPos;
	private bool m_isMouse2Move;
    void Control()
    {
		float x = 0,z = 0;
		x = Input.GetAxis("Horizontal");
		z = Input.GetAxis ("Vertical");

		m_trans.Translate(new Vector3(x,0,z) * m_moveSpeed);

		return;
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
			m_trans.position = m_cameraInitPos + offset;

		}
    }
}
