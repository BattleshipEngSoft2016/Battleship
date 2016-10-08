using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende a classe {@link Action} para guardar informações da
 * rotação de uma peça num jogo.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Action
 */
public class RotateAction : Action {
public override String getClass2()
{
return "RotateAction";
}
new public static String getClass()
{
return "RotateAction";
}

	public int X = -1;
	public int Y = -1;
	
	public String SENSE = "";
	
	public Piece ACTOR = new NullPiece();
	
	public RotateAction (int x, int y, String sense, Piece Actor) : base(ROTATE){

		TYPE = ROTATE;
		X = x;
		Y = y;
		SENSE = sense;
		ACTOR = Actor;
	}
}
}