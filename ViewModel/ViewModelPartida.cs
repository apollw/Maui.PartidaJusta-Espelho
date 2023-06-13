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

namespace Maui.PartidaJusta_Espelho.ViewModel
{
    public class ViewModelPartida : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ModelPartida _objPartida;
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

        #region
        //Declaração de Comandos Vinculados
        #endregion

        public ViewModelPartida()
        {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTimerElapsed;
            ObjPartida = new ModelPartida();
            ListaDeTimes = new ObservableCollection<ModelTime>(CarregarListaTimes());
            ListaDeTimesSuperior = new ObservableCollection<ModelTime>();
            ListaDeTimesInferior = new ObservableCollection<ModelTime>();

            foreach (ModelTime element in ListaDeTimes)
            {
                if (ListaDeTimes.IndexOf(element) % 2 == 0)
                {
                    ListaDeTimesSuperior.Add(element);
                }
                else
                    ListaDeTimesInferior.Add(element);
            }

        }

        #region
        //Getters e Setters
        public ModelPartida ObjPartida
        {
            get => _objPartida;
            set
            {
                _objPartida = value;
                OnPropertyChanged(nameof(ObjPartida));
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
        public void SalvarPartida()
        {
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

            //Registrar o placar exato da partida

            //Finaliza a Partida
            PodeRegistrarPartida = true;
            PartidaRegistrada = true;


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

        public void AtualizarListaCarregada()
        {
            ListaDeTimes = new ObservableCollection<ModelTime>(CarregarListaTimes());
        }

        public void StartTimer()
        {
            Time = TimeSpan.Zero;
            _timer.Start();
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


        //

    }
}
