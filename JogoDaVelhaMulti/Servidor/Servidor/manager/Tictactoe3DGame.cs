using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende o jogo de tabuleiro genérico {@link Game} para permitir
 * o gerenciamento de um jogo da velha 3D  (tabuleiro 4 x 4 x 4).
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 * @see TictactoePiece
 * @see TictactoeGame
 */
public class Tictactoe3DGame : Game {
public override String getClass2()
{
return "Tictactoe3DGame";
}
new public static String getClass()
{
return "Tictactoe3DGame";
}
	int PLAYER1 = -1;
	int PLAYER2 = -1;	

	public Tictactoe3DGame(String name) : base(name) {

		GAME_TYPE = "tic tac toe 3D";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
	}
	
	public Tictactoe3DGame(String name, int player1) : base(name, player1) {

		GAME_TYPE = "tic tac toe 3D";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
	}

	public override void startGame(){
		if (PLAYERS.Count == 2){
			PLAYER1 = (int)PLAYERS[0];
			PLAYER2 = (int)PLAYERS[1];
			ACTUAL_PLAYER = PLAYER1;
			WAITING_PLAYERS=false;
			GAME_ENDED = false;
		}else{
			Console.Error.WriteLine("Wrong number of players:" + PLAYERS.Count);
		}
	}
	
	public bool canAdd(int x, int y, Piece P){
		if ((GAME_ENDED) || (WAITING_PLAYERS)){
			return false;
		}
		if (BOARD.getPiece(x,y).getClass2() != NullPiece.getClass()){
			return false;
		}
		int cont_O = BOARD.contPieces(PLAYER2);
		int cont_X = BOARD.contPieces(PLAYER1);

		if (P.getPlayer() == PLAYER2){
			if (cont_O == cont_X-1){
				ACTUAL_PLAYER = PLAYER1;
				return true;
			}
		}else if ((P.getPlayer() == PLAYER1)){
			if (cont_O == cont_X){
				ACTUAL_PLAYER = PLAYER2;
				return true;
			}			
		}
		return false;
	}
	
	public override bool add(int x, int y, int Player){
		TictactoePiece P = null;
		if (Player == PLAYER1){
			P = new TictactoePiece("X",PLAYER1);
		} else if (Player == PLAYER2){
			P = new TictactoePiece("O",PLAYER2);
		} else {
			Console.WriteLine("Invalid player: '" + Player + "'. Players: '"+PLAYER1+"' and '"+PLAYER2+"'.");
			return false;
		}
		if (canAdd(x,y,(Piece)P)){
			if (BOARD.addPiece(x,y,(Piece)P)){
			}else{
				Console.WriteLine("BLASFEMIA!!!");
			}
			if (gameEnded()){
				Console.WriteLine("End game. Winner: " + WINNER);
			}
			CONT_ID++;
			return true;
		}
		return false;
	}
	
	public override void initializeBoard(){
		BOARD = new Board(4,16);
	}
	
	public override bool gameEnded(){
		if (GAME_ENDED){
			return true;
		}
		if (player1Won()){
			WINNER = PLAYER1;
			GAME_ENDED = true;
			return true;
		}else if (player2Won()){
			WINNER = PLAYER2;
			GAME_ENDED = true;
			return true;
		}
		return false;
	}
	
	public bool player1Won(){
		if (WINNER != -1 && WINNER == PLAYER1){
			return true;
		}else if (won(PLAYER1)){
			WINNER = PLAYER1;
			return true;
		}
		return false;
	}
	public bool player2Won(){
		if (WINNER != -1 && WINNER == PLAYER2){
			return true;
		}else if (won(PLAYER2)){
			WINNER = PLAYER2;
			return true;
		}
		return false;
	}	
	
	
	
	public bool won(int Player){		
		int c1, c2, c3, c4;
		for (int cont=0;cont<16;cont++){
			
			
			c1 = BOARD.getPiece(0,cont).getPlayer();
			c2 = BOARD.getPiece(1,cont).getPlayer();
			c3 = BOARD.getPiece(2,cont).getPlayer();
			c4 = BOARD.getPiece(3,cont).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
				
				return true;
			}
		}
		for (int cont=0;cont<4;cont++){
			
			for (int cont2=0;cont2<4;cont2++){
				c1 = BOARD.getPiece(cont,0+cont2*4).getPlayer();
				c2 = BOARD.getPiece(cont,1+cont2*4).getPlayer();
				c3 = BOARD.getPiece(cont,2+cont2*4).getPlayer();
				c4 = BOARD.getPiece(cont,3+cont2*4).getPlayer();
				if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
					
					return true;
				}
			}
		}
		
		
		for (int cont=0;cont<4;cont++){
			
			for (int cont2=0;cont2<4;cont2++){
				c1 = BOARD.getPiece(cont,cont2).getPlayer();
				c2 = BOARD.getPiece(cont,cont2 +1*4).getPlayer();
				c3 = BOARD.getPiece(cont,cont2+2*4).getPlayer();
				c4 = BOARD.getPiece(cont,cont2+3*4).getPlayer();
				if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
					
					return true;
				}
			}
		}

		
		
		for (int cont=0;cont<4;cont++){
			c1 = BOARD.getPiece(cont,0).getPlayer();
			c2 = BOARD.getPiece(cont,5).getPlayer();
			c3 = BOARD.getPiece(cont,10).getPlayer();
			c4 = BOARD.getPiece(cont,15).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
				
				return true;
			}
		}
		for (int cont=0;cont<4;cont++){
			c1 = BOARD.getPiece(cont,3).getPlayer();
			c2 = BOARD.getPiece(cont,6).getPlayer();
			c3 = BOARD.getPiece(cont,9).getPlayer();
			c4 = BOARD.getPiece(cont,12).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
				
				return true;
			}
		}
		

		
		for (int cont=0;cont<4;cont++){
			c1 = BOARD.getPiece(0,cont).getPlayer();
			c2 = BOARD.getPiece(1,cont+4).getPlayer();
			c3 = BOARD.getPiece(2,cont+8).getPlayer();
			c4 = BOARD.getPiece(3,cont+12).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
				
				return true;
			}
		}
		for (int cont=0;cont<4;cont++){
			c1 = BOARD.getPiece(3,cont).getPlayer();
			c2 = BOARD.getPiece(2,cont+4).getPlayer();
			c3 = BOARD.getPiece(1,cont+8).getPlayer();
			c4 = BOARD.getPiece(0,cont+12).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
				
				return true;
			}
		}

		
		
		c1 = BOARD.getPiece(0,0).getPlayer();
		c2 = BOARD.getPiece(1,5).getPlayer();
		c3 = BOARD.getPiece(2,10).getPlayer();
		c4 = BOARD.getPiece(3,15).getPlayer();
		if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
			
			return true;
		}
		
		
		c1 = BOARD.getPiece(3,0).getPlayer();
		c2 = BOARD.getPiece(2,5).getPlayer();
		c3 = BOARD.getPiece(1,10).getPlayer();
		c4 = BOARD.getPiece(0,15).getPlayer();
		if (c1 == Player && c2 == Player && c3 == Player && c4 == Player){
			
			return true;
		}
		
		
		return false;
	}
}

	
}