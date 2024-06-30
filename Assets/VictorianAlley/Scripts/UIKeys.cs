using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIKeys : MonoBehaviour {

	public Light sunlight;
	public ReflectionProbe[] probes;
	public ParticleSystem[] particles;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < particles.Length; i++) {
			if(particles[i] != null) {
				particles [i].Stop ();
				particles [i].Clear ();
			}
		}	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F) && sunlight != null) {

			if (sunlight.enabled)
				sunlight.enabled = false;
			else
				sunlight.enabled = true;

			for(int i=0;i<probes.Length;i++) {
				if(probes[i] != null)
					probes[i].RenderProbe();
			}
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			for (int i = 0; i < particles.Length; i++) {
				if(particles[i] != null) {
					if (particles [i].isPlaying) {
						particles [i].Stop ();
						particles [i].Clear ();
					}
					else
						particles [i].Play ();
				}
			}	
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Screen.fullScreen = false;
			Cursor.visible = true;
		}


	}
}
