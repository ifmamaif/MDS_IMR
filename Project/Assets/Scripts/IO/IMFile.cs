using UnityEngine;
using System.IO;

public class IMFile {
	
	public static string ReadLine(FileStream f){
		char auxChar = '\0';
		string auxString;
		while (auxChar <= 32) {
			auxChar = (char)f.ReadByte ();					
		}

		auxString = auxChar.ToString();
		auxChar = (char)f.ReadByte ();	
		while (auxChar > 31) {
			auxString += auxChar.ToString();
			auxChar = (char)f.ReadByte ();			
		}
		return auxString;
	}

}
