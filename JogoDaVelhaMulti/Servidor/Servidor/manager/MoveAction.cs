using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende a classe {@link Action} para guardar informações sobre
 * o movimento de uma peça num jogo.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see Action
 */
public class MoveAction : Action {
public override String getClass2()
{
return "MoveAction";
}
new public static String getClass()
{
return "MoveAction";
}

	public int FROM_X = -1;
	public int FROM_Y = -1;
	
	public int TO_X = -1;
	public int TO_Y = -1;
	
	public Piece ACTOR = new NullPiece();
	
	public MoveAction (int x, int y, int x2, int y2, Piece Actor) : base(MOVE){

		TYPE = MOVE;
		FROM_X = x;
		FROM_Y = y;
		TO_X = x2;
		TO_Y = y2;
		ACTOR = Actor;
	}
	
	public override String action2String(){
		return "MOVE PIECE '" + ACTOR.getString() + "' from (" + FROM_X+","+FROM_Y+") to (" + TO_X +","+TO_Y+")";
	}
}
}