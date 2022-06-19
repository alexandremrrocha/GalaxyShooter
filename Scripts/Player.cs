using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosaoPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tiroTriploPrefab;
    
    [SerializeField]
    private GameObject _shieldGameObject;
    
    [SerializeField]
    private float _fireRate = 0.25f;
    
    [SerializeField]
    private float _speed = 5.0f;

    private float _nextFire = 0.0f;

    public bool tiroTriplo = false;

    public bool speedBoost = false;

    public int vidas = 3;

    public bool shield = false;
     
    // Start is called before the first frame update
    private void Start()
    {
        transform.position = new Vector3(0,0,0);
        //Debug.Log(vidas);
    }

    // Update is called once per frame
    private void Update()
    {
        movimento();
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){ 
            atirar();
        }
        if(vidas == 0){
            Instantiate(_explosaoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void movimento(){
        if(speedBoost == true){ //Speed boost faz a nave se movimentar no dobro da velocidade
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * _speed * 2.0f * horizontalInput * Time.deltaTime );
            //Movimentar para cima e para baixo
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * _speed * 2.0f * verticalInput * Time.deltaTime, Space.World );
        } else{
            //Movimentar para direita e esquerda
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * _speed *horizontalInput * Time.deltaTime );
            //Movimentar para cima e para baixo
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * _speed *verticalInput * Time.deltaTime, Space.World );
        }

        //Restrição do movimento para cima e baixo
        if(transform.position.y > 0 ){ //eu poderia ter restringido até o começo da tela que seria 4.15f
            transform.position = new Vector3 (transform.position.x,0,0); 
        } else if(transform.position.y < -4.17f) {
            transform.position = new Vector3(transform.position.x,-4.17f,0);
        }

        //Sistema de movimentação Wrapping
        if(transform.position.x > 9.5f ){ 
            transform.position = new Vector3 (-9.5f,transform.position.y,0); 
        } else if(transform.position.x < -9.5f) {
            transform.position = new Vector3(9.5f,transform.position.y,0);
        }
    }

    private void atirar(){
        //Aparecer um laser conforme tecla pressionada
            if(tiroTriplo == true && Time.time > _nextFire){
                Instantiate(_tiroTriploPrefab, transform.position, Quaternion.identity);
                 _nextFire = Time.time + _fireRate;
            }
            else if(Time.time > _nextFire){ //Cooldown
                 Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.93f, 0) , Quaternion.identity);
                 _nextFire = Time.time + _fireRate;
            }
    }

    public void tiroTriploAtivar(){
        tiroTriplo = true;
        StartCoroutine(tiroTriploDesativar());
    }
    public IEnumerator tiroTriploDesativar(){
        yield return new WaitForSeconds(5.0f);
        tiroTriplo = false;
    }

     public void speedBoostAtivar(){
        speedBoost = true;
        StartCoroutine(speedBoostDesativar());
    }
    public IEnumerator speedBoostDesativar(){
        yield return new WaitForSeconds(5.0f);
        speedBoost = false;
    }

    public void enableShield(){
        shield = true;
        _shieldGameObject.SetActive(true);
    }

    public void disableShield(){
        shield = false;
        _shieldGameObject.SetActive(false);
    }
}
