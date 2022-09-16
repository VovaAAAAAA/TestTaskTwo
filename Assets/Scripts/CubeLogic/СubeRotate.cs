using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СubeRotate : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private int _power;
    [SerializeField] private int _powerRotate;
	private Vector3 _diceVelocity;

    private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

    public void RotateCube()
    {
        float dirX = Random.Range(0, _powerRotate);
        float dirY = Random.Range(0, _powerRotate);
        float dirZ = Random.Range(0, _powerRotate);
        _diceVelocity = new Vector3(dirX, dirY, dirZ);
        transform.rotation = Quaternion.identity;
        _rb.AddForce(transform.up * _power);
        _rb.AddTorque(_diceVelocity * 1.5f);
    }

    public EdgeAbility GetNum()
    {
        Transform[] obj = new Transform[6]; // Объявляет, что массив хранит шесть граней кубика
        Transform upobj = transform.GetChild(0); // Объявление роста
        EdgeAbility edgeAbility = null;
        for (int i = 0; i < 6; i++) //, чтобы определить, какая сторона обращена вверх
        {
            obj[i] = transform.GetChild(0).GetChild(i);
            if (obj[i].position.y > upobj.position.y)
            {
                upobj = obj[i];
                if (upobj.GetComponent<EdgeAbility>() != null)
                {
                    edgeAbility = upobj.GetComponent<EdgeAbility>();
                }
            }
        }
        return edgeAbility;
    }
}
