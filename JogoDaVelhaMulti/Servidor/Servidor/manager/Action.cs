using Servidor.manager;
using System.Collections;
using System;

namespace Servidor.manager {


/**
 * Esta classe é a super-classe para possibilitar o log das diversas ações
 * que podem ser feitas por um jogador. A ação básica, representada por esta
 * classe, é a ação nula "null". Além disso, ela contém as constantes
 * utilizadas por todas as ações (sub-classes de Action).
 * 
 * @author Luciano Antonio Digiampietri
 * @version 1.0 beta - 03/11/2008
 * @see AddAction
 * @see MoveAction
 * @see MoveAndEatAction
 * @see RotateAction
 */
public class Action {
public virtual String getClass2()
{
return "Action";
}
public static String getClass()
{
return "Action";
}
	public const String NULL_ACTION = "null";
	public const String MOVE = "move";
	public const String MOVE_AND_EAT = "move and eat";
	public const String ADD = "add";
	public const String ROTATE = "rotate";
	public const String PASS = "pass";
	
	public String TYPE = NULL_ACTION;
	
	public Action (String type){
		TYPE = type;
	}
	
	public virtual String action2String(){
		return "null action";
	}
}
}