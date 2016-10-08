using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {
/**
 * Esta classe estende a classe genérica {@link Piece} e é usada para preencher
 * casas vazias do tabuleiro (casas "sem" peças).
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see   Piece
 */
public class NullPiece : Piece {
public override String getClass2()
{
return "NullPiece";
}
new public static String getClass()
{
return "NullPiece";
}

	public NullPiece() : base(""){

		
	}
	
	public override String getString(){
		return "";
	}
	
}
}