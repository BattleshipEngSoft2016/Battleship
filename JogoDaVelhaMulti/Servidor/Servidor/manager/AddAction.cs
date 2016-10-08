using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende a classe {@link Action} para guardar informações da
 * adição de uma peça num jogo.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see Action
 */
public class AddAction : Action {
public override String getClass2()
{
return "AddAction";
}
new public static String getClass()
{
return "AddAction";
}

	public int X = -1;
	public int Y = -1;
		
	public Piece ACTOR = new NullPiece();
	
	public AddAction (int x, int y, Piece Actor) : base(ADD){

		TYPE = ADD;
		X = x;
		Y = y;
		ACTOR = Actor;
	}
}
}