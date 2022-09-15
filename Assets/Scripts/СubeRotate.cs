using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СubeRotate : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private int _power;
    [SerializeField] private int _powerRotate;
    private bool isGrounded;
	private Vector3 _diceVelocity;

    private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			float dirX = Random.Range(0, _powerRotate);
			float dirY = Random.Range(0, _powerRotate);
			float dirZ = Random.Range(0, _powerRotate);
            //transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            _diceVelocity = new Vector3(dirX, dirY, dirZ);
            transform.rotation = Quaternion.identity;
            _rb.AddForce(transform.up * _power);
            _rb.AddTorque(_diceVelocity);
        }

        //if(isGrounded == false)
        //    transform.Rotate(new Vector3(transform.rotation.x + _diceVelocity.x, transform.rotation.y + _diceVelocity.y, transform.rotation.z + _diceVelocity.z));

        if (Input.GetKeyDown(KeyCode.K))
        {
            GetNum();
        }
	}

	private void GetNum()
    {
        Transform[] obj = new Transform[6]; // Объявляет, что массив хранит шесть граней кубика
        Transform upobj = transform.GetChild(0).GetChild(0); // Объявление роста 
        for (int i = 0; i < 6; i++) //, чтобы определить, какая сторона обращена вверх
        {
            obj[i] = transform.GetChild(0).GetChild(i);
            if (obj[i].position.y > upobj.position.y)
            {
                upobj = obj[i];
            }
        }
        Debug.Log(upobj.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Table")
        {
            isGrounded = true;
        }
    }    
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Table")
        {
            isGrounded = false;
        }
    }

}
