using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {

/**
 * Esta classe extende a classe básica de jogos({@link Game}) de forma a
 * gerenciar um jogo de damas.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 * @see CheckersPiece
 */

/* REGRAS UTILIZADAS NA IMPLEMENTAÇÃO:
 * A pedra movimenta-se em diagonal, sobre as casas escuras, para a frente, e uma casa de cada vez.
 * A pedra que atingir a oitava casa adversária, será promovida a "dama", única peça que poderá andar para frente e para trás e comer para frente e para trás; porém apenas UMA casa de cada vez (assim como a peça normal).
 * A tomada NÃO é obrigatória.
 * A peça que toma poderá tomar mais de uma peça num mesmo movimento ou parar quando após a primeira tomada.
 * Um jogador ganha o jogo quando tomar (comer) todas as peças do adversário ou quando o adversário não tiver mais nenhum movimento.
 * Não foram estabelecidos limites para repetições de uma mesma jogada ou de um conjunto de jogadas.
 */

public class CheckersGame : Game {
public override String getClass2()
{
return "CheckersGame";
}
new public static String getClass()
{
return "CheckersGame";
}
	private int PLAYER1 = -1;
	private int PLAYER2 = -1;
	bool EAT_MOVE = false;
	private Piece EAT_PIECE = new NullPiece();

	public CheckersGame(String name) : base(name){

		GAME_TYPE = "checkers";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
	}
	
	public CheckersGame(String name, int player1) : base(name, player1) {

		GAME_TYPE = "checkers";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
		PLAYER1 = player1;
	}

	public override void startGame(){
		Console.WriteLine("# of Players:" + PLAYERS.Count);
		if (PLAYERS.Count == 2){
			PLAYER1 = (int)PLAYERS[0];
			PLAYER2 = (int)PLAYERS[1];
			ACTUAL_PLAYER = PLAYER1;
			WAITING_PLAYERS=false;
			GAME_ENDED = false;
			insertPieces();
		}else{
			Console.Error.WriteLine("Wrong number of players:" + PLAYERS.Count);
		}
	}
	

	
	public override bool endTurn(int Player){
		if (EAT_MOVE && Player == ACTUAL_PLAYER){
			EAT_MOVE = false;
			if (Player == PLAYER1){
				ACTUAL_PLAYER = PLAYER2;
			}else{
				ACTUAL_PLAYER = PLAYER1;
			}		
		CONT_ID++;
		return true;
		}
		return false;
	}
	
	
		
	public bool isPossibleToKeepEating (int x, int y, int Player){
		if (canEatSomething(x,y,Player)){
			return true;
		}
		return false;
	}

	public bool canEatSomething (int x, int y, int Player){
		if (canMove(x,y,x+2,y+2,Player)) return true;
		if (canMove(x,y,x+2,y-2,Player)) return true;
		if (canMove(x,y,x-2,y+2,Player)) return true;
		if (canMove(x,y,x-2,y-2,Player)) return true;
		return false;
	}

	
	public override bool move(int x1, int y1, int x2, int y2, int Player){
		if (canMove(x1, y1, x2, y2, Player)){
			BOARD.movePiece(x1,y1,x2,y2);
			if ((Player == PLAYER1) && (x2==7)){
				((CheckersPiece)BOARD.getPiece(x2,y2)).TYPE = CheckersPiece.CHECKER;
			}else if((Player == PLAYER2) && (x2==0)){
				((CheckersPiece)BOARD.getPiece(x2,y2)).TYPE = CheckersPiece.CHECKER;
			}
			
			if (Math.Abs(x2-x1) == 2 && Math.Abs(y2-y1)==2){
				int x_adversary = x1+(x2-x1)/2;
				int y_adversary = y1+(y2-y1)/2;
				eat(x_adversary,y_adversary);
				 if (isPossibleToKeepEating(x2,y2,Player)){
					 EAT_MOVE=true;
					 EAT_PIECE = BOARD.getPiece(x2,y2);
					 CONT_ID++;
					if (gameEnded()){
						Console.WriteLine("End game. Winner: " + WINNER);
					}
					 return true;
				 }
			}
			EAT_MOVE=false;
			if (Player == PLAYER1){
				ACTUAL_PLAYER = PLAYER2;
			 }else{
				ACTUAL_PLAYER = PLAYER1;
			  }
			  CONT_ID++;
				if (gameEnded()){
					Console.WriteLine("End game. Winner: " + WINNER);
				}
			  return true;
		}
		return false;
	}

	public override bool canMove(int x1, int y1, int x2, int y2, int Player){
		
		if (GAME_ENDED || WAITING_PLAYERS){
			return false;
		}
		if (Player != ACTUAL_PLAYER){
			return false;
		}
		
		if (!BOARD.validCell(x1,y1) || !BOARD.validCell(x2,y2)){
			return false;
		}
		
		if (BOARD.getPiece(x1,y1).getClass2() == NullPiece.getClass()){
			return false;
		}
		if (BOARD.getPiece(x2,y2).getClass2() != NullPiece.getClass()){
			return false;
		}

		if (BOARD.getPiece(x1,y1).getClass2() == NullPiece.getClass()){
			return false;
		}
				
		CheckersPiece P1 = (CheckersPiece)BOARD.getPiece(x1,y1); 
		if (P1.getPlayer() != Player){
			Console.WriteLine("player:" + P1.getPlayer());
			return false;
		}
		
		
		
		if ((Math.Abs(x2-x1) == 1 && Math.Abs(y2-y1)==1) && !EAT_MOVE){
			if (P1.TYPE == CheckersPiece.CHECKER){
				return true;
			}else if (P1.TYPE == CheckersPiece.SIMPLE){
				if (Player == PLAYER1){
					if (x2>x1){
						return true;
					}
				}else if (Player == PLAYER2){
					if (x2<x1){
						return true;
					}			
				}else{
					Console.WriteLine("Invalid player name: " + Player);
				}
			}else{
				Console.WriteLine("Invalid piece type: " + P1.TYPE);
			}
		}else{
			
			
			if ((Math.Abs(x2-x1) == 2 && Math.Abs(y2-y1)==2)&&(!EAT_MOVE || EAT_PIECE == BOARD.getPiece(x1,y1))){
				int x_adversary = x1+(x2-x1)/2;
				int y_adversary = y1+(y2-y1)/2;
				if (isAdversary((Piece)BOARD.getPiece(x_adversary,y_adversary),Player)){
					if (P1.TYPE == CheckersPiece.CHECKER){
						if (canEat(x_adversary,y_adversary)){
							return true;
						}
					}else if (P1.TYPE == CheckersPiece.SIMPLE){
						if (Player == PLAYER1){
							if (x2>x1){
								if (canEat(x_adversary,y_adversary)){
									return true;
								}
							}
						}else if (Player == PLAYER2){
							if (x2<x1){
								if (canEat(x_adversary,y_adversary)){
									return true;
								}
							}			
						}else{
							Console.WriteLine("Invalid player name: " + Player);
						}
					}else{
						Console.WriteLine("Invalid piece type: " + P1.TYPE);
					}
				}
			}
		}
		return false;
	}

	public bool isAdversary(Piece P1, int Player){
		int temp_player = P1.getPlayer();
		if ((temp_player == PLAYER1) || (temp_player == PLAYER2)){
			if (((Player == PLAYER1) || (Player == PLAYER2)) && temp_player != Player){
				return true;
			}
		}
		return false;
	}

	public bool eat(int x, int y){
		if (BOARD.getPiece(x,y).getClass2() == CheckersPiece.getClass()){
			if (BOARD.removePiece(x,y)){
				if (gameEnded()){
					Console.WriteLine("End game. Winner: " + WINNER);
				}
				return true;
			}
		}
		return false;
	}

	public bool canEat(int x, int y){
		if (BOARD.getPiece(x,y).getClass2() == CheckersPiece.getClass()){
			return true;
		}
		return false;
	}

	
	public bool canAdd(int x, int y, Piece P){
		return false;
	}
	
	public bool add(int x, int y, String Player){
		return false;
	}
	
	public override void initializeBoard(){
		BOARD = new Board(8,8);
	}

	public void insertPieces(){
		for (int cont=0;cont<4;cont++){
			BOARD.addPiece(0, cont*2,new CheckersPiece("x",PLAYER1,CheckersPiece.SIMPLE));
			BOARD.addPiece(1, 1+cont*2,new CheckersPiece("x",PLAYER1,CheckersPiece.SIMPLE));
			BOARD.addPiece(2, cont*2,new CheckersPiece("x",PLAYER1,CheckersPiece.SIMPLE));
			
			BOARD.addPiece(5, 1+cont*2,new CheckersPiece("o",PLAYER2,CheckersPiece.SIMPLE));
			BOARD.addPiece(6, cont*2,new CheckersPiece("o",PLAYER2,CheckersPiece.SIMPLE));
			BOARD.addPiece(7, 1+cont*2,new CheckersPiece("o",PLAYER2,CheckersPiece.SIMPLE));
		}
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
		int cont_X = BOARD.contPieces(PLAYER1);
		int cont_O = BOARD.contPieces(PLAYER2);
		if (PLAYER1 == Player && cont_O == 0 && cont_X > 0){
			return true;
		}else if (PLAYER2 == Player && cont_X == 0 && cont_O > 0){
			return true;
		}
		if (PLAYER1 == Player && ACTUAL_PLAYER == PLAYER2){
			if (cannotMove(PLAYER2)){
				WINNER = PLAYER1;
				return true;
			}
		} if (PLAYER2 == Player && ACTUAL_PLAYER == PLAYER1){
			if (cannotMove(PLAYER1)){
				WINNER = PLAYER2;
				return true;
			}					
		}
		return false;
	}
	
	private bool cannotMove(int Player){
		if (EAT_MOVE) return false;
		if (GAME_ENDED || WAITING_PLAYERS){
			return false;
		}
		if (Player != ACTUAL_PLAYER){
			return false;
		}
		
		for (int x1=0;x1<8;x1++){
			for (int y1=0;y1<8;y1++){
				if (BOARD.getPiece(x1,y1).getClass2() != NullPiece.getClass()){
					CheckersPiece P1 = (CheckersPiece)BOARD.getPiece(x1,y1); 
					if (P1.getPlayer() == Player){
						for (int dx=1;dx<=2;dx++){
							for (int posNegX=-1;posNegX<2;posNegX = posNegX+2){
								for (int posNegY=-1;posNegY<2;posNegY = posNegY+2){
									int destX = x1+dx*posNegX;
									int destY = x1+dx*posNegY;
									if (BOARD.validCell(destX,destY) && canMove(x1,y1,destX,destY,Player)) return false;
								}	
							}
						}
												
					}
				}
			}
		}
		return true;
	}
}
}