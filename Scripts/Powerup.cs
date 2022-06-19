using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private int powerupId; //0 = tiroTriplo      1 = speed boost    2 = escudos

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          transform.Translate(Vector3.down * _speed * Time.deltaTime);
          if(transform.position.y <= -5.82f){
            Destroy(gameObject);
        }
    }

   private void OnTriggerEnter2D(Collider2D outro) {
        if(outro.tag == "Player"){
            Player player = outro.GetComponent<Player>();
            if(player != null){                     //if(outro.tag == "Player") Das duas formas da certo
                //Tiro triplo
                if(powerupId == 0){
                    player.tiroTriploAtivar();
                }
                //Speed boost
               else if(powerupId == 1){
                   player.speedBoostAtivar();
                }
                //Escudos
               else if(powerupId == 2){
                    player.enableShield();
                }
            }
            Destroy(gameObject);
        }
   }
}
