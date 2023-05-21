using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float upSpeedValue=100;
    [SerializeField] float rotationSpeedValue=100;
    [SerializeField] AudioClip voiceEngine;

    [SerializeField] ParticleSystem rocketJetParticles;
    [SerializeField] ParticleSystem rightSideJetParticles;
    [SerializeField] ParticleSystem leftSideJetParticles;
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
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

   

    private void StartThrusting()
    {
        //Debug.Log("Pressed Space");
        rb.AddRelativeForce(Vector3.up * upSpeedValue * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(voiceEngine);
        }
        if (!rocketJetParticles.isPlaying) 
        { rocketJetParticles.Play(); }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        rocketJetParticles.Stop();
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }else
        {
            StopRotate();
        }

    }

   
     private void RotateLeft()
    {
        ApplyRotation(rotationSpeedValue);
        if (!leftSideJetParticles.isPlaying)
        {
            leftSideJetParticles.Play();
            rightSideJetParticles.Stop();
        }
    }

    private void RotateRight()
    {
        //Debug.Log("Rotating right");
        ApplyRotation(-rotationSpeedValue);
        if (!rightSideJetParticles.isPlaying)
        {
            rightSideJetParticles.Play();
            leftSideJetParticles.Stop();
        }
    }
     private void StopRotate()
    {
        rightSideJetParticles.Stop();
        leftSideJetParticles.Stop();
    }

   

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation=false;
    }
}
