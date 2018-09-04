using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
	Mana,
	Energy,
	Rage
}

public static class ResourceColor {

	/*Mana = "005AD4;
	Energy = "F1DA00";
	Rage = "C30B00";*/
	
	public static Color Mana = new Color(0, 90, 212);
	public static Color Energy = new Color(241, 218, 0);
	public static Color Rage = new Color(195, 11, 0);

}
