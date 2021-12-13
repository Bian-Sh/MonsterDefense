/**
 * Copyright 2019 Oskar Sigvardsson
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace GK {
	public class Shoot : MonoBehaviour {
		public GameObject Projectile; 
		public float MinDelay = 0.25f;
		public float InitialSpeed = 10.0f;
		public Transform SpawnLocation;
		float lastShot = -1000.0f;

		[Header("声效")]
		public AudioSource audioSource; //声音播放器
		[Header("枪火")]
		public ParticleSystem gunfire; //枪火焰预制体


		//void Update() {
		//	var shooting = CrossPlatformInputManager.GetButton("Fire1");
		//	if (shooting) {
		//		if (Time.time - lastShot >= MinDelay) {
		//			lastShot = Time.time;
		//			var go = Instantiate(Projectile, SpawnLocation.position, SpawnLocation.rotation);
		//			go.GetComponent<Rigidbody>().velocity = InitialSpeed *SpawnLocation.forward;
		//		}
		//	} 
		//}

		public void Fire() 
		{
			var go = Instantiate(Projectile, SpawnLocation.position, SpawnLocation.rotation);
			go.transform.localScale *= 0.01f;
			var rg = go.GetComponent<Rigidbody>();
			rg.velocity = InitialSpeed * SpawnLocation.forward;
			var fire = Instantiate(gunfire,SpawnLocation.position,SpawnLocation.rotation,SpawnLocation);
			fire.transform.localEulerAngles = new Vector3(0,-90,0);
			fire.transform.localScale *= 0.5f;
			audioSource.Play();


		}
	}
}
