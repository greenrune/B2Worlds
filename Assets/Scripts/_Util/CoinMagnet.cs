using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class CoinMagnet : MonoBehaviour
    {
        private GameObject player;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if(player != null) {
                transform.LookAt(player.transform.position);
                transform.Translate(Vector3.forward * Time.deltaTime * 15);
            }
        }
    }
