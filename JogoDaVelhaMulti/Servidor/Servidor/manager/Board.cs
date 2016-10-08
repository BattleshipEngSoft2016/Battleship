using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe gerencia um Tabuleiro para qualquer tipo de jogo ({@link Game}).
 * A interface pública é responsável por todas as ações num Tabuleiro.
 * Cabe ao Jogo ({@link Game}) verificar se uma ação é válida ou não 
 * (em relação às regras do jogo).
 * Cada Tabuleiro ({@link Board}) contém suas peças ({@link Piece}) e nenhuma
 * casa possui valor null; há um tipo especial de peça, correspondente a casa
 * vazia: {@link NullPiece}.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see Game
 * @see Piece
 * @see NullPiece
 */
public class Board {
public virtual String getClass2()
{
return "Board";
}
public static String getClass()
{
return "Board";
}
	private Piece[,] BOARD = null;
	private int DIMENSION_X = 0;
	private int DIMENSION_Y = 0;
	
	
	public Board(int x, int y) {
		BOARD = new Piece[x,y];
		DIMENSION_X = x;
		DIMENSION_Y = y;
		for (int contx=0;contx<x;contx++){
			for (int conty=0;conty<y;conty++){
				BOARD[contx,conty] = new NullPiece();
			}
		}

	}
	
	public Board backup(){
		Board BACKUP = new Board(DIMENSION_X,DIMENSION_Y);
		for (int contx=0;contx<DIMENSION_X;contx++){
			for (int conty=0;conty<DIMENSION_Y;conty++){
				BACKUP.removePiece(contx,conty);
				BACKUP.addPiece(contx,conty,this.getPiece(contx,conty));
			}
		}
		return BACKUP;
	}
	
	
	
	
	
	public void print (int total_space){
		print(total_space," ", " ");
	}
	
	public void print (int total_space, String internal_space, String external_space){
		String space  = "---------------------";
		for (int conty=0;conty<DIMENSION_Y;conty++){
			Console.Write(space.Substring(0,total_space+1));
		}
		Console.WriteLine('-');
		for (int contx=0;contx<DIMENSION_X;contx++){
			Console.Write('|');
			for (int conty=0;conty<DIMENSION_Y;conty++){
				String temp = "";
				if (BOARD[contx,conty] == null){
					temp = "";
				}else{
					temp = BOARD[contx,conty].getString();
				}
				temp = internal_space + temp + "            ";
				Console.Write(temp.Substring(0,total_space-external_space.Length));
				Console.Write(external_space+"|");
			}
			Console.WriteLine();
			for (int conty=0;conty<DIMENSION_Y;conty++){
				Console.Write(space.Substring(0,total_space+1));
			}
			Console.WriteLine('-');
		}
	}
	
	public String board2string (){
		String result = "";
		for (int contx=0;contx<DIMENSION_X;contx++){
			for (int conty=0;conty<DIMENSION_Y;conty++){
				String temp = " ";
				if (BOARD[contx,conty] == null){
					temp = " ";
				}else{
					temp = BOARD[contx,conty].getString();
				}
				if (temp.Equals("")){
					temp = " ";
				}
				result += "<" + temp + ">";
			}
		}
		return result;
	}
	
	
	public Piece getPiece(int x, int y){
		if (validCell(x,y)){
			return BOARD[x,y];
		}
		return (Piece)(new NullPiece());
	}
	
	public bool addPiece(int x, int y, Piece element){
		if (validCell(x,y)){
			BOARD[x,y] = element;
			return true;
		}
		return false;
	}

	public bool replacePiece(int x, int y, Piece element){
		if (validCell(x,y)){
			BOARD[x,y] = element;
			return true;
		}
		return false;
	}
	
	public bool replacePiece(int x, int y,int x2, int y2){
		if (validCell(x,y)){
			Piece temp = BOARD[x,y];
			BOARD[x,y] = BOARD[x2,y2];
			BOARD[x2,y2] = temp;
			
			return true;
		}
		return false;
	}
	
	
	public bool removePiece(int x, int y){
		if (validCell(x,y)){
			BOARD[x,y] = new NullPiece();
			return true;
		}
		return false;
	}
	
	public bool deletePiece(int x, int y){
		return removePiece(x,y);
	}
	
	public bool validCell(int x, int y){
		if ((x >= 0) && (y >= 0)){
			if ((x < DIMENSION_X) && (y < DIMENSION_Y)){
				return true;
			}
		}
		return false;
	}
	
	public bool movePiece(int x1, int y1, int x2, int y2){
		if (validCell(x1,y1) && validCell(x2,y2)){
		    BOARD[x2,y2] = BOARD[x1,y1];
		    BOARD[x1,y1] = new NullPiece();
			return true;
		}
		return false;
	}
	
	public int getLines(){
		return DIMENSION_X;
	}

	public int getColumns(){
		return DIMENSION_Y;
	}
	
	public int contPieces(Piece c1){
		int cont = 0;
		for (int contx=0;contx<DIMENSION_X;contx++){
			for (int conty=0;conty<DIMENSION_Y;conty++){
				Piece P = BOARD[contx,conty];
				if (P != null){
				  if (P.getClass2() == c1.getClass2()){
					  cont++;
				  }
				}
			}
		}
	    return cont;
	}
	
	public int contPieces(int Player){
		int cont = 0;
		for (int contx=0;contx<DIMENSION_X;contx++){
			for (int conty=0;conty<DIMENSION_Y;conty++){
				Piece P = BOARD[contx,conty];
				if (P != null){
				  if (P.getPlayer() == Player ){
					  cont++;
				  }
				}
			}
		}
	    return cont;
	}
}
}