using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.Model
{
    public class ModelPartida
    {
        private int _Id = 0;
        private int _TimeCasa = 0;
        private int _TimeVisitante = 0;
        private DateTime _Data = DateTime.Now;
        private int _TimeVencedor = 0;
        private int _TimeCasaGols = 0;
        private int _TimeVisitanteGols = 0;
        private List<ModelPartidaJogador> _ListaPartidaJogador = new List<ModelPartidaJogador>();
        public int Id { get => _Id; set => _Id = value; }
        public int TimeCasa { get => _TimeCasa; set => _TimeCasa = value; }
        public int TimeVisitante { get => _TimeVisitante; set => _TimeVisitante = value; }
        public DateTime Data { get => _Data; set => _Data = value; }
        public int TimeVencedor { get => _TimeVencedor; set => _TimeVencedor = value; }
        public int TimeCasaGols { get => _TimeCasaGols; set => _TimeCasaGols = value; }
        public int TimeVisitanteGols { get => _TimeVisitanteGols; set => _TimeVisitanteGols = value; }
        public List<ModelPartidaJogador> ListaJogador { get => _ListaPartidaJogador; set => _ListaPartidaJogador = value; }
        public List<ModelJogador> GetListaJogadores()
        {
            List<ModelJogador> tmpList = new List<ModelJogador>();
                foreach (ModelPartidaJogador element in _ListaPartidaJogador)
                    tmpList.Add(element.ObjModelJogador);
                return tmpList;
        }
    }
}