using Servidor.netGameManager;
using System.Collections;
using System;

namespace Servidor.netGameManager
{

    /* CLASSE: AddressPort
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Esta classe serve para juntar dois atributos: address e port.
     * address: contém o endereço IP (por exemplo, do servidor de jogos 
     *          ou da aplicação cliente)
     * port: número da porta de comunicação (relacionada ao endereço)
     */

    public class AddressPort
    {
        public String getClass2()
        {
            return "AddressPort";
        }
        public static String getClass()
        {
            return "AddressPort";
        }
 
        public System.Net.IPAddress address;
        public int port;
        public AddressPort(System.Net.IPAddress a, int p)
        {
            address = a;
            port = p;
        }
    }
}