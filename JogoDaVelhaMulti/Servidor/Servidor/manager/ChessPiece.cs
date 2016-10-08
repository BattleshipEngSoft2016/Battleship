using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe implementa todas as peças de Xadrez {@link ChessGame}.
 * Ela estende a classe de peças genéricas {@link Piece} para a criação
 * das peças de xadres: king, queen, rook, bishop, knight, pawn 
 * (respectivamente: rei, rainha, torre, bisto, cavalo e peão).
 *  
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 *
 * @see ChessGame
 * @see Piece
 */

public class ChessPiece : Piece{
public override String getClass2()
{
return "ChessPiece";
}
new public static String getClass()
{
return "ChessPiece";
}
	public const String KING   = "king";
	public const String QUEEN  = "queen";
	public const String ROOK   = "rook";
	public const String BISHOP = "bishop";
	public const String KNIGHT = "knight";
	public const String PAWN   = "pawn";
	
	private String type = PAWN;
	
	public ChessPiece(String id, int player) : base(id, player) {

	}

	public ChessPiece(String id, int player, String type) : base(id, player) {

		this.type = type;
	}

	public String get_type(){
		return type;
	}
	
	public override String getString(){
		if (type.Equals(KING,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Kin" + ID;
		}else if (type.Equals(QUEEN,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Que" + ID;
		}else if (type.Equals(ROOK,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Roo" + ID;
		}else if (type.Equals(BISHOP,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Bis" + ID;
		}else if (type.Equals(KNIGHT,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Kni" + ID;
		}else if (type.Equals(PAWN,StringComparison.CurrentCultureIgnoreCase)){
			return ID + "Paw" + ID;
		}
		
		Console.Error.WriteLine("Invalid chess type: " + type);
		return "ERROR - invalid type: "+type;
	}
	
}
}