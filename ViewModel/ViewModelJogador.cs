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
        public event PropertyChangedEventHandler PropertyChanged;
        //A ViewModel se comunica com a View através de propriedades públicas
        private bool _cadastroConcluido;
        private ModelJogador _objJogador;
        private ObservableCollection<ModelJogador> _listaCarregada;
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
            //ListaCarregada = new ObservableCollection<ModelJogador>();
            ListaCarregada = CarregarLista();
        }
        //Getters e Setters
        #region
        public ModelJogador ObjJogador
        {
            get => _objJogador;
            set
            {
                _objJogador = value;
                OnPropertyChanged(nameof(ObjJogador));
            }
        }
        public ObservableCollection<ModelJogador> ListaCarregada
        {
            get => _listaCarregada;
            set
            {
                _listaCarregada = value;
                OnPropertyChanged(nameof(ListaCarregada));
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
        #endregion
        //PropertyChanged
        #region

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
                CadastroConcluido = false;

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
                listaTemp.AddRange(ListaCarregada);
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
                foreach (ModelJogador element in ListaCarregada)
                {
                    if (element.Id == ObjJogador.Id && isVerified)
                    {
                        isEdicao = true;
                        ListaCarregada.Remove(element);
                        ListaCarregada.Add(ObjJogador);

                        CadastroConcluido = true;
                        break; // Saia do loop, já que encontramos o jogador a ser editado
                    }
                }

                if (isVerified && !isEdicao)
                {

                    ListaCarregada.Add(ObjJogador);

                    //Salvar alterações no JSON
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(ListaCarregada));
                    //Exemplo de salvamento assíncrono usando System.IO.File.WriteAllText
                    //string json = JsonConvert.SerializeObject(ListaCarregada);
                    //await Task.Run(() => File.WriteAllText(filePath, json));

                    CadastroConcluido = true;
                }


            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Fechar");
            }
        }
        public ObservableCollection<ModelJogador> CarregarLista()
        {
            if (ListaCarregada == null)
            {
                ObservableCollection<ModelJogador> jogadores = new ObservableCollection<ModelJogador>();
                string filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    if (json != string.Empty)
                        jogadores = JsonConvert.DeserializeObject<ObservableCollection<ModelJogador>>(json);
                }
                return jogadores;
            }
            //Se não é a primeira vez, carregar a lista atual
            return ListaCarregada;
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
        public void AtualizarListaCarregada()
        {
            //ListaCarregada = new ObservableCollection<ModelJogador>(CarregarLista());
        }
    }
}


