using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour 
{

    public GameObject explosion;
    public Transform explosion_spawnpoint;
    public Material m1;
    public AudioClip clipp;
    public AudioSource _audSource;
    public float MoveSpeed = 10;
    public float MaxSpeed = 20;
    public float Drag = 0.98f;
    public float SteerAngle = 20;
    public float Traction = 1;
    public GameObject spawnPoint;
    public int coin = 0;
    public GameObject player;
    public float nitro = 0f;
    public bool nitroCheck = false;
    private Vector3 MoveForce;
    public float nitro1 = 15;
    public float nitro2 = 12;
    public float  standard = 10;
    




    void Turbo()
    {
        switch (coin)
        {
        case 1:
            MoveSpeed = 11;
            break;
        case 2:
            MoveSpeed = 12;
            break;
            
        case 3:
            MoveSpeed = 13;
            break;
        }
    }






    // Update is called once per frame
     void Update() 
     {
        Turbo();

    


        var nitroState = 0;
        if(nitro > 5)
        {
            nitroState = 1;
        }

        if(nitro < 5)
        {
            nitroState = 2;
        }

        


        if(nitro == 0)
        {
            nitroCheck = false;
            nitro = 0f;
            nitroState = 0;
        }
        
        switch (nitro)
        {
        case 1:
            MoveSpeed = nitro1;
            break;
        case 2:
            MoveSpeed = nitro2;
            break;
            
        case 0:
            MoveSpeed = standard;
            break;
        }

        if (nitroCheck == true)
        {
            Timer();
        }

        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    }


    void ponerplaysonido(AudioClip clip)
    {
        _audSource.clip = clip;
        _audSource.Play();
        
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.name == "LightSaber")
        {
            GameObject exp = Instantiate(explosion, explosion_spawnpoint.position, explosion_spawnpoint.rotation);
            Destroy(exp,1f);
            player.GetComponent<Renderer>().material.SetColor("_Color",Color.black);
            ponerplaysonido(clipp);
            
            
            
            
        }

        if (col.transform.gameObject.tag == "Coin")
        {
           nitro = 10f;
           nitroCheck = true;
           
           coin++;
           Destroy(col.transform.gameObject);
        }


    }




    void OnTriggerExit(Collider col)
    {
        
        if (col.transform.gameObject.name == "LightSaber")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


     }



     void Timer()
     {

        if(nitro >= 0)
        {
         nitro -= Time.deltaTime;
        }

     }










    
 }
