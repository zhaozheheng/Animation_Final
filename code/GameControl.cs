using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl {
	public List<List<List<Unit>>> cubeMatrix = null;
	public string record;
	private int cubeLength = 3;

	//生成对应的旋转公式
	public void rotate(string direction, bool isClockWise, bool flag = true){
		int iterations = 1;
		bool whichWise = (isClockWise == false);
		List<Unit> previousMatrix = null, currentMatrix = null;
		if(whichWise){
			iterations = cubeLength;
		}
		switch(direction){
		case "front":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("XY", 0, false));
				currentMatrix = getMatrix (getFace ("XY", 0, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [(j + 2) % 8].setColors (previousMatrix [j].getColors ());
					currentMatrix [(j + 2) % 8].colorChangeRotate ("Z");
				}
			}
			if(flag != false){
				record += "F";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		case "midXY":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("XY", 1, false));
				currentMatrix = getMatrix (getFace ("XY", 1, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [(j + 2) % 8].setColors (previousMatrix [j].getColors ());
					currentMatrix [(j + 2) % 8].colorChangeRotate ("Z");
				}
			}
			break;
		case "back":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("XY", 2, false));
				currentMatrix = getMatrix (getFace ("XY", 2, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [j].setColors (previousMatrix [(j + 2) % 8].getColors ());
					currentMatrix [j].colorChangeRotate ("Z");
					currentMatrix [j].colorChangeRotate ("Z");
					currentMatrix [j].colorChangeRotate ("Z");
				}
			}
			if(flag != false){
				record += "B";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		case "right":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("YZ", 2, false));
				currentMatrix = getMatrix (getFace ("YZ", 2, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [j].setColors (previousMatrix [(j + 2) % 8].getColors ());
					currentMatrix [j].colorChangeRotate ("X");
				}
			}
			if(flag != false){
				record += "R";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		case "midYZ":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("YZ", 1, false));
				currentMatrix = getMatrix (getFace ("YZ", 1, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [(j + 2) % 8].setColors (previousMatrix [j].getColors ());
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
				}
			}
			break;
		case "left":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("YZ", 0, false));
				currentMatrix = getMatrix (getFace ("YZ", 0, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [(j + 2) % 8].setColors (previousMatrix [j].getColors ());
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("X");
				}
			}
			if(flag != false){
				record += "L";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		case "up":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("ZX", 2, false));
				currentMatrix = getMatrix (getFace ("ZX", 2, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [(j + 2) % 8].setColors (previousMatrix [j].getColors ());
					currentMatrix [(j + 2) % 8].colorChangeRotate ("Y");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("Y");
					currentMatrix [(j + 2) % 8].colorChangeRotate ("Y");
				}
			}
			if(flag != false){
				record += "U";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		case "midZX":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("ZX", 1, false));
				currentMatrix = getMatrix (getFace ("ZX", 1, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [j].setColors (previousMatrix [(j + 2) % 8].getColors ());
					currentMatrix [j].colorChangeRotate ("Y");
				}
			}
			break;
		case "down":
			for(int i = 0; i < iterations; i++){
				previousMatrix = getMatrix (getFace ("ZX", 0, false));
				currentMatrix = getMatrix (getFace ("ZX", 0, true));
				for(int j = 0; j < 8; j++){
					currentMatrix [j].setColors (previousMatrix [(j + 2) % 8].getColors ());
					currentMatrix [j].colorChangeRotate ("Y");
				}
			}
			if(flag != false){
				record += "D";
				if(isClockWise == false){
					record += "i";
				}
			}
			break;
		default:
			break;
		}
	}

	//获取变化之后的各个面
	public List<List<Unit>> getFace(string kind, int r, bool flag){
		List<List<Unit>> face = null;
		List<Unit> row = null;
		switch(kind){
		case "XY":
			face = new List<List<Unit>>();
			for (int i = 0; i < cubeLength; i++){
				row = new List<Unit>();
				for (int j = 0; j < cubeLength; j++){
					if (flag){
						row.Add(cubeMatrix[i][j][r]);
					}else{
						Unit tempCube = new Unit(cubeMatrix[i][j][r].getColors());
						row.Add(tempCube);
					}
				}
				face.Add(row);
			}
			return face;
		case "YZ":
			face = new List<List<Unit>>();
			for (int i = 0; i < cubeLength; i++){
				row = new List<Unit>();
				for (int j = 0; j < cubeLength; j++){
					if (flag){
						row.Add(cubeMatrix[r][i][j]);
					}else{
						Unit tempCube = new Unit(cubeMatrix[r][i][j].getColors());
						row.Add(tempCube);
					}
				}
				face.Add(row);
			}
			return face;
		case "ZX":
			face = new List<List<Unit>>();
			for (int i = 0; i < cubeLength; i++){
				row = new List<Unit>();
				for (int j = 0; j < cubeLength; j++){
					if (flag){
						row.Add(cubeMatrix[i][r][j]);
					}else{
						Unit tempCube = new Unit(cubeMatrix[i][r][j].getColors());
						row.Add(tempCube);
					}
				}
				face.Add(row);
			}
			return face;
		default:
			return face;
		}
	}

	//返回一个面上所有face的二阶矩阵
	List<Unit> getMatrix(List<List<Unit>> face){
		List<Unit> matrix = new List<Unit> ();
		for(int i = 0; i < cubeLength; i++){
			matrix.Add (face [0] [i]);
		}
		matrix.Add (face [1] [2]);
		for(int i = cubeLength; i > 0; i--){
			matrix.Add (face [2] [i - 1]);
		}
		matrix.Add (face [1] [0]);
		return matrix;
	}
}
