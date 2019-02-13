using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitRenderer : MonoBehaviour {

	public List<GameObject> faces = new List<GameObject> ();

	public void reRender(Unit cube){
		for(int i = 0; i < faces.Count; i++){
			Renderer renderer = faces [i].GetComponent<Renderer> ();
			renderer.enabled = true;
			renderer.material.color = cube.getColors () [i];
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
