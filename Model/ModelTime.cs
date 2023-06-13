using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.Model
{
    public class ModelTime
    {
        private int _Id = 0;
        private string _Nome = string.Empty;
        private int _Nivel = 0;
        private int _TotalJogadores = 0;
        private List<ModelJogador> _ListaJogador = new List<ModelJogador>();

        public int Id { get => _Id; set => _Id = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public int Nivel { get => _Nivel; set => _Nivel = value; }
        public int TotalJogadores { get => _TotalJogadores; set => _TotalJogadores = value; }
        public List<ModelJogador> ListaJogador { get => _ListaJogador; set => _ListaJogador = value; }
    }
}
