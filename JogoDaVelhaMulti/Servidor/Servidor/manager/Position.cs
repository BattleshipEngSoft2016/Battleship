using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe armazena um par ordenado X, Y correspondendo a posição
 * de uma peça ({@link Piece) no tabuleiro ({@link Board}).
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Piece
 * @see Board
 */
public class Position {
public virtual String getClass2()
{
return "Position";
}
public static String getClass()
{
return "Position";
}
	public int X = -1;
	public int Y = -1;

	public Position (int x, int y){
		X = x;
		Y = y;
	}
}
}