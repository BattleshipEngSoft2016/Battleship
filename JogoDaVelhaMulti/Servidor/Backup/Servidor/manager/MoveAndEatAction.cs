using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende a classe {@link Action} para guardar informações sobre
 * o movimento que acarretou na eliminação de uma peça num jogo.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see Action
 */
public class MoveAndEatAction : MoveAction {
public override String getClass2()
{
return "MoveAndEatAction";
}
new public static String getClass()
{
return "MoveAndEatAction";
}

	public Piece EATEN = new NullPiece();
	public int EATEN_X = -1;
	public int EATEN_Y = -1;
	
	public MoveAndEatAction (int x, int y, int x2, int y2, Piece Actor, Piece Eaten) : base(x,y,x2,y2,Actor){

		TYPE = MOVE_AND_EAT;
		FROM_X = x;
		FROM_Y = y;
		TO_X = x2;
		TO_Y = y2;
		ACTOR = Actor;
		EATEN = Eaten;
		EATEN_X = x2;
		EATEN_Y = y2;
	}

	
	public MoveAndEatAction (int x, int y, int x2, int y2, Piece Actor, Piece Eaten, int x3, int y3) : base(x,y,x2,y2,Actor){

		TYPE = MOVE_AND_EAT;
		FROM_X = x;
		FROM_Y = y;
		TO_X = x2;
		TO_Y = y2;
		ACTOR = Actor;
		EATEN = Eaten;
		EATEN_X = x3;
		EATEN_Y = y3;
	}
	
	public override String action2String(){
		return "MOVE AND EAT PIECE '" + ACTOR.getString() + "' from (" + FROM_X+","+FROM_Y+") to (" + TO_X +","+TO_Y+"); EATEN PIECE '" + EATEN.getString() + "' (" + EATEN_X +","+EATEN_Y+")";
	}
}
}