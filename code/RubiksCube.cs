using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube{
	Dto dto = new Dto();
	public GameControl gameControl;
	private int cubeLength = 3;

	public RubiksCube(GameControl control){
		gameControl = control;

		gameControl.cubeMatrix = new List<List<List<Unit>>> ();
		//初始化所有方块为灰色
		for(int i = 0; i < cubeLength; i++){
			List<List<Unit>> CubeRow = new List<List<Unit>> ();
			for(int j = 0; j < cubeLength; j++){
				List<Unit> CubeColum = new List<Unit> ();
				for(int k = 0; k < cubeLength; k++){
					Unit tempCube = new Unit ();
					tempCube.setAllEdgeColors (dto.getGrey());
					CubeColum.Add (tempCube);
				}
				CubeRow.Add (CubeColum);
			}
			gameControl.cubeMatrix.Add (CubeRow);
		}
		//为每个面着色,国际魔方标准色为：上黄－下白，前蓝－后绿，左橙－右红。
		for(int i = 0; i < cubeLength; i++){
			for (int j = 0; j < cubeLength; j++)
			{
				gameControl.cubeMatrix[i][2][j].setColor(Unit.faces.top, dto.getYellow());
				gameControl.cubeMatrix[i][0][j].setColor(Unit.faces.bottom, dto.getWhite());
				gameControl.cubeMatrix[i][j][0].setColor(Unit.faces.front, dto.getBlue());
				gameControl.cubeMatrix[i][j][2].setColor(Unit.faces.back, dto.getGreen());
				gameControl.cubeMatrix[0][i][j].setColor(Unit.faces.left, dto.getOrange());
				gameControl.cubeMatrix[2][i][j].setColor(Unit.faces.right, dto.getRed());
			}
		}
	}

	//X,Y,Z三个方向旋转视角
	public void shift(string axis, bool isClockWise){
		switch(axis){
		case "x":
			gameControl.rotate ("left", isClockWise, false);
			gameControl.rotate ("midYZ", isClockWise);
			gameControl.rotate ("right", !isClockWise, false);

			gameControl.record += "X";
			if(isClockWise == false){
				gameControl.record += "i";
			}
			break;
		case "y":
			gameControl.rotate ("down", isClockWise, false);
			gameControl.rotate ("midZX", isClockWise);
			gameControl.rotate ("up", !isClockWise, false);

			gameControl.record += "Y";
			if(isClockWise == false){
				gameControl.record += "i";
			}
			break;
		case "z":
			gameControl.rotate ("front", isClockWise, false);
			gameControl.rotate ("midXY", isClockWise);
			gameControl.rotate ("back", !isClockWise, false);

			gameControl.record += "Z";
			if(isClockWise == false){
				gameControl.record += "i";
			}
			break;
		default:
			break;
		}
	}

	//返回棱
	public Vector3 coloredCubesOnEdge(Color a, Color b){
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					if(gameControl.cubeMatrix[i][j][k].colorNum() == 2 && gameControl.cubeMatrix[i][j][k].containColors(a, b)){
						return new Vector3 (i, j, k);
					}
				}
			}
		}
		throw new System.ArgumentException ("Wrong Wrong Wrong!");
	}

	//返回角
	public Vector3 coloredCubesInCorner(Color a, Color b, Color c){
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					if(gameControl.cubeMatrix[i][j][k].containColors(a, b, c)){
						return new Vector3 (i, j, k);
					}
				}
			}
		}
		throw new System.ArgumentException ("Wrong Wrong Wrong!");
	}

	//检测上层的各种情况下，方块是否正确归位
	public bool checkOnTop(string kind){
		bool solved = true;
		List<List<Unit>> square = null;
		switch(kind){
		case "Cross":
			square = gameControl.getFace ("ZX", 2, false);
			if(square[0][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][0].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[2][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][2].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			return solved;
		case "Horizontal":
			square = gameControl.getFace ("ZX", 2, false);
			if(square[0][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[2][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			return solved;
		case "Backward":
			square = gameControl.getFace ("ZX", 2, false);
			if(square[0][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][2].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			if(square[1][1].getColor(Unit.faces.top) != dto.getYellow()){
				solved = false;
			}
			return solved;
		case "Corners":
			for(int i = 0; i <= cubeLength; i++){
				Unit TLFCube = gameControl.cubeMatrix [0] [2] [0];
				Unit MLFCube = gameControl.cubeMatrix [0] [1] [0];
				if(TLFCube.getColor(Unit.faces.front) != MLFCube.getColor(Unit.faces.front) || 
					TLFCube.getColor(Unit.faces.left) != MLFCube.getColor(Unit.faces.left)){
					solved = false;
				}
				shift ("y", true);
			}
			return solved;
		case "All":
			square = gameControl.getFace ("ZX", 2, false);
			for(int i = 0; i < 3; i++){
				for(int j = 0; j < 3; j++){
					if(square[i][j].getColor(Unit.faces.top) != dto.getYellow()){
						solved = false;
					}
				}
			}
			return solved;
		default:
			return solved;
		}
	}

	//测试是否所有方块都正确归位
	public bool allSolved(){
		bool solved = true;
		Color color;
		List<List<Unit>> edgeCube;

		for(int i = 0; i <= cubeLength; i++){
			edgeCube = gameControl.getFace ("XY", 0, true);
			color = edgeCube [1] [1].getColor (Unit.faces.front);
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					if(edgeCube[j][k].getColor(Unit.faces.front) != color){
						solved = false;
					}
				}
			}
			shift ("y", true);
		}

		edgeCube = gameControl.getFace ("ZX", 2, true);
		color = edgeCube [1] [1].getColor (Unit.faces.top);
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				if(edgeCube[i][j].getColor(Unit.faces.top) != color){
					solved = false;
				}
			}
		}
		return solved;
	}

	//根据公式旋转魔方
	public void RunFormula (string str){
		for(int steps = 0; steps < str.Length; steps++){
			char ch = str [steps];
			bool isClockWise = true;
			if(steps + 1 < str.Length){
				if(str[steps + 1] == 'i'){
					isClockWise = false;
					steps++;
				}else{
					isClockWise = true;
				}
			}
			switch(ch){
			case 'R':
				gameControl.rotate ("right", isClockWise);
				break;
			case 'L':
				gameControl.rotate ("left", isClockWise);
				break;
			case 'U':
				gameControl.rotate ("up", isClockWise);
				break;
			case 'D':
				gameControl.rotate ("down", isClockWise);
				break;
			case 'F':
				gameControl.rotate ("front", isClockWise);
				break;
			case 'B':
				gameControl.rotate ("back", isClockWise);
				break;
			case 'X':
				shift ("x", isClockWise);
				break;
			case 'Y':
				shift ("y", isClockWise);
				break;
			case 'Z':
				shift ("z", isClockWise);
				break;
			default:
				break;
			}
		}
	}

	//随机产生一个乱序魔方旋转序列
	public string Scramble(int n){
		string str = "";
		List<string> motions = new List<string> (){ "R", "L", "U", "D", "F", "B" };

		for(int i = 0; i < n; i++){
			int j = ((int)(Random.value * 32676)) % 6;
			int k = ((int)(Random.value * 32676)) % 2;

			str += motions [j];
			if(k == 0){
				str += "i";
			}
		}
		return str;
	}

	//颠倒随机变换操作序列
	public string Reverse(string str){
		string reverseStr = "";
		int i = str.Length - 1;
		while(i >= 0){
			if (str [i] != 'i') {
				reverseStr += str [i] + "i";
				i--;
			}else{
				reverseStr += str [i - 1];
				i -= 2;
			}
		}
		return reverseStr;
	}

	//交换逆时针旋转操作的标识符
	public void swap(char a, char b){
		char c;
		c = a;
		a = b;
		b = c;
	}

	//清除所有公式，以方便生成新公式
	public void eraseRecord(){
		gameControl.record = "";
	}

	//复制相同的方块
	public RubiksCube copyCube(){
		RubiksCube rCube = new RubiksCube (new GameControl());
		rCube.gameControl.record = gameControl.record;
		for(int i = 0; i < cubeLength; i++){
			for(int j = 0; j < cubeLength; j++){
				for(int k = 0; k < cubeLength; k++){
					Unit cube = new Unit ();
					for(int r = 0; r < 6; r++){
						Color color = gameControl.cubeMatrix [i] [j] [k].getColor ((Unit.faces)r);
						cube.setColor ((Unit.faces)r, color);
					}
					rCube.gameControl.cubeMatrix[i][j][k] = cube;
				}
			}
		}
		return rCube;
	}
}

