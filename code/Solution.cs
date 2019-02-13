using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Solution {
	Dto dto = new Dto();
	RubiksCube rubiksCube;
	private int cubeLength = 3;

	public Solution(RubiksCube rCube){
		rubiksCube = rCube;
	}

	public string Solutions(){
		rubiksCube.eraseRecord();

		//将魔方颠倒，使得白色面朝上
		if(rubiksCube.gameControl.cubeMatrix[1][2][1].getColor(Unit.faces.top) != dto.getWhite()){
			if(rubiksCube.gameControl.cubeMatrix[1][2][1].getColor(Unit.faces.top) == dto.getYellow()){
				rubiksCube.shift("z", true);
				rubiksCube.shift("z", true);
			}else{
				while(rubiksCube.gameControl.cubeMatrix[1][1][0].getColor(Unit.faces.top) != dto.getWhite()){
					rubiksCube.shift("y", true);
				}
				rubiksCube.shift("x", false);
			}
		}
		stage (2);
		stage (3);
		rubiksCube.shift("z", true);
		rubiksCube.shift("z", true);
		stage (4);
		stage (5);
		stage (6);
		return rubiksCube.gameControl.record;
	}

	//不同的解密阶段
	private void stage(int stageID){
		switch(stageID){
		case 2://先使第一层产生白色的十字
			for(int i = 0; i <= cubeLength; i++){
				handling (rubiksCube, "whiteEdges");
				rubiksCube.shift ("y", true);
			}
			break;
		case 3://解决白色面的边角
			for(int i = 0; i <= cubeLength; i++){
				handling (rubiksCube, "whiteCorners");
				rubiksCube.shift ("y", true);
			}
			break;
		case 4://解决中间层的棱
			for(int i = 0; i <= cubeLength; i++){
				handling (rubiksCube, "MRowsOnEdges");
				rubiksCube.shift ("y", true);
			}
			break;
		case 5://解决顶层，使其产生十字
			solutionAfterChecking ("cross&top");
			break;
		case 6://解决余下的乱序方块
			solutionAfterChecking ("allSolved");
			break;
		default:
			break;
		}
	}

	//对不同的问题进行不同的处理
	private void handling(RubiksCube rCube, string dif){
		Color color, colorA, colorB;
		Vector3 position, targetPos;
		Unit tempCube;
		switch(dif){
		case "whiteEdges":
			color = rCube.gameControl.cubeMatrix [1] [1] [0].getColor (Unit.faces.front);
			position = rCube.coloredCubesOnEdge (color, dto.getWhite());
			//Debug.Log (position);
			targetPos = new Vector3 (1, 2, 0);
			if(targetPos != position){
				if(position.y == 2){
					switch((int)position.x){
					case 0:
						rCube.gameControl.rotate ("left", true);
						break;
					case 1:
						rCube.gameControl.rotate ("back", true);
						rCube.gameControl.rotate ("back", true);
						break;
					case 2:
						rCube.gameControl.rotate ("right", false);
						break;
					default:
						break;
					}
				}else if(position.y == 1 && position.z ==2){
					switch((int)position.x){
					case 0:
						rCube.gameControl.rotate ("back", true);
						rCube.gameControl.rotate ("down", true);
						rCube.gameControl.rotate ("back", false);
						break;
					case 2:
						rCube.gameControl.rotate ("back", false);
						rCube.gameControl.rotate ("down", true);
						rCube.gameControl.rotate ("back", true);
						break;
					default:
						break;
					}
				}
				position = rCube.coloredCubesOnEdge (color, dto.getWhite());
				//Debug.Log (position);
				if(position.y == 0){
					while(position.z != 0){
						rCube.gameControl.rotate ("down", true);
						position = rCube.coloredCubesOnEdge (color, dto.getWhite());
						//Debug.Log (position);
					}
				}
				while(position.y != 2){
					rCube.gameControl.rotate ("front", true);
					position = rCube.coloredCubesOnEdge (color, dto.getWhite());
				}
			}
			tempCube = rCube.gameControl.cubeMatrix[(int)targetPos.x][(int)targetPos.y][(int)targetPos.z];

			if(tempCube.getColor(Unit.faces.top) != dto.getWhite()){
				rCube.shift ("y", true);
				rCube.RunFormula (dto.formulas[0]);
				//Debug.Log ("0");
				rCube.shift ("y", false);
			}
			break;
		case "whiteCorners":
			colorA = rCube.gameControl.cubeMatrix [1] [1] [0].getColor (Unit.faces.front);
			colorB = rCube.gameControl.cubeMatrix [2] [1] [1].getColor (Unit.faces.right);
			position = rCube.coloredCubesInCorner (colorA, colorB, dto.getWhite());
			targetPos = new Vector3 (2, 2, 0);
			if(targetPos != position){
				if(position.y == 2){
					if(position.z == 0){
						rCube.gameControl.rotate ("left", true);
						rCube.gameControl.rotate ("down", true);
						rCube.gameControl.rotate ("left", false);
					}
					if(position.z == 2){
						switch((int)position.x){
						case 0:
							rCube.gameControl.rotate ("left", false);
							rCube.gameControl.rotate ("down", true);
							rCube.gameControl.rotate ("left", true);
							break;
						case 2:
							rCube.gameControl.rotate ("right", true);
							rCube.gameControl.rotate ("down", true);
							rCube.gameControl.rotate ("right", false);
							break;
						default:
							break;
						}
					}
				}

				position = rCube.coloredCubesInCorner (colorA, colorB, dto.getWhite());
				while(position != new Vector3(2, 0, 0)){
					rCube.gameControl.rotate ("down", true);
					position = rCube.coloredCubesInCorner (colorA, colorB, dto.getWhite());
				}
			}
			while(true){
				position = rCube.coloredCubesInCorner (colorA, colorB, dto.getWhite());
				tempCube = rCube.gameControl.cubeMatrix[(int)position.x][(int)position.y][(int)position.z];

				if(position == targetPos && tempCube.getColor(Unit.faces.front) == colorA && tempCube.getColor(Unit.faces.top) == dto.getWhite()){
					break;
				}
				rCube.RunFormula (dto.formulas[1]);
				//Debug.Log ("1");
			}
			break;
		case "MRowsOnEdges":
			colorA = rCube.gameControl.cubeMatrix [1] [1] [0].getColor (Unit.faces.front);
			colorB = rCube.gameControl.cubeMatrix [2] [1] [1].getColor (Unit.faces.right);
			position = rCube.coloredCubesOnEdge (colorA, colorB);
			targetPos = new Vector3 (2, 1, 0);
			if(position != targetPos || rCube.gameControl.cubeMatrix[(int)targetPos.x][(int)targetPos.y][(int)targetPos.z].getColor(Unit.faces.front) != colorA){
				if(position.y != 2){
					if(position.z == 0){
						switch((int)position.x){
						case 0:
							rCube.RunFormula (dto.formulas[3]);
							break;
						case 2:
							rCube.RunFormula (dto.formulas[2]);
							break;
						default:
							break;
						}
					}else{
						rCube.shift ("y", true);
						rCube.shift ("y", true);

						switch((int)position.x){
						case 0:
							rCube.RunFormula (dto.formulas[2]);
							break;
						case 2:
							rCube.RunFormula (dto.formulas[3]);
							break;
						default:
							break;
						}

						rCube.shift ("y", true);
						rCube.shift ("y", true);
					}
				}

				position = rCube.coloredCubesOnEdge (colorA, colorB);
				while(position != new Vector3(1, 2, 0)){
					rCube.gameControl.rotate ("up", true);
					position = rCube.coloredCubesOnEdge (colorA, colorB);
				}

				if(rCube.gameControl.cubeMatrix[(int)position.x][(int)position.y][(int)position.z].getColor(Unit.faces.front) != colorA){
					rCube.gameControl.rotate ("up", false);
					rCube.shift ("y", false);
					rCube.RunFormula (dto.formulas[3]);
					//Debug.Log ("3");
					rCube.shift ("y", true);
				}else{
					rCube.RunFormula (dto.formulas[2]);
					//Debug.Log ("2");
				}
			}
			break;
		default:
			break;
		}
	}

	//检测方块的不同状态：例如cross和top是否完成或者是是否全部完成，若没有完成执行相应的公式进行变化
	private void solutionAfterChecking(string kind){
		switch(kind){
		case "cross&top"://解决颠倒之后黄色面，使其产生一个十字
			while(rubiksCube.checkOnTop("Cross") == false){
				for(int i = 0; i <= 3; i++){
					if(rubiksCube.checkOnTop("Backward")){
						break;
					}
					if(rubiksCube.checkOnTop("Horizontal")){
						break;
					}
					rubiksCube.gameControl.rotate("up", true);
				}

				if(rubiksCube.checkOnTop("Horizontal")){
					rubiksCube.RunFormula (dto.formulas[5]);
					//Debug.Log ("5");
				}else{
					rubiksCube.RunFormula (dto.formulas[4]);
					//Debug.Log ("4");
				}
			}
			
			while(rubiksCube.checkOnTop("All") == false){
				bool tag = true;

				for(int i = 0; i <= cubeLength; i++){
					Unit TLB = rubiksCube.gameControl.cubeMatrix [0] [2] [2];
					Unit TRB = rubiksCube.gameControl.cubeMatrix [2] [2] [2];

					if(TLB.getColor(Unit.faces.top) == dto.getYellow() && TRB.getColor(Unit.faces.top) == dto.getYellow()){
						tag = false;
						break;
					}
					rubiksCube.shift ("y", true);
				}

				if(tag){
					for(int i = 0; i <= cubeLength; i++){
						Unit TLF = rubiksCube.gameControl.cubeMatrix [0] [2] [0];
						if(TLF.getColor(Unit.faces.top) == dto.getYellow()){
							tag = false;
							break;
						}
						rubiksCube.shift ("y", true);
					}
				}

				if(tag){
					for(int i = 0; i <= cubeLength; i++){
						Unit TLF = rubiksCube.gameControl.cubeMatrix [0] [2] [0];
						if(TLF.getColor(Unit.faces.left) == dto.getYellow()){
							tag = false;
							break;
						}
						rubiksCube.shift ("y", true);
					}
				}

				rubiksCube.RunFormula (dto.formulas[6]);
				//Debug.Log ("6");
			}
			break;
		case "allSolved"://使剩余的错误方块归位
			bool isCornersOK = false;

			for(int i = 0; i <= cubeLength; i++){
				if(rubiksCube.checkOnTop("Corners")){
					isCornersOK = true;
					break;
				}
				rubiksCube.gameControl.rotate ("up", true);
			}

			while(isCornersOK == false){
				bool canFindCornersNearby = false;
				for(int i = 0; i <= cubeLength; i++){
					Color TFL = rubiksCube.gameControl.cubeMatrix [0] [2] [0].getColor (Unit.faces.front);
					Color TFR = rubiksCube.gameControl.cubeMatrix [2] [2] [0].getColor (Unit.faces.front);

					if(TFL == TFR){
						canFindCornersNearby = true;
						break;
					}
					rubiksCube.shift ("y", true);
				}

				if(canFindCornersNearby){
					Color TFL = rubiksCube.gameControl.cubeMatrix [0] [2] [0].getColor (Unit.faces.front);
					Color MFM = rubiksCube.gameControl.cubeMatrix [1] [1] [0].getColor (Unit.faces.front);

					while(TFL != MFM){
						rubiksCube.gameControl.rotate ("up", true);
						rubiksCube.shift ("y", true);
						TFL = rubiksCube.gameControl.cubeMatrix [0] [2] [0].getColor (Unit.faces.front);
						MFM = rubiksCube.gameControl.cubeMatrix [1] [1] [0].getColor (Unit.faces.front);
					}

					rubiksCube.shift ("y", true);
					rubiksCube.shift ("y", true);
				}

				rubiksCube.RunFormula (dto.formulas[7]);
				//Debug.Log ("7");
				for(int i = 0; i <= cubeLength; i++){
					if(rubiksCube.checkOnTop("Corners")){
						isCornersOK = true;
						break;
					}
					rubiksCube.gameControl.rotate ("up", true);
				}
			}

			while(rubiksCube.allSolved() == false){
				for(int i = 0; i <= cubeLength; i++){
					Color TFL = rubiksCube.gameControl.cubeMatrix [0] [2] [0].getColor (Unit.faces.front);
					Color TFM = rubiksCube.gameControl.cubeMatrix [1] [2] [0].getColor (Unit.faces.front);

					if(TFL == TFM){
						rubiksCube.shift ("y", true);
						rubiksCube.shift ("y", true);
						break;
					}
					rubiksCube.shift ("y", true);
				}

				Color TFMtemp = rubiksCube.gameControl.cubeMatrix [1] [2] [0].getColor (Unit.faces.front);
				Color MRM = rubiksCube.gameControl.cubeMatrix [2] [1] [1].getColor (Unit.faces.right);

				if(TFMtemp == MRM){
					rubiksCube.RunFormula (dto.formulas[9]);
					//Debug.Log ("9");
				}else{
					rubiksCube.RunFormula (dto.formulas[8]);
					//Debug.Log ("8");
				}
			}
			break;
		default:
			break;
		}
	}

}
