using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.Model
{    public class ModelJogador
    {
        private int _Id = 0;
        private string _Nome = string.Empty;
        private string _Telefone = string.Empty;
        private int _Nota = 0;
        private int _Posicao = 0;
        private int _Status = 0;

        public int Id { get => _Id; set => _Id = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        public string Telefone { get => _Telefone; set => _Telefone = value; }
        public int Nota { get => _Nota; set => _Nota = value; }
        public int Posicao { get => _Posicao; set => _Posicao = value; }
        public int Status { get => _Status; set => _Status = value; }
    }
}
