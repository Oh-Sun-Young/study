using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0,15.0f,-5.0f);

    [SerializeField]
    GameObject _player = null;

    MeshRenderer mr;

    void LateUpdate()
    {
        if(_mode == Define.CameraMode.QuarterView)
        {
            if(mr != null)
            {
                mr.enabled = true;
                mr = null;
            }

            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Block")))
            {
                float dist = (hit.point - _player.transform.position + Vector3.up * 5.5f).magnitude * 0.85f;
                transform.position = _player.transform.position + _delta.normalized * dist;
                mr = hit.transform.gameObject.GetComponent<MeshRenderer>();
                mr.enabled = false;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);

            }
        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }
}
