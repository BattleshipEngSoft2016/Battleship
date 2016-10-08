
using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {


/**
 * Esta classe estende a classe genérica de jogos ({@link Game}) para permitir
 * o gerenciamento do jogo deflexion (também conhecido como The World of Khet):
 * http://www.khet.com/
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * 
 * @see Game
 */
public class DeflexionGame : Game {
public override String getClass2()
{
return "DeflexionGame";
}
new public static String getClass()
{
return "DeflexionGame";
}
	private int PLAYER1 = -1; 
	private int PLAYER2 = -1; 
	bool CONFIRM_MOVE = false;
	public const String CLOCKWISE = "CLOCKWISE";
	public const String ANTICLOCKWISE = "ANTICLOCKWISE";
	public const String COUNTERCLOCKWISE = "COUNTERCLOCKWISE";
	
	public DeflexionGame(String name) : base(name){

		GAME_TYPE = "deflexion";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
	}
	
	public DeflexionGame(String name, int player1) : base(name, player1) {

		GAME_TYPE = "deflexion";
		MAX_PLAYERS = 2;
		MIN_PLAYERS = 2;
		PLAYER1 = player1;
	}

	public override void startGame(){
		if (PLAYERS.Count == 2){
			PLAYER1 = (int)PLAYERS[0];
			PLAYER2 = (int)PLAYERS[1];
			ACTUAL_PLAYER = PLAYER1;
			Console.WriteLine("Deflexion, players: " + PLAYER1 + " ; " + PLAYER2);
			WAITING_PLAYERS=false;
			GAME_ENDED = false;
			CONFIRM_MOVE = false;
			insertPieces();
		}else{
			Console.Error.WriteLine("Wrong number of players:" + PLAYERS.Count);
		}
	}
	

	public override bool endTurn(int Player){
		Console.WriteLine("Deflexion end turn1");
		if (CONFIRM_MOVE && Player == ACTUAL_PLAYER){
			CONFIRM_MOVE = false;
			disparLaser(Player);
			Console.WriteLine("Deflexion end turn2");
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
		
		
/*	public bool is_possible_to_keep_move (int x, int y, String Player){
		if (can_eat_something(x,y,Player)){
			return true;
		}
		return false;
	}
	*/

	/*
	public bool can_eat_something (int x, int y, String Player){
		if (can_move(x,y,x+2,y+2,Player)) return true;
		if (can_move(x,y,x+2,y-2,Player)) return true;
		if (can_move(x,y,x-2,y+2,Player)) return true;
		if (can_move(x,y,x-2,y-2,Player)) return true;
		return false;
	}*/

	
	public override bool move(int x1, int y1, int x2, int y2, int Player){
		
		if (canMove(x1, y1, x2, y2, Player)){
			BOARD.replacePiece(x1,y1,x2,y2);
			CONFIRM_MOVE=true;
			/*if (Player == Player1){
				Actual_player = Player2;
			 }else{
				Actual_player = Player1;
			  }
			  */
			  CONT_ID++;
			  endTurn(Player);
			  return true;
		}
		return false;
	}

	public override bool canMove(int x1, int y1, int x2, int y2, int Player){
		Console.WriteLine("Deflexion can move: " + x1 + " " + x2 + " " + y1 + " " + y2 + " " + Player +" " + PLAYER1 + " " + PLAYER2 + " " + GAME_ENDED);
		if (GAME_ENDED){
			return false;
		}
		
		if (CONFIRM_MOVE){
			return false;
		}

		if ((Math.Abs(x2-x1) > 1 || Math.Abs(y2-y1)>1)){
			return false;
		}
		
		if ((Math.Abs(x2-x1) == 0 && Math.Abs(y2-y1)==0)){
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
			Console.WriteLine("Moving deflexion2.1");
			if (!((DeflexionPiece)BOARD.getPiece(x1,y1)).get_type().Equals(DeflexionPiece.DJED,StringComparison.CurrentCultureIgnoreCase)){
				Console.WriteLine("Moving deflexion2.2");
				return false;
			}else{
				Console.WriteLine("Moving deflexion2.3");
				String temp_type = ((DeflexionPiece)BOARD.getPiece(x2,y2)).get_type();
				if ((temp_type.Equals(DeflexionPiece.DJED)) || (temp_type.Equals(DeflexionPiece.PHAROAH,StringComparison.CurrentCultureIgnoreCase))){
					Console.WriteLine("Moving deflexion4");
					return false;
				}
			}
		}
		
		if (BOARD.getPiece(x1,y1).getClass2() == NullPiece.getClass()){
			return false;
		}
		
		DeflexionPiece P1 = (DeflexionPiece)BOARD.getPiece(x1,y1); 
		Console.WriteLine(Player + " " + P1.getPlayer());
		if (P1.getPlayer() != Player){
			return false;
		}
		
		
		if ((y2==0) && (P1.getPlayer() == PLAYER2)){ 
			return false;
		}

		if ((y2==9) && (P1.getPlayer() == PLAYER1)){ 
			return false;
		}
				
		return true;
	}

	public bool canRotate(int x, int y, int Player){
		if (BOARD.validCell(x,y)){
			if (BOARD.getPiece(x,y).getClass2() == DeflexionPiece.getClass()){
				if (((DeflexionPiece)BOARD.getPiece(x,y)).get_type().Equals(DeflexionPiece.DJED,StringComparison.CurrentCultureIgnoreCase) || ((DeflexionPiece)BOARD.getPiece(x,y)).get_type().Equals(DeflexionPiece.PYRAMID,StringComparison.CurrentCultureIgnoreCase)){
					return true;
				}
			}
		}
		return false;
	}
	
	public override bool rotate(int x, int y, String direction, int Player){
		if (ACTUAL_PLAYER == Player){
			if (canRotate(x,y,Player)){
				DeflexionPiece P1 = ((DeflexionPiece)BOARD.getPiece(x,y));
				String ori = P1.get_orientation();
				if (direction.Equals(CLOCKWISE,StringComparison.CurrentCultureIgnoreCase)){
					if (ori.Equals("NE",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("SE");
					}else if (ori.Equals("SE",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("SW");
					}else if (ori.Equals("SW",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("NW");
					}else if (ori.Equals("NW",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("NE");
					}else{
						Console.WriteLine("Invalid orientation: " + ori);
					}
				}else if (direction.Equals(ANTICLOCKWISE,StringComparison.CurrentCultureIgnoreCase) || direction.Equals(COUNTERCLOCKWISE,StringComparison.CurrentCultureIgnoreCase)){
					if (ori.Equals("NE",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("NW");
					}else if (ori.Equals("SE",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("NE");
					}else if (ori.Equals("SW",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("SE");
					}else if (ori.Equals("NW",StringComparison.CurrentCultureIgnoreCase)){
						P1.update_orientation("SW");
					}else{
						Console.WriteLine("Invalid orientation: " + ori);
					}
				}else{
					Console.WriteLine("Invalid type of rotation.");
					return false;
				}
				Console.WriteLine("Deflexion rotation OK");
				BOARD.replacePiece(x,y,P1);
				CONFIRM_MOVE = true;
				endTurn(Player);
				return true;
			}
		}
		return false;
	}

	
	public bool burn(int x, int y){
		Console.WriteLine("Burning: " + x + "," + y);
		Console.WriteLine(BOARD.getPiece(x,y).getClass2());
		if (BOARD.getPiece(x,y).getClass2() == DeflexionPiece.getClass()){
			String deleted_type =((DeflexionPiece)BOARD.getPiece(x,y)).get_type();
			int deleted_player =((DeflexionPiece)BOARD.getPiece(x,y)).getPlayer();
			if (BOARD.removePiece(x,y)){
				if (deleted_type.Equals(DeflexionPiece.PHAROAH,StringComparison.CurrentCultureIgnoreCase)){
					GAME_ENDED = true;
					if (deleted_player == PLAYER1){
						WINNER = PLAYER2;
					}else if (deleted_player == PLAYER2){
						WINNER = PLAYER1; 
					}else{
						Console.WriteLine("Invalid player - burn end game");
					}
				
					Console.WriteLine("End game. Winner: " + WINNER);
				}
				CONT_ID++;
				return true;
			}
		}
		return false;
	}

	public bool disparLaser(int Player){
		int laser_x = 0;
		int laser_y = 0;
		String orientation = "S";
		if (Player == PLAYER2){
			laser_x = 7;
			laser_y = 9;
			orientation = "N";			
		}else if (Player == PLAYER1){
			laser_x = 0;
			laser_y = 0;
			orientation = "S";			
		}else{
			Console.WriteLine("Invalid player: '"+Player+"'");
			return false;
		}
		Console.WriteLine("Disparing laser:");
		LAST_MESSAGE = "<LASER>";
		auxDisparLaser(laser_x,laser_y,orientation);
		LAST_MESSAGE += "</LASER>";
		CONT_ID++;
		return true;
	}

	public void auxDisparLaser(int x, int y, String laser_orientation){
		if (BOARD.validCell(x,y)){
			Console.Write(x + "," +  y + " " + laser_orientation + " ");
			LAST_MESSAGE += x + "," +  y + " " + laser_orientation + ";";
			Piece P1 = BOARD.getPiece(x,y);
			if (P1.getClass2() == NullPiece.getClass()){
				if (laser_orientation.Equals("N",StringComparison.CurrentCultureIgnoreCase)){
					auxDisparLaser(x-1,y, laser_orientation);
				}else if (laser_orientation.Equals("S",StringComparison.CurrentCultureIgnoreCase)){
					auxDisparLaser(x+1,y, laser_orientation);
				}else if (laser_orientation.Equals("E",StringComparison.CurrentCultureIgnoreCase)){
					auxDisparLaser(x,y+1, laser_orientation);
				}else if (laser_orientation.Equals("W",StringComparison.CurrentCultureIgnoreCase)){
					auxDisparLaser(x,y-1, laser_orientation);
				}else  {
					Console.WriteLine("ERROR invalid laser orientation - aux_dispar_laser");
				}
			}else if (P1.getClass2() == DeflexionPiece.getClass()){
				String type = ((DeflexionPiece)P1).get_type();
				if ((type.Equals(DeflexionPiece.PHAROAH,StringComparison.CurrentCultureIgnoreCase)) || (type.Equals(DeflexionPiece.OBELISK,StringComparison.CurrentCultureIgnoreCase))){
					burn(x,y);
					LAST_MESSAGE += P1.getString();
				}else{
					String ori = ((DeflexionPiece)P1).get_orientation();
					
					String tag1 = "";
					String tag2 = "";
					if (laser_orientation.Equals("N",StringComparison.CurrentCultureIgnoreCase)){
						tag1 = "NE";
						tag2 = "NW";
					}else if (laser_orientation.Equals("S",StringComparison.CurrentCultureIgnoreCase)){
						tag1 = "SE";
						tag2 = "SW";
					}else if (laser_orientation.Equals("E",StringComparison.CurrentCultureIgnoreCase)){
						tag1 = "NE";
						tag2 = "SE";
					}else if (laser_orientation.Equals("W",StringComparison.CurrentCultureIgnoreCase)){
						tag1 = "SW";
						tag2 = "NW";
					}
						
					if (ori.Equals(tag1,StringComparison.CurrentCultureIgnoreCase) || ori.Equals(tag2,StringComparison.CurrentCultureIgnoreCase)){
							burn(x,y);
							LAST_MESSAGE += P1.getString();
					}else{
						if ((laser_orientation.Equals("N",StringComparison.CurrentCultureIgnoreCase)) || (laser_orientation.Equals("S",StringComparison.CurrentCultureIgnoreCase))){
							
							
							if (ori.Equals("NE",StringComparison.CurrentCultureIgnoreCase) || ori.Equals("SE",StringComparison.CurrentCultureIgnoreCase)){
								auxDisparLaser(x,y+1,"E");
							
							}else if (ori.Equals("NW",StringComparison.CurrentCultureIgnoreCase) || ori.Equals("SW",StringComparison.CurrentCultureIgnoreCase)){
								auxDisparLaser(x,y-1,"W");
							}else{
								Console.WriteLine("ERROR invalid deflexion: " + laser_orientation + " - " + ori +"- aux_dispar_laser");
							}
						}else if ((laser_orientation.Equals("E",StringComparison.CurrentCultureIgnoreCase)) || (laser_orientation.Equals("W",StringComparison.CurrentCultureIgnoreCase))){
							
							if (ori.Equals("NE",StringComparison.CurrentCultureIgnoreCase) || ori.Equals("NW",StringComparison.CurrentCultureIgnoreCase)){
								auxDisparLaser(x-1,y,"N");
							
							}else if (ori.Equals("SE",StringComparison.CurrentCultureIgnoreCase) || ori.Equals("SW",StringComparison.CurrentCultureIgnoreCase)){
								auxDisparLaser(x+1,y,"S");
							}else{
								Console.WriteLine("ERROR invalid deflexion2: " + laser_orientation + " - " + ori +"- aux_dispar_laser");
							}							
						}else{
							Console.WriteLine("ERROR invalid deflexion3: " + laser_orientation + " - " + ori +"- aux_dispar_laser");
						}
					}
				}
				
			}else{
				Console.WriteLine("ERROR invalid class piece type - aux_dispar_laser");
			}
		}else{
			Console.WriteLine("No piece burned.");
		}
	}
	
	public bool canAdd(int x, int y, Piece P){
		return false;
	}
	
	public bool add(int x, int y, String Player){
		return false;
	}
	
	public override void initializeBoard(){
		BOARD = new Board(8,10);
	}

	public void insertPieces(){
		for (int cont=0;cont<4;cont++){
			
			BOARD.addPiece(3, 0,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"NE"));
			BOARD.addPiece(4, 0,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"SE"));
			BOARD.addPiece(1, 2,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"SW"));
			BOARD.addPiece(0, 7,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"SE"));
			BOARD.addPiece(3, 7,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"SE"));
			BOARD.addPiece(4, 7,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"NE"));
			BOARD.addPiece(5, 6,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PYRAMID,"SE"));

			BOARD.addPiece(3, 4,new DeflexionPiece("X",PLAYER1,DeflexionPiece.DJED,"SW"));
			BOARD.addPiece(3, 5,new DeflexionPiece("X",PLAYER1,DeflexionPiece.DJED,"SE"));
			
			BOARD.addPiece(0, 4,new DeflexionPiece("X",PLAYER1,DeflexionPiece.OBELISK));
			BOARD.addPiece(0, 6,new DeflexionPiece("X",PLAYER1,DeflexionPiece.OBELISK));
			
			BOARD.addPiece(0, 5,new DeflexionPiece("X",PLAYER1,DeflexionPiece.PHAROAH));

						
			BOARD.addPiece(3, 2,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"SW"));
			BOARD.addPiece(4, 2,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"NW"));
			BOARD.addPiece(2, 3,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"NW"));
			BOARD.addPiece(3, 9,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"NW"));
			BOARD.addPiece(4, 9,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"SW"));
			BOARD.addPiece(6, 7,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"NE"));
			BOARD.addPiece(7, 2,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PYRAMID,"NW"));

			BOARD.addPiece(4, 4,new DeflexionPiece("O",PLAYER2,DeflexionPiece.DJED,"NW"));
			BOARD.addPiece(4, 5,new DeflexionPiece("O",PLAYER2,DeflexionPiece.DJED,"NE"));
			
			BOARD.addPiece(7, 3,new DeflexionPiece("O",PLAYER2,DeflexionPiece.OBELISK));
			BOARD.addPiece(7, 5,new DeflexionPiece("O",PLAYER2,DeflexionPiece.OBELISK));
			
			BOARD.addPiece(7, 4,new DeflexionPiece("O",PLAYER2,DeflexionPiece.PHAROAH));
			

			
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
	
	public bool player2Won(){
		if (WINNER  != -1 && WINNER == PLAYER1){
			return true;
		}
		return false;
	}
	
	public bool player1Won(){
		if (WINNER  != -1 && WINNER == PLAYER2){
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
		return false;
	}
	
}
}