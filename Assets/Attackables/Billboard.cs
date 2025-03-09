using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
#nullable enable
public class Billboard : MonoBehaviour
{

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }


    private void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }

}
