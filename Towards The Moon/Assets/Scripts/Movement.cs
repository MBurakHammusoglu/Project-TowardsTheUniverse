using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float upSpeedValue=100;
    [SerializeField] float rotationSpeedValue=100;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    
    }
    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            //Debug.Log("Pressed Space");
            rb.AddRelativeForce(Vector3.up * upSpeedValue * Time.deltaTime);
            if(!audioSource.isPlaying){
                audioSource.Play();
            }
        }else{
            audioSource.Stop();
        }
    }
    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeedValue);
        }
        else if(Input.GetKey(KeyCode.D)){
            //Debug.Log("Rotating right");
            ApplyRotation(-rotationSpeedValue);
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=false;
    }
}
