using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {


/**
 * Esta classe estende a classe genérica jogos {@link Game} para gerenciar um
 * jogo de Xadrez.
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 * @see ChessPiece
 */

/*
 * Não foram estabelecidos limites para repetições de uma mesma jogada ou de um conjunto de jogadas.
 * A promoção de peão está ocorrendo de forma automática para rainha.
 */
public class ChessGame : Game {
public override String getClass2()
{
return "ChessGame";
}
new public static String getClass()
{
return "ChessGame";
}
	
	private int PLAYER1 = -2;
	private int PLAYER2 = -2;
	public bool CHECK = false;
	public bool WAITING_FOR_CHOICE = false;
	public Action LAST_ACTION = new Action(Action.NULL_ACTION);
	
	
	public bool LEFT_ROOK_PLAYER1_MOVED = false;
	public bool RIGHT_ROOK_PLAYER1_MOVED = false;
	public bool KING_PLAYER1_MOVED = false;
	
	public bool LEFT_ROOK_PLAYER2_MOVED = false;
	public bool RIGHT_ROOK_PLAYER2_MOVED = false;
	public bool KING_PLAYER2_MOVED = false;
	
	public ChessGame(String name) : base(name){

		GAME_TYPE = "chess";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
	}
	
	public ChessGame(String name, int player1) : base(name, player1) {

		GAME_TYPE = "chess";
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
	
	public virtual bool inCheck(int Player){
		
		Position KingPosition = findKing(Player);	
		int KingX = KingPosition.X;					
		int KingY = KingPosition.Y;
		int x2 = KingX;
		int y2 = KingY;
		for (int x=0;x<=8;x++){
			for (int y=0;y<=8;y++){
				if (BOARD.getPiece(x,y).getClass2().Equals(ChessPiece.getClass())){ 
					if (((ChessPiece)BOARD.getPiece(x,y)).PLAYER != Player){
						ChessPiece P1 = (ChessPiece)BOARD.getPiece(x,y);
						int x1 = x;
						int y1 = y;
						
						if (P1.get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
							if (Player == PLAYER1){
								if((x2-x1==-1) && Math.Abs(y2-y1)==1){
									return true;
								}
							}else if (Player == PLAYER2){
								if((x2-x1==1) && Math.Abs(y2-y1)==1){
									return true;
								}
							}	
						}else if (P1.get_type().Equals(ChessPiece.KNIGHT,StringComparison.CurrentCultureIgnoreCase)){
							if (((Math.Abs(x2-x1)==1) && (Math.Abs(y2-y1)==2)) || ((Math.Abs(x2-x1)==2) && (Math.Abs(y2-y1)==1))){
								return true;
							}
						}else if (P1.get_type().Equals(ChessPiece.BISHOP,StringComparison.CurrentCultureIgnoreCase)){
							if (Math.Abs(x2-x1)== Math.Abs(y2-y1) && y2 != y1){
								if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
									return true;
								}
							}
						}else if (P1.get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){
							if ((x2-x1 == 0 && Math.Abs(y2-y1)>0) || (y2-y1 == 0 && Math.Abs(x2-x1)>0)){
								if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
									return true;
								}
							}
						}else if (P1.get_type().Equals(ChessPiece.QUEEN,StringComparison.CurrentCultureIgnoreCase)){
							if ((x2-x1 == 0 && Math.Abs(y2-y1)>0) || (y2-y1 == 0 && Math.Abs(x2-x1)>0)){
								if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
									return true;
								}
							}else if (Math.Abs(x2-x1)== Math.Abs(y2-y1) && y2 != y1){
								if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
									return true;
								}
							}
						}else if (P1.get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase)){
							if ((Math.Abs(y2-y1)<=1) && (Math.Abs(x2-x1)<=1) && (Math.Abs(y2-y1) + Math.Abs(x2-x1) >= 1)){
									return true;
							}
						}

						
						
					}
				}
			}
		}
		return false;
	}
	
	public Position findKing(int Player){
		
		for (int x=0;x<=8;x++){
			for (int y=0;y<=8;y++){
				if (BOARD.getPiece(x,y).getClass2() != NullPiece.getClass()){
					if ((((ChessPiece)BOARD.getPiece(x,y)).get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase)) && ((ChessPiece)BOARD.getPiece(x,y)).PLAYER == Player){
						return new Position(x,y);
					}
				}
			}
		}
		return new Position(-1,-1); 
	}
	
	public override bool move(int x1, int y1, int x2, int y2, int Player){
		bool eat = false;
		Piece eaten_piece = BOARD.getPiece(x2,y2);
		bool CheckMate = false;
		if (canMove(x1, y1, x2, y2, Player)){
			int x3 = x2;
			int y3 = y2;
			if ((y1 != y2) && eaten_piece.getClass2() == NullPiece.getClass() && ((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
				eaten_piece = BOARD.getPiece(x1,y2);
				BOARD.removePiece(x1,y2);
				
				x3 = x1;
				y3 = y2;
			}
			if (eaten_piece.getClass2() != NullPiece.getClass()){
				eat = true;
			}
			
			
			
			if ((x1==0 && y1 == 0) && (!LEFT_ROOK_PLAYER2_MOVED)){
				LEFT_ROOK_PLAYER2_MOVED = true;
			}else if ((x1==0 && y1 == 7) && (!RIGHT_ROOK_PLAYER2_MOVED)){
				RIGHT_ROOK_PLAYER2_MOVED = true;
			}else if ((x1==7 && y1 == 0) && (!LEFT_ROOK_PLAYER1_MOVED)){
				LEFT_ROOK_PLAYER1_MOVED = true;
			}else if ((x1==7 && y1 == 7) && (!RIGHT_ROOK_PLAYER1_MOVED)){
				RIGHT_ROOK_PLAYER1_MOVED = true;
			}
			
			if (((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase)){
				if (Player == PLAYER1){
					KING_PLAYER1_MOVED = true;
				}else{
					KING_PLAYER2_MOVED = true;
				}
			}
			
			
			
			bool change_pawn = false;
			if (((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
				if (x2==0 || x2==7){
					change_pawn = true;
				}
			}
			
			
			if (x1==7 && y1 == 4 && ((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase) && Math.Abs(y2-y1)>1){
				
				if (y2==2){
					BOARD.movePiece(7,0,7,3);
				}else{ 
					BOARD.movePiece(7,7,7,5);
				}
			}else if (x1==0 && y1 == 4 && ((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase) && Math.Abs(y2-y1)>1){
				if (y2==2){ 
					BOARD.movePiece(0,0,0,3);
				}else{ 
					BOARD.movePiece(0,7,0,5);
				}
			}

			BOARD.movePiece(x1,y1,x2,y2); 
			
			
			if (change_pawn){
				if (x2==0){
					BOARD.addPiece(x2, y2,new ChessPiece("X",PLAYER1,ChessPiece.QUEEN));
				}else{
					BOARD.addPiece(x2, y2,new ChessPiece("O",PLAYER2,ChessPiece.QUEEN));
				}
			}
			
			String CheckTag = "";
			if (Player == PLAYER1 && inCheck(PLAYER2)){
				CheckTag = "<CHECK MESSAGE>PLAYER2 IN CHECK</CHECK MESSAGE>";
			}else if (Player == PLAYER2 && inCheck(PLAYER1)){
				CheckTag = "<CHECK MESSAGE>PLAYER1 IN CHECK</CHECK MESSAGE>";
			}
			
			
			if (eat){
				LAST_ACTION = new MoveAndEatAction(x1,y1,x2,y2,BOARD.getPiece(x2,y2),eaten_piece,x3,y3);
			}else{
				LAST_ACTION = new MoveAction(x1,y1,x2,y2,BOARD.getPiece(x2,y2));
			}

			
			if (ACTUAL_PLAYER == PLAYER1){
				ACTUAL_PLAYER = PLAYER2;
			}else{
				ACTUAL_PLAYER = PLAYER1;
			}
			
			CheckMate = inCheckMate(ACTUAL_PLAYER);
			if (CheckMate){
				if (ACTUAL_PLAYER==PLAYER1){
					CheckTag = "<CHECK MESSAGE>PLAYER1 IN CHECKMATE</CHECK MESSAGE>";
					WINNER = PLAYER2;
				}else{
					CheckTag = "<CHECK MESSAGE>PLAYER2 IN CHECKMATE</CHECK MESSAGE>";
					WINNER = PLAYER1;
				}
				GAME_ENDED = true;
			}
			LAST_MESSAGE = "<LAST ACTION>"+LAST_ACTION.action2String()+"</LAST ACTION>"+CheckTag;
			if (CheckMate){
				Console.WriteLine("   CheckMate: " + CheckMate);
			}
			return true;
		}
		return false;
	}
	
	
	private bool inCheckMate(int Player) {
		
		Position p = findKing(Player);
		if (!(inCheck(Player))){
			return false;
		}
		int KingX = p.X;
		int KingY = p.Y;
		if (canMoveAnywhere(KingX,KingY,ChessPiece.KING)){ 
			return false;
		}
		
		for (int cx=0;cx<=8;cx++){
			for (int cy=0;cy<=8;cy++){
				if (BOARD.getPiece(cx,cy).getClass2().Equals(ChessPiece.getClass())){
					ChessPiece P1 = (ChessPiece)BOARD.getPiece(cx,cy);
					if ((P1.PLAYER == Player && !P1.get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase))){
						if (canMoveAnywhere(cx,cy,P1.get_type())){
							return false;
						}
					}
				}
			}
		}
		return true;
	}

	
	private bool canMoveAnywhere(int x, int y, String pieceType) {
		Console.WriteLine("canMoveAnywhere: (" + x +","+y + ")" + pieceType );
		if (pieceType.Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-1;cx<=1;cx++){
				for (int cy=-1;cy<=1;cy++){
					if (cx != 0 || cy != 0){
						
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   King can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}
					}
				}
			}
		}else if (pieceType.Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-2;cx<=2;cx++){
				for (int cy=-1;cy<=1;cy++){
					if (cx != 0 || cy != 0){
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   Pawn can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}
					}
				}
			}
		}else if (pieceType.Equals(ChessPiece.KNIGHT,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-2;cx<=2;cx++){
				for (int cy=-2;cy<=2;cy++){
					if (Math.Abs(cx)+Math.Abs(cy) == 3){
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   Knight can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}
					}
				}
			}
		}else if (pieceType.Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-8;cx<=8;cx++){
				for (int cy=-8;cy<=8;cy++){
					if ((cx==0 || cy==0) && !(cx==0 && cy==0)){
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   Rook can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}
					}
				}
			}
		}else if (pieceType.Equals(ChessPiece.BISHOP,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-8;cx<=8;cx++){
				if (canMove(x,y,x+cx,y+cx,ACTUAL_PLAYER)){
					Console.WriteLine("   Bishop can move to ("+(x+cx)+","+(y+cx)+")");
					return true;
				}
				int cy = -cx;
				if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
					Console.WriteLine("   Bishop can move to ("+(x+cx)+","+(y+cy)+")");
					return true;
				}
			}
		}else if (pieceType.Equals(ChessPiece.QUEEN,StringComparison.CurrentCultureIgnoreCase)){
			for (int cx=-8;cx<=8;cx++){
				for (int cy=-8;cy<=8;cy++){
					if ((cx==0 || cy==0) && !(cx==0 && cy==0)){
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   Queen can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}
					}
					if (cx!=0 && Math.Abs(cx)==Math.Abs(cy)){
						if (canMove(x,y,x+cx,y+cy,ACTUAL_PLAYER)){
							Console.WriteLine("   Queen can move to ("+(x+cx)+","+(y+cy)+")");
							return true;
						}	
					}
				}
			}
		}
		return false;
	}

	
	public override bool canMove(int x1, int y1, int x2, int y2, int Player){
		if (canMoveAux(x1,y1,x2,y2,Player)){
			
			Piece eaten_piece = BOARD.getPiece(x2,y2);
			Board BOARD_BACKUP = BOARD.backup();
			if (canMoveAux(x1, y1, x2, y2, Player)){
				
				
				
				
				if ((y1 != y2) && eaten_piece.getClass2() == NullPiece.getClass() && ((ChessPiece)BOARD.getPiece(x1,y1)).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
					eaten_piece = BOARD.getPiece(x1,y2);
					BOARD.removePiece(x1,y2);
					
					
					
				}
				BOARD.movePiece(x1,y1,x2,y2);
				
				if (inCheck(Player)){
					BOARD = BOARD_BACKUP;
					
					return false;			
				}else{
					BOARD = BOARD_BACKUP;
					return true;
				}	
			}
			BOARD = BOARD_BACKUP;
		}
		return false;
		
		
	}
	
	
	public bool canMoveAux(int x1, int y1, int x2, int y2, int Player){
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
		
		bool null_destination = true;
		if (BOARD.getPiece(x2,y2).getClass2() != NullPiece.getClass()){
			null_destination = false;
			if (((ChessPiece)BOARD.getPiece(x2,y2)).PLAYER == Player){
				return false;	
			}
		}

		
		
		ChessPiece P1 = (ChessPiece)BOARD.getPiece(x1,y1); 
		if (P1.getPlayer() != Player){
			Console.WriteLine("The piece belong to the other player: " + P1.getPlayer() + ", you are " + Player);
			return false;
		}
		
		
		if (P1.get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){
			if (Player == PLAYER1){
				if ((x2-x1==-1) && (y2==y1) && (null_destination)){
					return true;
				}else if ((x2-x1==-2) && (y2==y1) && (x1==6) && (null_destination)){
					return true;
				}else if((x2-x1==-1) && Math.Abs(y2-y1)==1){
					if (!(null_destination)){
						return true;
					}else if (BOARD.getPiece(x1,y2).getClass2() != NullPiece.getClass()){ 
						if (((ChessPiece)BOARD.getPiece(x1,y2)).PLAYER != Player && ((ChessPiece)BOARD.getPiece(x1,y2)).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){ 
							if ((LAST_ACTION.getClass2() == MoveAction.getClass())){
								if (((MoveAction)LAST_ACTION).TO_X == x1 && ((MoveAction)LAST_ACTION).TO_Y == y2 && ((ChessPiece)((MoveAction)LAST_ACTION).ACTOR).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase) && ((MoveAction)LAST_ACTION).FROM_X == x1-2){
									return true;
								}
							}	  
						}
					}					
				}
			}else if (Player == PLAYER2){
				if ((x2-x1==1) && (y2==y1) && (null_destination)){
					return true;
				}else if ((x2-x1==2) && (y2==y1) && (x1==1) && (null_destination)){
					return true;
				}else if((x2-x1==1) && Math.Abs(y2-y1)==1){
					if (!(null_destination)){
						return true;
					}else if (BOARD.getPiece(x1,y2).getClass2() != NullPiece.getClass()){
						if (((ChessPiece)BOARD.getPiece(x1,y2)).PLAYER != Player && ((ChessPiece)BOARD.getPiece(x1,y2)).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)){ 
							if ((LAST_ACTION.getClass2() == MoveAction.getClass())){
								if (((MoveAction)LAST_ACTION).TO_X == x1 && ((MoveAction)LAST_ACTION).TO_Y == y2 && ((ChessPiece)((MoveAction)LAST_ACTION).ACTOR).get_type().Equals(ChessPiece.PAWN,StringComparison.CurrentCultureIgnoreCase)  && ((MoveAction)LAST_ACTION).FROM_X == x1+2){
									return true;   
								}
							}	  
						}
					}
					
				}

			}
			
		}else if (P1.get_type().Equals(ChessPiece.KNIGHT,StringComparison.CurrentCultureIgnoreCase)){
			if (((Math.Abs(x2-x1)==1) && (Math.Abs(y2-y1)==2)) || ((Math.Abs(x2-x1)==2) && (Math.Abs(y2-y1)==1))){
				return true;
			}
		}else if (P1.get_type().Equals(ChessPiece.BISHOP,StringComparison.CurrentCultureIgnoreCase)){
			if (Math.Abs(x2-x1)== Math.Abs(y2-y1) && y2 != y1){
				if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
					return true;
				}
			}
		}else if (P1.get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){
			if ((x2-x1 == 0 && Math.Abs(y2-y1)>0) || (y2-y1 == 0 && Math.Abs(x2-x1)>0)){
				if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
					return true;
				}
			}
		}else if (P1.get_type().Equals(ChessPiece.QUEEN,StringComparison.CurrentCultureIgnoreCase)){
			if ((x2-x1 == 0 && Math.Abs(y2-y1)>0) || (y2-y1 == 0 && Math.Abs(x2-x1)>0)){
				if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
					return true;
				}
			}else if (Math.Abs(x2-x1)== Math.Abs(y2-y1) && y2 != y1){
				if (!thereIsSomeoneInTheWay(x1,y1,x2,y2)){
					return true;
				}
			}
		}else if (P1.get_type().Equals(ChessPiece.KING,StringComparison.CurrentCultureIgnoreCase)){
			if ((Math.Abs(y2-y1)<=1) && (Math.Abs(x2-x1)<=1) && (Math.Abs(y2-y1) + Math.Abs(x2-x1) >= 1)){
					return true;
			}else{
				if (Player == PLAYER1){
					if (KING_PLAYER1_MOVED == false){
						
						if (x1==7 && y1==4 && x2 == 7 && y2 == 6){
							if (RIGHT_ROOK_PLAYER1_MOVED == false && BOARD.getPiece(7,5).getClass2()==NullPiece.getClass() && BOARD.getPiece(7,6).getClass2()==NullPiece.getClass()){
								if (BOARD.getPiece(x1,7).getClass2().Equals(ChessPiece.getClass())){
									if (((ChessPiece)BOARD.getPiece(x1,7)).PLAYER == Player && ((ChessPiece)BOARD.getPiece(x1,7)).get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){ 
										Board BACK_BOARD = BOARD.backup();
										if (canMove(x1,y1,x2,5,Player)){
											BOARD.movePiece(x1,y1,x2,y2);
											BOARD.movePiece(x1,y1,x2,5);
											bool CanCastling = !(inCheck(Player));
											BOARD = BACK_BOARD;
											Console.WriteLine("Can roque!");
											return CanCastling;
										}
									}
								}
							}
						}else if (x1==7 && y1==4 && x2 == 7 && y2 == 2){
							
								if (LEFT_ROOK_PLAYER1_MOVED == false && BOARD.getPiece(7,1).getClass2()==NullPiece.getClass() && BOARD.getPiece(7,2).getClass2()==NullPiece.getClass() && BOARD.getPiece(7,3).getClass2()==NullPiece.getClass()){
									if (BOARD.getPiece(x1,0).getClass2().Equals(ChessPiece.getClass())){
										if (((ChessPiece)BOARD.getPiece(x1,0)).PLAYER == Player && ((ChessPiece)BOARD.getPiece(x1,7)).get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){ 
											Board BACK_BOARD = BOARD.backup();
											if (canMove(x1,y1,x2,3,Player)){
												BOARD.movePiece(x1,y1,x2,y2);
												BOARD.movePiece(x1,y1,x2,3);
												bool CanCastling = !(inCheck(Player));
												BOARD = BACK_BOARD;
												Console.WriteLine("Can roque!");
												return CanCastling;
											}
										}
									}
								}
						}
					}
				}

				
				if (Player == PLAYER2){
					if (KING_PLAYER2_MOVED == false){
						
						if (x1==0 && y1==4 && x2 == 0 && y2 == 6){
							if (RIGHT_ROOK_PLAYER2_MOVED == false && BOARD.getPiece(0,5).getClass2()==NullPiece.getClass() && BOARD.getPiece(0,6).getClass2()==NullPiece.getClass()){
								if (BOARD.getPiece(x1,0).getClass2().Equals(ChessPiece.getClass())){
									if (((ChessPiece)BOARD.getPiece(x1,0)).PLAYER == Player && ((ChessPiece)BOARD.getPiece(x1,0)).get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){ 
										Board BACK_BOARD = BOARD.backup();
										if (canMove(x1,y1,x2,5,Player)){
											BOARD.movePiece(x1,y1,x2,y2);
											BOARD.movePiece(x1,y1,x2,5);
											bool CanCastling = !(inCheck(Player));
											BOARD = BACK_BOARD;
											Console.WriteLine("Can roque!");
											return CanCastling;
										}
									}
								}
							}
						}else if (x1==0 && y1==4 && x2 == 0 && y2 == 2){
							
								if (LEFT_ROOK_PLAYER1_MOVED == false && BOARD.getPiece(0,1).getClass2()==NullPiece.getClass() && BOARD.getPiece(0,2).getClass2()==NullPiece.getClass() && BOARD.getPiece(0,3).getClass2()==NullPiece.getClass()){
									if (BOARD.getPiece(x1,0).getClass2().Equals(ChessPiece.getClass())){
										if (((ChessPiece)BOARD.getPiece(x1,0)).PLAYER == Player && ((ChessPiece)BOARD.getPiece(x1,0)).get_type().Equals(ChessPiece.ROOK,StringComparison.CurrentCultureIgnoreCase)){ 
											Board BACK_BOARD = BOARD.backup();
											if (canMove(x1,y1,x2,3,Player)){
												BOARD.movePiece(x1,y1,x2,y2);
												BOARD.movePiece(x1,y1,x2,3);
												bool CanCastling = !(inCheck(Player));
												BOARD = BACK_BOARD;
												Console.WriteLine("Can roque!");
												return CanCastling;
											}
										}
									}
								}
							}
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
		if (BOARD.getPiece(x,y).getClass2() == ChessPiece.getClass()){
			if (BOARD.removePiece(x,y)){
				if (gameEnded()){
					Console.WriteLine("End game. Winner: " + WINNER);
				}
				return true;
			}
		}
		return false;
	}

	public bool canEat(int x, int y, int x2, int y2, int Player){
		if (BOARD.validCell(x2,y2)){
		  if (BOARD.getPiece(x2,y2).getClass2() == ChessPiece.getClass()){
			  return canMove(x,y,x2,y2,Player);
		  }
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
		for (int cont=0;cont<8;cont++){
			BOARD.addPiece(1, cont,new ChessPiece("O",PLAYER2,ChessPiece.PAWN));		
			BOARD.addPiece(6, cont,new ChessPiece("X",PLAYER1,ChessPiece.PAWN));
		}
		for (int cont=0;cont<2;cont++){
			BOARD.addPiece(0, Math.Abs(7*cont),new ChessPiece("O",PLAYER2,ChessPiece.ROOK));
			BOARD.addPiece(7, Math.Abs(7*cont),new ChessPiece("X",PLAYER1,ChessPiece.ROOK));
			
			BOARD.addPiece(0, Math.Abs(7*cont-1),new ChessPiece("O",PLAYER2,ChessPiece.KNIGHT));
			BOARD.addPiece(7, Math.Abs(7*cont-1),new ChessPiece("X",PLAYER1,ChessPiece.KNIGHT));
			
			BOARD.addPiece(0, Math.Abs(7*cont-2),new ChessPiece("O",PLAYER2,ChessPiece.BISHOP));
			BOARD.addPiece(7, Math.Abs(7*cont-2),new ChessPiece("X",PLAYER1,ChessPiece.BISHOP));	
		}
		
		BOARD.addPiece(0, 3,new ChessPiece("O",PLAYER2,ChessPiece.QUEEN));
		BOARD.addPiece(0, 4,new ChessPiece("O",PLAYER2,ChessPiece.KING));

		BOARD.addPiece(7, 3,new ChessPiece("X",PLAYER1,ChessPiece.QUEEN));
		BOARD.addPiece(7, 4,new ChessPiece("X",PLAYER1,ChessPiece.KING));
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
		if (WINNER == Player)
			return true;
		return false;
	}
	
	public bool thereIsSomeoneInTheWay(int x1, int y1,int x2, int y2){
		int add_x = 1;
		int add_y = 1;
		if (x2-x1 < 0){
			add_x = -1;
		}else if (x2==x1){
			add_x = 0;
		}
		if (y2-y1 < 0){
			add_y = -1;
		}else if (y2==y1){
			add_y = 0;
		}
		x1 += add_x;
		y1 += add_y;
		int cont = 0; 
		while (x1 != x2 || y1 != y2 && cont<10){
			cont++;
			
			if (BOARD.validCell(x1,y1)){
				if ((BOARD.getPiece(x1,y1).getClass2() != NullPiece.getClass())){
						return true;
				}
			}
			x1 += add_x;
			y1 += add_y;
		}
		return false;
	}
	
}
}