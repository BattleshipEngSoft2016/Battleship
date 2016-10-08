using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe estende o jogo de tabuleiro genérico {@link Game} para permitir
 * o gerenciamento de um jogo da velha (tabuleiro 3 x 3).
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 * @see TictactoePiece
 * @see TictactoeGame
 */
public class TictactoeGame : Game {
public override String getClass2()
{
return "TictactoeGame";
}
new public static String getClass()
{
return "TictactoeGame";
}
	int PLAYER1 = -1;
	int PLAYER2 = -1;	

	public TictactoeGame(String name) : base(name) {

		GAME_TYPE = "tictactoe";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
		GAME_ENDED = false;
	}
	
	public TictactoeGame(String name, int player1) : base(name, player1) {

		GAME_TYPE = "tictactoe";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
		GAME_ENDED = false;
	}

	public override void startGame(){
		Console.WriteLine("Starting tic tac toe game: # of Players:" + PLAYERS.Count);
		if (PLAYERS.Count >= 2){
			PLAYER1 = (int)PLAYERS[0];
			PLAYER2 = (int)PLAYERS[1];
			Console.WriteLine("Tic tac toe, players: " + PLAYER1 + " ; " + PLAYER2);
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
			P = new TictactoePiece("+",PLAYER1);
		} else if (Player == PLAYER2){
			P = new TictactoePiece("-",PLAYER2);
		} else {
			Console.WriteLine("Invalid player: '" + Player + "'. Players: '"+PLAYER1+"' and '"+PLAYER2+"'.");
			return false;
		}
		if (canAdd(x,y,(Piece)P)){
			if (BOARD.addPiece(x,y,(Piece)P)){
			}else{
				Console.WriteLine("BLASFEMIA!!!");
			}
			
			if ((BOARD.contPieces(new  TictactoePiece("",-1)) == 9) && WINNER == -1){
				DRAWN_GAME = true;
				GAME_ENDED = true;
				LAST_MESSAGE = "DRAWN GAME";		
			}
			if (gameEnded()){
				if (DRAWN_GAME){
					Console.WriteLine("End game. "+LAST_MESSAGE);
				}else{
					Console.WriteLine("End game. Winner: " + WINNER);
				}
			}
			CONT_ID++;
			return true;
		}
		return false;
	}
	
	public override void initializeBoard(){
		BOARD = new Board(3,3);
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
		if (WINNER  != -1 && WINNER == PLAYER2){
			return true;
		}else if (won(PLAYER2)){
			WINNER = PLAYER2;
			return true;
		}
		return false;
	}	
	
	
	
	public bool won(int Player){		
		int c1, c2, c3;
		if (Player == -1){
			return false;
		}
		for (int cont=0;cont<3;cont++){
			
			c1 = BOARD.getPiece(0,cont).getPlayer();
			c2 = BOARD.getPiece(1,cont).getPlayer();
			c3 = BOARD.getPiece(2,cont).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player){
				return true;
			}
			
			
			c1 = BOARD.getPiece(cont,0).getPlayer();
			c2 = BOARD.getPiece(cont,1).getPlayer();
			c3 = BOARD.getPiece(cont,2).getPlayer();
			if (c1 == Player && c2 == Player && c3 == Player){
				return true;
			}
		}
		
		c1 = BOARD.getPiece(0,0).getPlayer();
		c2 = BOARD.getPiece(1,1).getPlayer();
		c3 = BOARD.getPiece(2,2).getPlayer();
		if (c1 == Player && c2 == Player && c3 == Player){
			return true;
		}
		
		
		c1 = BOARD.getPiece(0,2).getPlayer();
		c2 = BOARD.getPiece(1,1).getPlayer();
		c3 = BOARD.getPiece(2,0).getPlayer();
		if (c1 == Player && c2 == Player && c3 == Player){
			return true;
		}
		return false;
	}
}

	
}