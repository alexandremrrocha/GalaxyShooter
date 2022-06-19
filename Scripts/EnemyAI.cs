using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _inimigoExplosaoPrefab;

     [SerializeField]
    private float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.down * _speed * Time.deltaTime);
         if(transform.position.y <= -5.82f){
           transform.position = new Vector3 (Random.Range(-8.3f, 8.3f),6.22f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D outro) {
        if(outro.tag == "Player"){
            Player player = outro.GetComponent<Player>();
            if(player != null && player.shield == false){                    
                player.vidas -= 1;
            }else if(player != null && player.shield == true){
                player.disableShield();
            }
            Instantiate(_inimigoExplosaoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        } else if(outro.tag == "Laser"){
            Laser laser = outro.GetComponent<Laser>();                    //não precisaria de pegar ele aqui
            Instantiate(_inimigoExplosaoPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            laser.Destruir();           //poderia ter colocado simplesmente outro.Destroy(gameObject);
        }
    }
}
