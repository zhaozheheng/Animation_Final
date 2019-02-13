using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksRenderer : MonoBehaviour {

	public GameObject CubePrefab;
	public RubiksCube rubiksCube;
	public List<List<List<GameObject>>> prefabMatrix;
	public float space = 1.05f;
	public float rotateSpeed = 100;
	private int cubeLength = 3;
	// Use this for initialization
	void Start () {
		rubiksCube = new RubiksCube (new GameControl());

		prefabMatrix = new List<List<List<GameObject>>> ();
		for(int i = 0; i < cubeLength; i++){
			List<List<GameObject>> prefabRow = new List<List<GameObject>> ();
			for(int j = 0; j < cubeLength; j++){
				List<GameObject> prefabColumn = new List<GameObject> ();
				for(int k = 0; k < cubeLength; k++){
					GameObject unitPrefab = Instantiate (CubePrefab, Vector3.zero, Quaternion.identity) as GameObject;

					unitPrefab.transform.SetParent (transform);
					unitPrefab.transform.position = new Vector3 ((i - 1), (j - 1), (k - 1)) * space;

					prefabColumn.Add (unitPrefab);
				}
				prefabRow.Add (prefabColumn);
			}
			prefabMatrix.Add (prefabRow);
		}
	}
	
	// Update is called once per frame
	void Update () {
		refresh ();
	}

	//刷新魔方
	public void refresh(){
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					prefabMatrix [i] [j] [k].GetComponent<UnitRenderer> ().reRender (rubiksCube.gameControl.cubeMatrix [i] [j] [k]);
				}
			}
		}
	}

	//重置一下方块位置，配合刷新可使方块不会胡乱排列
	public void resetPositions(){
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					prefabMatrix [i] [j] [k].transform.position = new Vector3 ((i - 1), (j - 1), (k - 1)) * space;
					prefabMatrix [i] [j] [k].transform.rotation = Quaternion.identity;
				}
			}
		}
	}

	//根据最终的公式record来分别旋转魔方
	public IEnumerator processStep(string str){
		for(int i = 0; i < str.Length; i++){//n用于作为这个解魔方过程步骤的计数器
			char ch = str [i];
			bool isClockWise = true;
			//读取到i的话就自动转为逆时针旋转
			if(i + 1 < str.Length && str[i + 1] == 'i'){
				isClockWise = false;
				i++;
			}

			//顺时针逆时针决定转角的正负
			float rotateAngle = 0, delta = 0;
			int direction = 1;
			if(isClockWise){
				direction = -1;
			}

			//不同的旋转操作
			switch(ch){
			case 'R':
				for(rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta){
					delta = -1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [2] [j] [k].transform.RotateAround (transform.position, transform.right, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("right", isClockWise);
				break;
			case 'L':
				for(rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta){
					delta = 1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [0] [j] [k].transform.RotateAround (transform.position, transform.right, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("left", isClockWise);
				break;
			case 'U':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = -1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [j] [2] [k].transform.RotateAround (transform.position, transform.up, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("up", isClockWise);
				break;
			case 'D':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = 1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [j] [0] [k].transform.RotateAround (transform.position, transform.up, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("down", isClockWise);
				break;
			case 'B':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = -1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [j] [k] [2].transform.RotateAround (transform.position, transform.forward, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("back", isClockWise);
				break;
			case 'F':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = 1 * direction * rotateSpeed * Time.deltaTime;
					for (int j = 0; j < cubeLength; j++) {
						for (int k = 0; k < cubeLength; k++) {
							prefabMatrix [j] [k] [0].transform.RotateAround (transform.position, transform.forward, delta);
						}
					}
					yield return null;
				}
				rubiksCube.gameControl.rotate ("front", isClockWise);
				break;
			case 'X':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = direction * rotateSpeed * Time.deltaTime;
					transform.RotateAround (transform.position, transform.right, delta);
					yield return null;
				}
				rubiksCube.shift ("x", isClockWise);
				break;
			case 'Y':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = direction * rotateSpeed * Time.deltaTime;
					transform.RotateAround (transform.position, transform.up, delta);
					yield return null;
				}
				rubiksCube.shift ("y", isClockWise);
				break;
			case 'Z':
				for (rotateAngle = 0; Mathf.Abs (rotateAngle) < 90; rotateAngle += delta) {
					delta = direction * rotateSpeed * Time.deltaTime;
					transform.RotateAround (transform.position, transform.forward, delta);
					yield return null;
				}
				rubiksCube.shift ("z", isClockWise);
				break;
			default:
				break;
			}
			transform.rotation = Quaternion.identity;
			transform.position = Vector3.zero;
			resetPositions ();
			refresh ();
		}
		yield return null;
	}
}
