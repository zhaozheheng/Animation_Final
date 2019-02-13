using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public RubiksRenderer cPrefab;
	Solution solution;
	public Slider speedControl;
	public Text animationSpeed;
	public Toggle focusViewing;
	public bool cameraRotate = true;
	Vector3 resetCamera = new Vector3(4, 4, -4);
	string totalLink = "";
	string reverseScrambleLink = "";
	string processStep = "";
	bool CCWControl = false;
	private IEnumerator numerator;
	private Dto dto = new Dto();
	private int n = 0;

	// Use this for initialization
	void Start () {
		speedControl = speedControl.GetComponent<Slider> ();
		speedControl.value = cPrefab.rotateSpeed;
		setAnimationSpeed (cPrefab.rotateSpeed);
		focusViewing = focusViewing.GetComponent<Toggle> ();
		focusViewing.isOn = cameraRotate;

		Camera.main.transform.position = resetCamera;
		Camera.main.transform.LookAt (cPrefab.transform.position);
	}
	
	// Update is called once per frame
	public void Update () {
		if(cameraRotate){
			Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, Time.deltaTime * 10);
		}
		if(Input.GetKey (KeyCode.LeftShift)){
			CCWControl = true;
		}else{
			CCWControl = false;
		}
		if(CCWControl){
			if(Input.GetKeyDown(KeyCode.R)){
				StartCoroutine(cPrefab.processStep ("Ri"));
				processStep += "Ri";
				totalLink += "Ri";
			}
			if(Input.GetKeyDown(KeyCode.L)){
				StartCoroutine(cPrefab.processStep ("Li"));
				processStep += "Li";
				totalLink += "Li";
			}
			if(Input.GetKeyDown(KeyCode.U)){
				StartCoroutine(cPrefab.processStep ("Ui"));
				processStep += "Ui";
				totalLink += "Ui";
			}
			if(Input.GetKeyDown(KeyCode.D)){
				StartCoroutine(cPrefab.processStep ("Di"));
				processStep += "Di";
				totalLink += "Di";
			}
			if(Input.GetKeyDown(KeyCode.B)){
				StartCoroutine(cPrefab.processStep ("Bi"));
				processStep += "Bi";
				totalLink += "Bi";
			}
			if(Input.GetKeyDown(KeyCode.F)){
				StartCoroutine(cPrefab.processStep ("Fi"));
				processStep += "Fi";
				totalLink += "Fi";
			}
		}else{
			if(Input.GetKeyDown(KeyCode.R)){
				StartCoroutine(cPrefab.processStep ("R"));
				processStep += "R";
				totalLink += "R";
			}
			if(Input.GetKeyDown(KeyCode.L)){
				StartCoroutine(cPrefab.processStep ("L"));
				processStep += "L";
				totalLink += "L";
			}
			if(Input.GetKeyDown(KeyCode.U)){
				StartCoroutine(cPrefab.processStep ("U"));
				processStep += "U";
				totalLink += "U";
			}
			if(Input.GetKeyDown(KeyCode.D)){
				StartCoroutine(cPrefab.processStep ("D"));
				processStep += "D";
				totalLink += "D";
			}
			if(Input.GetKeyDown(KeyCode.B)){
				StartCoroutine(cPrefab.processStep ("B"));
				processStep += "B";
				totalLink += "B";
			}
			if(Input.GetKeyDown(KeyCode.F)){
				StartCoroutine(cPrefab.processStep ("F"));
				processStep += "F";
				totalLink += "F";
			}
		}
		if(Input.GetKeyDown(KeyCode.I)){
			reverseSolution ();
		}
		if(Input.GetKeyDown(KeyCode.S)){
			reset ();
			totalLink += dto.specialFigure [n];
			specialFigure (dto.specialFigure [n]);
			n++;
			if(n > dto.specialFigure.Count - 1){
				n = 0;
			}
		}
	}

	//重新设置魔方状态
	public void reset(){
		reverseScrambleLink = cPrefab.rubiksCube.Reverse(totalLink);
		cPrefab.rubiksCube.RunFormula (reverseScrambleLink);
		totalLink = "";
		reverseScrambleLink = "";
		processStep = "";
	}

	//设置解决魔方的旋转速度
	public void setAnimationSpeed(float speed){
		animationSpeed.text = "Solve Speed: " + (int)speed;
		cPrefab.rotateSpeed = speed;
	}

	//搅乱魔方，使其随机乱序
	public void Scramble(){
		if(numerator != null){
			StopCoroutine (numerator);
		}
		string scrambleLink = cPrefab.rubiksCube.Scramble (50);
		totalLink += scrambleLink;
		cPrefab.rubiksCube.RunFormula (scrambleLink);
		Debug.Log (totalLink);
		cPrefab.rubiksCube.eraseRecord ();
		cPrefab.refresh ();
	}

	//用reverse的序列恢复魔方
	public void reverseSolution(){
		reverseScrambleLink = cPrefab.rubiksCube.Reverse(totalLink);
		Debug.Log (reverseScrambleLink);
		numerator = cPrefab.processStep (reverseScrambleLink);
		StartCoroutine(numerator);
		totalLink = "";
		reverseScrambleLink = "";
		processStep = "";
	}

	//显示一些有趣的图形
	public void specialFigure(string str){
		numerator = cPrefab.processStep (str);
		StartCoroutine(numerator);
	}

	//解决魔方
	public void Slove(){
		Debug.Log ("Solve");
		if(numerator != null){
			StopCoroutine (numerator);
		}

		RubiksCube rCube = cPrefab.rubiksCube.copyCube ();
		solution = new Solution (rCube);

		string sol = solution.Solutions ();
		//GameControl gameControl = new GameControl();
		//RubiksCube solCube = new RubiksCube (gameControl);
		//solCube.RunCustomFormula (sol);
		numerator = cPrefab.processStep (sol);
		StartCoroutine (numerator);
		Debug.Log (sol);
		totalLink = "";
		reverseScrambleLink = "";
	}
}
