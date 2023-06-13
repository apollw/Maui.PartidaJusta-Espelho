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
    public class ViewModelJogador : INotifyPropertyChanged
    {
<<<<<<< HEAD
=======
        private ModelJogador objJogador;

        private ObservableCollection<ModelJogador> _listaCarregada;

>>>>>>> 8963500108d8283c9b49f492a2e02e9336ef3fe2
        public event PropertyChangedEventHandler PropertyChanged;
        //A ViewModel se comunica com a View através de propriedades públicas
        private bool _cadastroConcluido = false;
        private ModelJogador _objJogador;
        private ObservableCollection<ModelJogador> _listaCarregada;
        private bool isLoading;
        public string _entryNome { get; set; }
        public string _entryTelefone { get; set; }
        public int _stepperValue { get; set; }
        #region
        //Declaração de Comandos Vinculados
        public ICommand CmdSalvarJogador => new Command(SalvarJogador);
        #endregion

        //No construtor da VM, instanciamos um objeto de Jogador
        public ViewModelJogador()
        {
            ObjJogador = new ModelJogador();
            ListaCarregada = new ObservableCollection<ModelJogador>(CarregarLista());
        }
        #region

        //Getters e Setters
        public ObservableCollection<ModelJogador> ListaCarregada
        {
            get => _listaCarregada;
            set
            {
                _listaCarregada = value;
                OnPropertyChanged(nameof(ListaCarregada));
            }
        }
        public ModelJogador ObjJogador
        {
            get => _objJogador;
            set
            {
                _objJogador = value;
                OnPropertyChanged(nameof(ObjJogador));
            }
        }
        public bool CadastroConcluido
        {
            get => _cadastroConcluido;
            set
            {
                _cadastroConcluido = value;
                OnPropertyChanged(nameof(CadastroConcluido));
            }
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        #endregion

        #region
        //PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //Métodos Associados aos Commands
        public async void SalvarJogador()
        {
            try
            {
                if (_stepperValue == 0)
                {
                    _stepperValue = 1;
                }

                bool isNomeEmpty = false;
                bool isTelefoneEmpty = false;
                bool isVerified = false;

                if (string.IsNullOrEmpty(_entryNome))
                {
                    isNomeEmpty = true;
                    throw new Exception("Nome não informado");
                }
                if (string.IsNullOrEmpty(_entryTelefone))
                {
                    isTelefoneEmpty = true;
                    throw new Exception("Telefone não informado");
                }

                if (!isNomeEmpty && !isTelefoneEmpty)
                    isVerified = true;

                if (isVerified)
                {
                    //Preencher objeto ModelJogador
                    ObjJogador.Id = GerarNovoId(ObjJogador.Id);
                    ObjJogador.Nome = _entryNome;
                    ObjJogador.Telefone = _entryTelefone;
                    ObjJogador.Nota = _stepperValue;
                }

                List<ModelJogador> listaTemp = new List<ModelJogador>();
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    List<ModelJogador> jogadores = new List<ModelJogador>();

                    if (json != string.Empty)
                        jogadores = JsonConvert.DeserializeObject<List<ModelJogador>>(json);

                    listaTemp = jogadores;
                }

                //Verificar Telefone Repetido
                foreach (ModelJogador element in listaTemp)
                {
                    if (element.Telefone == ObjJogador.Telefone && ObjJogador.Id != element.Id)
                    {
                        isVerified = false;
                        throw new Exception("Telefone já registrado!");
                    }
                }

                bool isEdicao = false;
                List<ModelJogador> listaEdicao = new List<ModelJogador>(listaTemp);
                foreach (ModelJogador element in listaEdicao)
                {
                    if (element.Id == ObjJogador.Id && isVerified)
                    {
                        isEdicao = true;
                        listaTemp.Remove(element);
                        listaTemp.Add(ObjJogador);

                        File.WriteAllText(filePath, JsonConvert.SerializeObject(listaTemp));
                        CadastroConcluido = true;
                    }
                }

                if (isVerified && !isEdicao)
                {
                    List<ModelJogador> jogadoresParaAdicionar = new List<ModelJogador>(listaTemp);
                    jogadoresParaAdicionar.Add(ObjJogador);
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jogadoresParaAdicionar));
                    CadastroConcluido = true;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Fechar");
            }
        }

        public List<ModelJogador> CarregarLista()
        {
            List<ModelJogador> jogadores = new List<ModelJogador>();

            string filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                if (json != string.Empty)
                    jogadores = JsonConvert.DeserializeObject<List<ModelJogador>>(json);

                ListaCarregada = new ObservableCollection<ModelJogador>(jogadores);

            }
            return jogadores;
        }

        public void AtualizarListaCarregada()
        {
            ListaCarregada = new ObservableCollection<ModelJogador>(CarregarLista());
        }

        private int GerarNovoId(int id)
        {
            if (id != 0)
            {
                return id; // Retorna a ID existente do jogador em edição
            }
            else
            {
                if (ListaCarregada.Count == 0)
                {
                    return 1; // Se a lista está vazia, retorna 1 como o novo ID
                }
                else
                {
                    int ultimoIdUtilizado = ListaCarregada.Max(jogador => jogador.Id);
                    int novoId = ultimoIdUtilizado + 1;
                    return novoId;
                }
            }
        }

    }

}




