using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform playerTr;
    [SerializeField]
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateAround();
        FollowPlayer();
        //animator.SetFloat( "Input magnitude", inputMagnitude, 0.05f, Time.deltaTime );
        //animator.SetBool( "IsMoving", isMoving );
    }

    void RotateAround()
    {
        //transform.Rot( playerTr.position, Vector3.up, 10.0f * Time.deltaTime);
    }


    void FollowPlayer()
    {

        transform.position = Vector3.Lerp( transform.position, playerTr.position + offset, 10 * Time.deltaTime );

        //transform.position = playerTr.position + offset; 
    }

}
