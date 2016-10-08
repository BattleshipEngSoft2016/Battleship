using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe implementa dois tipos de peças de damas: a peça normal e a dama.
 * Ela estende a classe genérica {@link Piece} para ser utilizada em damas.
 * <p>
 * As peças de damas são utilizadas pelo jogo damas {@link CheckersGame}
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 * @see CheckersPiece
 * @see CheckersGame
 * @see Piece
 */
public class CheckersPiece : Piece{
public override String getClass2()
{
return "CheckersPiece";
}
new public static String getClass()
{
return "CheckersPiece";
}
	public const String SIMPLE = "simple";
	public const String CHECKER = "checker";
	public String TYPE = SIMPLE;
	
	public CheckersPiece(String id, int player) : base(id, player) {

	}

	public CheckersPiece(String id, int player, String type) : base(id, player) {

		this.TYPE = type;
	}
	
	
	
	

	public override String getString(){
		if (TYPE == SIMPLE){
			return ID.ToLower();
		}else if (TYPE == CHECKER){
			return ID.ToUpper();
		}
		return "ERROR - invalid type: "+TYPE;
	}
	
}
}