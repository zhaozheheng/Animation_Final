using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit{
	Dto dto = new Dto();
	public enum faces {front, left, back, right, top, bottom};
	List<Color> colors = new List<Color> ();


	public Unit(){
		for(int i = 0; i < 6; i++){
			colors.Add (dto.getGrey());
		}
		setAllEdgeColors (dto.getGrey());
	}

	public Unit(List<Color> c){
		for(int i = 0; i < 6; i++){
			colors.Add (dto.getGrey());
		}
		setColors (c);
	}

	public void setColor(faces face, Color c){
		colors [(int)face] = c;
	}

	public Color getColor(faces face){
		return colors [(int)face];
	}

	public void setColors(List<Color> colorList){
		for(int i = 0; i < 6; i++){
			setColor ((faces)i, colorList[i]);
		}
	}

	public List<Color> getColors(){
		List<Color> tempcolors = new List<Color> ();
		for(int i = 0; i < colors.Count; i++){
			tempcolors.Add (colors [i]);
		}
		return tempcolors;
	}

	public void setAllEdgeColors(Color c){
		for(int i = 0; i < colors.Count; i++){
			setColor ((faces)i, c);
		}
	}

	public bool containColors(params Color[] colorList){
		int matchedColors = 0;
		for(int i = 0; i < colorList.Length; i++){
			for(int j = 0; j < 6; j++){
				if(colors[j] == colorList[i]){
					matchedColors++;
				}
			}
		}
		return (matchedColors == colorList.Length);
	}

	//计算该层上非黑色的颜色数，用于确定方块是棱，边还是角
	public int colorNum(){
		int num = 0;
		for(int i = 0; i < 6; i++){
			if(colors[i] != dto.getGrey()){
				num++;
			}
		}
		return num;
	}

	public void colorChangeRotate(string axis){
		List<Color> previousColors = getColors ();
		switch(axis){
		case "X":
			previousColors = getColors ();
			setColor (faces.top, previousColors [(int)faces.front]);
			setColor (faces.back, previousColors [(int)faces.top]);
			setColor (faces.bottom, previousColors [(int)faces.back]);
			setColor (faces.front, previousColors [(int)faces.bottom]);
			break;
		case "Y":
			previousColors = getColors ();
			setColor (faces.front, previousColors [(int)faces.left]);
			setColor (faces.left, previousColors [(int)faces.back]);
			setColor (faces.back, previousColors [(int)faces.right]);
			setColor (faces.right, previousColors [(int)faces.front]);
			break;
		case "Z":
			previousColors = getColors ();
			setColor (faces.top, previousColors[(int)faces.left]);
			setColor (faces.right, previousColors[(int)faces.top]);
			setColor (faces.bottom, previousColors[(int)faces.right]);
			setColor (faces.left, previousColors[(int)faces.bottom]);
			break;
		default:
			break;
		}

	}
}