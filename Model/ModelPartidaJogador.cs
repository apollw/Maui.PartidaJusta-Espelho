using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.Model
{
    public class ModelPartidaJogador
    {
        private int _Partida = 0;
        private int _Jogador = 0;
        private int _IsTimeCasa = 0;
        private ModelJogador _ObjModelJogador = new ModelJogador();
        public int Partida { get => _Partida; set => _Partida = value; }
        public int Jogador { get => _Jogador; set => _Jogador = value; }
        public int IsTimeCasa { get => _IsTimeCasa; set => _IsTimeCasa = value; }
        public ModelJogador ObjModelJogador { get => _ObjModelJogador; set => _ObjModelJogador = value; }
    }
}