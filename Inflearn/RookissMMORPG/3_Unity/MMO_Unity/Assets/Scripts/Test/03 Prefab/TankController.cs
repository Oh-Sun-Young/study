using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tank
{
    // 온갖 정보
    public float speed = 10.0f;
    TankPlayer player; // Nested Prefab(포함)
}

// Prefab Variant (상속)
class FastTank : Tank
{

}

class TankPlayer
{

}

public class TankController : MonoBehaviour
{
    [SerializeField]
    float _speed;

    void Start()
    {
        Managers.input.KeyAction -= OnKeyboard; // 중복 체크 대비용
        Managers.input.KeyAction += OnKeyboard;

        Tank tank1 = new Tank(); // instance를 만든다
    }

    private void OnDestroy()
    {
        Managers.input.KeyAction -= OnKeyboard;
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
    }
}
