using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jogo_Main.Models
{
    public enum TipoMensagemRetorno
    {
        Acerto = 1,
        Erro = 2,
        Mensagem = 3,
        Inicio = 4,
        Montar = 6,
        Aguarde = 7,
        Atingido = 8,
        Destruido = 9,
        Fail = 10,
        Ganho = 11,
        Perdeu = 12,
        Finalizado = 13,
        MandoDeJogo = 14,
    }
}