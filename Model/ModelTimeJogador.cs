using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.Model
{   
    public class ModelTimeJogador
    {
        private int _Time = 0;
        private int _Jogador = 0;
        private ModelJogador _ObjModelJogador = new ModelJogador();
        public int Time { get => _Time; set => _Time = value; }
        public int Jogador { get => _Jogador; set => _Jogador = value; }
        public ModelJogador ObjModelJogador { get => _ObjModelJogador; set => _ObjModelJogador = value; }
    }
}
