using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dto {
	
	private static Color red = Color.red;
	private static Color blue = Color.blue;
	private static Color green = Color.green;
	private static Color orange = new Color (1.0f, 0.5f, 0.0f);
	private static Color yellow = Color.yellow;
	private static Color white = Color.white;
	private static Color grey = Color.grey;

	public List<string> formulas = new List<string> (){ "RiUFiUi", "RiDiRD", "URUiRiUiFiUF", 
		"UiLiULUFUiFi", "FURUiRiFi", "FRURiUiFi", 
		"RURiURUURi", "RiFRiBBRFiRiBBRRUi", "FFULRiFFLiRUFF", 
		"FFUiLRiFFLiRUiFF" };

	public List<string> specialFigure = new List<string> (){ "UiDFiBLRiUiD", "FFRFFRiUUFFLUUBBUUFiUURDiBBDFiDDRF", "LLRRFFBBUUDD", 
		"FBRLFBRLFBRLUDiUDi", "BBFiLLRRDDBBFFLLRRUUFi", "UiDFiBLRiUiDLLRRFFBBUUDD", 
		"LLDiFFDBDLFRiUiRiDiFLLBFFL", "RRBBUULLBBUUFFLLDLiRFLLFiUiDL", "FFUUFFBBUUFB", 
		"FFLiRBBUULRiDD" };
	
	public Color getRed(){
		return red;
	}

	public Color getBlue(){
		return blue;
	}

	public Color getGreen(){
		return green;
	}

	public Color getOrange(){
		return orange;
	}

	public Color getYellow(){
		return yellow;
	}

	public Color getWhite(){
		return white;
	}

	public Color getGrey(){
		return grey;
	}
}
