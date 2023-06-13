using Maui.PartidaJusta_Espelho.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.PartidaJusta_Espelho.ViewModel
{
    public class ViewModelTime : INotifyPropertyChanged
    {
        private ModelTime objTime;
        private ModelTime objTimeParaEditar;
        private ModelTime objTimeParaEditar2;
        private ModelJogador objJogador;
        private List<ModelJogador> listaParaTroca1;
        private List<ModelJogador> listaParaTroca2;
        private List<ModelJogador> listaGeralTroca;
        private bool criado;
        private List<ModelJogador> _jogadores;
        private List<ModelJogador> _jogadoresPresentes;
        private List<ModelTime> _times;
        private ObservableCollection<ModelTime> _listaCarregada;
        private int _tamanhoEquipe;
        private int _quantidadeTimes;

        #region
        public event PropertyChangedEventHandler PropertyChanged;
        //PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region
        //Declaração de Comandos Vinculados
        public ICommand CmdSortearTimes => new Command(SortearTimes);
        //public ICommand CmdSalvarJogador => new Command(SalvarJogador);
        #endregion

        //No construtor da VM, instanciamos um objeto de Jogador
        public ViewModelTime()
        {
            TamanhoEquipe = 2;
            Jogadores = new List<ModelJogador>();
            objTime = new ModelTime();
            listaParaTroca1 = new List<ModelJogador>();
            listaParaTroca2 = new List<ModelJogador>();
            //listaGeralTroca = new List<ModelJogador>(CarregarListaTroca());
            ListaCarregada = new ObservableCollection<ModelTime>(CarregarLista());
        }

        #region
        //Getters e Setters
        public ModelTime ObjTime { get => objTime; set => objTime = value; }
        public ModelTime ObjTimeParaEditar
        {
            get => objTimeParaEditar;
            set
            {
                objTimeParaEditar = value;
                OnPropertyChanged(nameof(ObjTimeParaEditar));
            }
        }
        public ModelTime ObjTimeParaEditar2
        {
            get => objTimeParaEditar2;
            set
            {
                objTimeParaEditar2 = value;
                OnPropertyChanged(nameof(ObjTimeParaEditar2));
            }
        }
        public ModelJogador ObjJogador
        {
            get => objJogador;
            set
            {
                objJogador = value;
                OnPropertyChanged(nameof(ObjJogador));
            }
        }
        public List<ModelJogador> ListaParaTroca1
        {
            get => listaParaTroca1;
            set
            {
                listaParaTroca1 = value;
                OnPropertyChanged(nameof(ListaParaTroca1));
            }
        }
        public List<ModelJogador> ListaParaTroca2
        {
            get => listaParaTroca2;
            set
            {
                listaParaTroca2 = value;
                OnPropertyChanged(nameof(ListaParaTroca2));
            }
        }
        public List<ModelJogador> ListaGeralTroca
        {
            get => listaGeralTroca;
            set
            {
                listaGeralTroca = value;
                OnPropertyChanged(nameof(ListaGeralTroca));
            }
        }
        public bool Criado { get; set; }
        public ObservableCollection<ModelTime> ListaCarregada
        {
            get => _listaCarregada;
            set
            {
                _listaCarregada = value;
                OnPropertyChanged(nameof(ListaCarregada));
            }
        }
        public List<ModelJogador> JogadoresPresentes
        {
            get => _jogadoresPresentes;
            set
            {
                _jogadoresPresentes = value;
                OnPropertyChanged(nameof(_jogadoresPresentes));
            }
        }
        public List<ModelJogador> Jogadores
        {
            get => _jogadores;
            set
            {
                _jogadores = value;
                OnPropertyChanged(nameof(Jogadores));
            }
        }
        public List<ModelTime> Times
        {
            get => _times;
            set
            {
                _times = value;
                OnPropertyChanged(nameof(Times));
            }
        }
        public int TamanhoEquipe
        {
            get => _tamanhoEquipe;
            set
            {
                _tamanhoEquipe = value;
                OnPropertyChanged(nameof(TamanhoEquipe));
            }
        }
        public int QuantidadeTimes
        {
            get => _quantidadeTimes;
            set
            {
                _quantidadeTimes = value;
                OnPropertyChanged(nameof(QuantidadeTimes));
            }
        }
        #endregion

        public void SortearTimes()
        {
            bool listaCarregada = false;
            // Verifica se o arquivo jogadores.json existe
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
            if (File.Exists(filePath))
            {
                // Se o arquivo existe, lê o conteúdo do arquivo e desserializa em uma lista de objetos JogadorModel
                string json = File.ReadAllText(filePath);
                List<ModelJogador> jogadores = new List<ModelJogador>();
                List<ModelJogador> jogadoresPresentes = new List<ModelJogador>();

                if (json != string.Empty)
                {
                    jogadores = JsonConvert.DeserializeObject<List<ModelJogador>>(json);
                    jogadoresPresentes = jogadores.Where(j => j.Status == 1).ToList();
                }
                Jogadores = new List<ModelJogador>(jogadores);
                JogadoresPresentes = new List<ModelJogador>(jogadoresPresentes);
                listaCarregada = true;
            }

            List<ModelJogador> listaTemporaria = new List<ModelJogador>();

            if (listaCarregada)
                listaTemporaria = new List<ModelJogador>(JogadoresPresentes);
            else
                listaTemporaria.DefaultIfEmpty();
            List<ModelTime> Times = new List<ModelTime>();

            //Embaralhar listaTemporaria
            Random rng = new Random();
            listaTemporaria = new List<ModelJogador>(listaTemporaria.OrderBy(x => rng.Next()));

            //Resgata o valor do picker e calcula a quantidade de times a ser gerada
            if (TamanhoEquipe != 0 && listaCarregada)
                //quantidadeTimes = Jogadores.Count / tamanhoEquipe;
                QuantidadeTimes = JogadoresPresentes.Count / TamanhoEquipe; //Sorteio agora é feito com base nos presentes

            if (QuantidadeTimes >= 1)
            {
                int k = 1;
                int j = 1;

                do
                {
                    ModelTime timeGen = new ModelTime();

                    //Inicializa a propriedade ListaJogador pela quantidade de jogadores fornecida
                    timeGen.ListaJogador = new List<ModelJogador>();
                    for (int i = 0; i < TamanhoEquipe; i++)
                    {
                        ModelJogador jogador = new ModelJogador();
                        jogador.Id = 0;
                        jogador.Nome = string.Empty;
                        jogador.Telefone = string.Empty;
                        jogador.Nota = 0;
                        timeGen.ListaJogador.Add(jogador);
                    }

                    //Laço interno para adicionar jogadores a cada time
                    do
                    {
                        Random rnd = new Random();
                        int indice = rnd.Next(listaTemporaria.Count - 1);
                        ModelJogador element = listaTemporaria[indice];

                        listaTemporaria.RemoveAt(indice);

                        timeGen.ListaJogador[k - 1].Id = element.Id;
                        timeGen.ListaJogador[k - 1].Nome = element.Nome;
                        timeGen.ListaJogador[k - 1].Telefone = element.Telefone;
                        timeGen.ListaJogador[k - 1].Nota = element.Nota;

                        k++; //Adicionou um jogador
                    }
                    while (k <= TamanhoEquipe);
                    k = 1;//Reinicia o ciclo de adicionar jogadores

                    timeGen.Nome = "Time " + j;

                    //Após esse processo, adicionamos o time na lista geral de times                    
                    Times.Add(timeGen);
                    j++;
                } while (j <= QuantidadeTimes);
                Criado = true;

                //Criar lista de espera com os jogadores que sobraram                                            
                if (listaTemporaria.Count != 0)
                {
                    ModelTime timeEspera = new ModelTime();
                    timeEspera.ListaJogador = new List<ModelJogador>();
                    for (int i = 0; i < listaTemporaria.Count; i++)
                    {
                        ModelJogador jogador = new ModelJogador();
                        jogador.Id = 0;
                        jogador.Nome = string.Empty;
                        jogador.Telefone = string.Empty;
                        jogador.Nota = 0;
                        timeEspera.ListaJogador.Add(jogador);
                    }

                    do
                    {
                        Random rnd = new Random();
                        int indice = rnd.Next(listaTemporaria.Count - 1);
                        ModelJogador element = listaTemporaria[indice];

                        listaTemporaria.RemoveAt(indice);

                        timeEspera.ListaJogador[k - 1].Id = element.Id;
                        timeEspera.ListaJogador[k - 1].Nome = element.Nome;
                        timeEspera.ListaJogador[k - 1].Telefone = element.Telefone;
                        timeEspera.ListaJogador[k - 1].Nota = element.Nota;

                        k++; //Adicionou um jogador

                    }
                    while (listaTemporaria.Count != 0);

                    timeEspera.Nome = "Lista de Espera";
                    Times.Add(timeEspera);
                }

                // Serializa a coleção Times em uma string JSON
                string json2 = JsonConvert.SerializeObject(Times);

                // Salva a string JSON em um arquivo
                string filePath2 = Path.Combine(FileSystem.AppDataDirectory, "times.json");
                File.WriteAllText(filePath2, json2);
            }
            else
                Criado = false;

            //Após salvar os times, atualiza o status de presença de todos os jogadores
            foreach (var jogador in Jogadores)
            {
                // Marcar ou desmarcar o jogador como presente
                jogador.Status = 0;

                // Salvar as informações atualizadas no arquivo JSON
                var filePathJogador = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
                string json = JsonConvert.SerializeObject(Jogadores);
                File.WriteAllText(filePathJogador, json);
            }
        }
        public List<ModelTime> CarregarLista()
        {
            List<ModelTime> times = new List<ModelTime>();
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "times.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (json != string.Empty)
                    times = JsonConvert.DeserializeObject<List<ModelTime>>(json);
            }
            return Times = new List<ModelTime>(times);
        }

        public List<ModelJogador> CarregarListaTroca()
        {
            List<ModelTime> times = new List<ModelTime>(ListaCarregada);
            List<ModelJogador> jogadores = times.SelectMany(time => time.ListaJogador).ToList();
            List<ModelJogador> listaAtualizada = new List<ModelJogador>();

            // Carregar lista sem jogadores selecionados para troca
            foreach (ModelJogador element2 in jogadores)
            {
                if (!ListaParaTroca1.Any(j => j.Id == element2.Id))
                {
                    ModelJogador objJogador = new ModelJogador();
                    objJogador.Id = element2.Id;
                    objJogador.Nome = element2.Nome;
                    objJogador.Telefone = element2.Telefone;
                    objJogador.Status = element2.Status;
                    objJogador.Nota = element2.Nota;

                    listaAtualizada.Add(objJogador);
                }
            }
            ListaGeralTroca = listaAtualizada;
            return listaAtualizada;
        }


        public void EditarTimes(ModelTime timeParaEditar)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "times.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<ModelTime> times = new List<ModelTime>();

                if (json != string.Empty)
                {
                    times = JsonConvert.DeserializeObject<List<ModelTime>>(json);
                }

                ObservableCollection<ModelTime> modelTimes = new ObservableCollection<ModelTime>(times);
            }

            //Após salvar os times, atualiza o status de presença de todos os jogadores
            foreach (var jogador in Jogadores)
            {
                // Marcar ou desmarcar o jogador como presente
                jogador.Status = 0;

                // Salvar as informações atualizadas no arquivo JSON
                var filePathJogador = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
                string json = JsonConvert.SerializeObject(Jogadores);
                File.WriteAllText(filePathJogador, json);
            }
        }
    }
}

