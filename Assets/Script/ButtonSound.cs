using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {
        public AudioSource mySound;
        public AudioClip clickSound;


        public void click() {

            mySound.PlayOneShot(clickSound);

        }
 }

