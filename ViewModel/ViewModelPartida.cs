using Maui.PartidaJusta_Espelho.Model;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.PartidaJusta_Espelho.ViewModel
{
    public class ViewModelPartida : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ModelPartida _objPartida;
        private ModelTime _timeParaEdicao;
        private List<ModelPartida> _listaPartida;
        private ObservableCollection<ModelJogador> _listaDeJogadores;
        private List<ModelJogador> _listaDeAdicao;
        private ObservableCollection<ModelTime> _listaDeTimes;
        private ObservableCollection<ModelTime> _listaDeTimesSuperior;
        private ObservableCollection<ModelTime> _listaDeTimesInferior;
        private List<ModelTime> _timesSelecionados;
        private List<ModelTime> _listaDeTimesTemporaria;
        private int _placar1;
        private int _placar2;
        private readonly IDispatcherTimer _timer;
        private TimeSpan _time;
        private bool _isTimerRunning;
        private string _numeroSelecionado1;
        private string _numeroSelecionado2;
        private object _timeSelecionado1;
        private object _timeSelecionado2;
        private bool _podeRegistrarPartida = false;
        private bool _partidaRegistrada = false;
        private bool _incompleto = false;


        #region
        //Declaração de Comandos Vinculados
        #endregion

        public ViewModelPartida()
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerElapsed;
            ObjPartida = new ModelPartida();
            TimeParaEdicao = new ModelTime();
            ListaDeAdicao = new List<ModelJogador>();
            ListaDeJogadores = new ObservableCollection<ModelJogador>(CarregarListaJogadores());
            ListaDeTimes = new ObservableCollection<ModelTime>(CarregarListaTimes());
        }

        #region
        //Getters e Setters
        public bool Incompleto
        {
            get => _incompleto;
            set
            {
                _incompleto = value;
                OnPropertyChanged(nameof(Incompleto));
            }
        }
        public ModelPartida ObjPartida
        {
            get => _objPartida;
            set
            {
                _objPartida = value;
                OnPropertyChanged(nameof(ObjPartida));
            }
        }
        public ModelTime TimeParaEdicao
        {
            get => _timeParaEdicao;
            set
            {
                _timeParaEdicao = value;
                OnPropertyChanged(nameof(TimeParaEdicao));
            }
        }
        public ObservableCollection<ModelJogador> ListaDeJogadores
        {
            get => _listaDeJogadores;
            set
            {
                _listaDeJogadores = value;
                OnPropertyChanged(nameof(ListaDeJogadores));
            }
        }
        public List<ModelJogador> ListaDeAdicao
        {
            get => _listaDeAdicao;
            set
            {
                _listaDeAdicao = value;
                OnPropertyChanged(nameof(ListaDeAdicao));
            }
        }
        public List<ModelPartida> ListaPartida
        {
            get => _listaPartida;
            set
            {
                _listaPartida = value;
                OnPropertyChanged(nameof(ListaPartida));
            }
        }
        public ObservableCollection<ModelTime> ListaDeTimes
        {
            get => _listaDeTimes;
            set
            {
                _listaDeTimes = value;
                OnPropertyChanged(nameof(ListaDeTimes));
            }
        }
        public ObservableCollection<ModelTime> ListaDeTimesSuperior
        {
            get => _listaDeTimesSuperior;
            set
            {
                _listaDeTimesSuperior = value;
                OnPropertyChanged(nameof(ListaDeTimesSuperior));
            }
        }
        public ObservableCollection<ModelTime> ListaDeTimesInferior
        {
            get => _listaDeTimesInferior;
            set
            {
                _listaDeTimesInferior = value;
                OnPropertyChanged(nameof(ListaDeTimesInferior));
            }
        }
        public List<ModelTime> ListaDeTimesTemporaria
        {
            get => _listaDeTimesTemporaria;
            set
            {
                _listaDeTimesTemporaria = value;
                OnPropertyChanged(nameof(ListaDeTimesTemporaria));
            }
        }
        public List<ModelTime> TimesSelecionados
        {
            get => _timesSelecionados;
            set
            {
                _timesSelecionados = value;
                OnPropertyChanged(nameof(TimesSelecionados));
            }
        }
        public object TimeSelecionado1
        {
            get => _timeSelecionado1;
            set
            {
                _timeSelecionado1 = value;
                OnPropertyChanged(nameof(TimeSelecionado1));
            }
        }
        public object TimeSelecionado2
        {
            get => _timeSelecionado2;
            set
            {
                _timeSelecionado2 = value;
                OnPropertyChanged(nameof(TimeSelecionado2));
            }
        }
        public int Placar1
        {
            get => _placar1;
            set
            {
                _placar1 = value;
                OnPropertyChanged(nameof(Placar1));
            }
        }
        public int Placar2
        {
            get => _placar2;
            set
            {
                _placar2 = value;
                OnPropertyChanged(nameof(Placar2));
            }
        }
        public string NumeroSelecionado1
        {
            get => _numeroSelecionado1;
            set
            {
                _numeroSelecionado1 = value;
                int.TryParse(value?.Replace("numero_", "")?.Replace(".png", ""), out int placar1);
                Placar1 = placar1;
                OnPropertyChanged(nameof(NumeroSelecionado1));
            }
        }
        public string NumeroSelecionado2
        {
            get => _numeroSelecionado2;
            set
            {
                _numeroSelecionado2 = value;
                int.TryParse(value?.Replace("numero_", "")?.Replace(".png", ""), out int placar2);
                Placar2 = placar2;
                OnPropertyChanged(nameof(NumeroSelecionado2));
            }
        }
        public TimeSpan Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            set
            {
                _isTimerRunning = value;
                OnPropertyChanged(nameof(IsTimerRunning));
            }
        }
        public bool PodeRegistrarPartida
        {
            get { return _podeRegistrarPartida; }
            set
            {
                _podeRegistrarPartida = value;
                OnPropertyChanged(nameof(PodeRegistrarPartida));
            }
        }
        public bool PartidaRegistrada
        {
            get { return _partidaRegistrada; }
            set
            {
                _partidaRegistrada = value;
                OnPropertyChanged(nameof(PartidaRegistrada));
            }
        }
        #endregion


        //PropertyChanged
        #region
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        //Métodos Associados aos Commands
        public async void SalvarPartida()
        {
            try
            {
                PodeRegistrarPartida = true;
                PartidaRegistrada = false;
                //Registrar os times selecionados nas CarouselView
                var timesSelecionados = new List<ModelTime>();
                foreach (var time in ListaDeTimes)
                {
                    if (TimeSelecionado1 == time)
                    {
                        timesSelecionados.Add(time);
                    }
                    if (TimeSelecionado2 == time)
                    {
                        timesSelecionados.Add(time);
                    }
                }
                TimesSelecionados = timesSelecionados;

                if (TimesSelecionados.Count == 0)
                {
                    PodeRegistrarPartida = false;
                    throw new Exception("Nenhum time registrado");
                }

                //Caso de times iguais
                if (TimesSelecionados[0].Id == TimesSelecionados[1].Id)
                {
                    PodeRegistrarPartida = false;
                    throw new Exception("Selecione times diferentes");
                }

                //Caso de times incompletos
                foreach (ModelTime element in TimesSelecionados)
                {
                    ModelTime timeParaEdicao = new ModelTime();

                    if (element.ListaJogador.Count < element.TotalJogadores)
                    {
                        timeParaEdicao = element;
                        Incompleto = true;
                        PodeRegistrarPartida = false;

                        await App.Current.MainPage.DisplayAlert("Aviso", "Um dos times está incompleto", "Editar");
                        break;
                        //await App.Current.MainPage.Navigation.PushAsync(new MenuEdicaoTime(timeParaEdicao));                     
                    }
                }

                //Finaliza a Partida
                if (PodeRegistrarPartida)
                {
                    ObjPartida = new ModelPartida();
                    ListaPartida = new List<ModelPartida>();

                    ObjPartida.Id = 0;
                    ObjPartida.Data = DateTime.Now;
                    ObjPartida.TimeCasa = TimesSelecionados[0].Id;
                    ObjPartida.TimeVisitante = TimesSelecionados[1].Id;
                    ObjPartida.TimeCasaGols = Placar1;
                    ObjPartida.TimeVisitanteGols = Placar2;

                    if (Placar1 > Placar2)
                        ObjPartida.TimeVencedor = TimesSelecionados[0].Id;
                    else if (Placar2 > Placar1)
                        ObjPartida.TimeVencedor = TimesSelecionados[1].Id;
                    else
                        ObjPartida.TimeVencedor = 0;

                    // Verifica se o arquivo partidas.json existe
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, "partidas.json");
                    if (File.Exists(filePath))
                    {
                        string json = File.ReadAllText(filePath);
                        List<ModelPartida> partidas = new List<ModelPartida>();

                        if (json != string.Empty)
                        {
                            partidas = JsonConvert.DeserializeObject<List<ModelPartida>>(json);
                        }
                        ListaPartida = partidas;
                    }

                    //ModelPartidaJogador modelPartidaJogador = new ModelPartidaJogador();
                    //modelPartidaJogador.Partida = ObjPartida.Id;
                    //modelPartidaJogador.Jogador = ObjPartida.

                    ListaPartida.Add(ObjPartida);

                    // Serializa a coleção ListaPartidas em uma string JSON
                    string json2 = JsonConvert.SerializeObject(ListaPartida);
                    // Salva a string JSON em um arquivo
                    string filePath2 = Path.Combine(FileSystem.AppDataDirectory, "partidas.json");
                    File.WriteAllText(filePath2, json2);

                    PartidaRegistrada = true;
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Fechar");
            }

        }
        public List<ModelTime> CarregarListaTimes()
        {
            List<ModelTime> times = new List<ModelTime>();

            string filePath = Path.Combine(FileSystem.AppDataDirectory, "times.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                if (json != string.Empty)
                    times = JsonConvert.DeserializeObject<List<ModelTime>>(json);

                ListaDeTimes = new ObservableCollection<ModelTime>(times);

            }
            return times;
        }
        public List<ModelJogador> CarregarListaJogadores()
        {
            List<ModelTime> times = new List<ModelTime>();
            List<ModelJogador> jogadores = new List<ModelJogador>();

            //Abre a lista de times sorteados
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "times.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                if (json != string.Empty)
                    times = JsonConvert.DeserializeObject<List<ModelTime>>(json);

                ListaDeTimes = new ObservableCollection<ModelTime>(times);

                //Adicionar todos os jogadores da lista de times na lista jogadores
                foreach (ModelTime element in ListaDeTimes)
                {
                    if (element.TotalJogadores == element.ListaJogador.Count)
                        foreach (ModelJogador jogador in element.ListaJogador)
                        {
                            ModelJogador player = new ModelJogador();

                            player.Id = jogador.Id;
                            player.Nome = jogador.Nome;
                            player.Telefone = jogador.Telefone;
                            player.Status = jogador.Status;
                            player.Posicao = jogador.Posicao;

                            jogadores.Add(player);
                        }
                }

            }

            return jogadores;
        }
        public void AtualizarListaCarregada()
        {
            ListaDeTimes = new ObservableCollection<ModelTime>(CarregarListaTimes());
            ListaDeJogadores = new ObservableCollection<ModelJogador>(CarregarListaJogadores());
        }
        public async void EditarTime(ModelTime timeParaEditar)
        {
            bool _podeEditar = true;
            int _numeroParaCompletar = timeParaEditar.TotalJogadores - timeParaEditar.ListaJogador.Count;
            try
            {
                //Não pode Editar se o número for diferente dos que faltam para completar
                if (ListaDeAdicao.Count != _numeroParaCompletar)
                {
                    _podeEditar = false;
                    throw new Exception("Selecione o número correto de jogadores");
                }

                if (_podeEditar)
                {
                    List<ModelJogador> jogadoresAdicionados = new List<ModelJogador>();

                    foreach (ModelTime element in ListaDeTimes)
                    {
                        if (element.Id != timeParaEditar.Id)
                        {
                            foreach (ModelJogador jogador in ListaDeAdicao)
                            {
                                ModelJogador jogadorExistente = element.ListaJogador.Find(j => j.Id == jogador.Id);
                                if (jogadorExistente != null)
                                {
                                    //Remove do time origem
                                    element.ListaJogador.RemoveAll(j => j.Id == jogador.Id);

                                    ModelJogador jogadorTemp = new ModelJogador();

                                    jogadorTemp.Id = jogadorExistente.Id;
                                    jogadorTemp.Nome = jogadorExistente.Nome;
                                    jogadorTemp.Telefone = jogadorExistente.Telefone;
                                    jogadorTemp.Status = jogadorExistente.Status;
                                    jogadorTemp.Nota = jogadorExistente.Nota;
                                    jogadorTemp.Posicao = jogadorExistente.Posicao;

                                    jogadoresAdicionados.Add(jogadorTemp);
                                }
                            }
                        }
                    }

                    foreach (ModelTime element in ListaDeTimes)
                    {
                        //Após retirar os jogadores, adicinar todos ao time destino
                        if (element.Id == timeParaEditar.Id)
                        {
                            element.ListaJogador.AddRange(jogadoresAdicionados);
                        }

                    }

                    //Após mudar os times, salvar o novo arquivo JSON
                    string json = JsonConvert.SerializeObject(ListaDeTimes);

                    // Salva a string JSON em um arquivo
                    string filePath = Path.Combine(FileSystem.AppDataDirectory, "times.json");
                    File.WriteAllText(filePath, json);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Fechar");
            }
        }
        public async void StartTimer()
        {
            try
            {
                PodeRegistrarPartida = true;

                //Registrar os times selecionados nas CarouselView
                var timesSelecionados = new List<ModelTime>();
                foreach (var time in ListaDeTimes)
                {
                    if (TimeSelecionado1 == time)
                    {
                        timesSelecionados.Add(time);
                    }
                    if (TimeSelecionado2 == time)
                    {
                        timesSelecionados.Add(time);
                    }
                }
                TimesSelecionados = timesSelecionados;

                if (TimesSelecionados.Count == 0)
                {
                    PodeRegistrarPartida = false;
                    throw new Exception("Nenhum time registrado");
                }

                //Caso de times iguais
                if (TimesSelecionados[0].Id == TimesSelecionados[1].Id)
                {
                    PodeRegistrarPartida = false;
                    throw new Exception("Selecione times diferentes");
                }

                //Caso de times incompletos
                foreach (ModelTime element in TimesSelecionados)
                {
                    ModelTime timeParaEdicao = new ModelTime();

                    if (element.ListaJogador.Count < element.TotalJogadores)
                    {
                        timeParaEdicao = element;
                        Incompleto = true;
                        PodeRegistrarPartida = false;

                        await App.Current.MainPage.DisplayAlert("Aviso", "Um dos times está incompleto", "Editar");
                        await App.Current.MainPage.Navigation.PushAsync(new MenuEdicaoTime(timeParaEdicao));
                    }
                }

                //Finaliza a Partida
                if (PodeRegistrarPartida)
                {
                    //Se é uma Partida Válida, pode iniciar a contagem de tempo
                    Time = TimeSpan.Zero;
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Fechar");
            }
        }
        public void StopTimer()
        {
            _timer.Stop();
        }
        private void OnTimerElapsed(object sender, EventArgs e)
        {
            Time = Time.Add(TimeSpan.FromSeconds(1));
        }
        public void ResetTimer()
        {
            Time = TimeSpan.Zero;
        }

    }
}

