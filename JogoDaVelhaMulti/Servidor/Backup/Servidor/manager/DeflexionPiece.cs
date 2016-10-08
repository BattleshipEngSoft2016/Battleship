using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe implementas todas as peças do jogo deflexion ({@link DeflexionGame}.
 * Ela estende a classe genérica de peças {@link Piece} para permitir a orientação
 * (a direção para a qual os espelhos estão apontando) e os tipos de peça do
 * deflexion (pharoah, djed, pyramid e obelisk).
 *  
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see DeflexionGame
 * @see Piece
 */
public class DeflexionPiece : Piece{
public override String getClass2()
{
return "DeflexionPiece";
}
new public static String getClass()
{
return "DeflexionPiece";
}
	public const String PHAROAH = "pharoah";
	public const String DJED = "djed";
	public const String PYRAMID = "pyramid";
	public const String OBELISK = "obelisk";
	
	private String orientation = "NE";
	private String type = PYRAMID;
	public DeflexionPiece(String id, int player) : base(id, player) {

	}

	public DeflexionPiece(String id, int player, String type, String orientation) : base(id, player) {

		this.orientation = orientation.ToUpper();
		this.type = type;
	}

	public DeflexionPiece(String id, int player, String type) : base(id, player) {

		this.type = type;
	}
	
	public void update_orientation(String ori) {
		this.orientation = ori.ToUpper();
	}
	
	public String get_orientation(){
		return orientation;
	}
	
	public String get_type(){
		return type;
	}
	

	public override String getString(){
		String tag_orientation = "??";
		if (orientation.Equals("NE",StringComparison.CurrentCultureIgnoreCase)){
			tag_orientation = "$\\";
		}else if (orientation.Equals("NW",StringComparison.CurrentCultureIgnoreCase)){
			tag_orientation = "/$";
		}else if (orientation.Equals("SE",StringComparison.CurrentCultureIgnoreCase)){
			tag_orientation = "$/";
		}else if (orientation.Equals("SW",StringComparison.CurrentCultureIgnoreCase)){
			tag_orientation = "\\$";
		}
		
		
		if (type.Equals(PHAROAH,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Pha" +ID;
		}else if (type.Equals(OBELISK,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Obe" + ID;
		}else if (type.Equals(DJED,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "D"+tag_orientation + ID;
		}else if (type.Equals(PYRAMID,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "P" +tag_orientation+ ID;
		}
		
		Console.WriteLine(type + " " + tag_orientation);
		return "ERROR - invalid type: "+type;
	}
	
}
}