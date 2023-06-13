using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;

namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuSorteioFinalizarEdicao : ContentPage
{
    ViewModelTime viewModel = new ViewModelTime();
    public MenuSorteioFinalizarEdicao()
    {
        InitializeComponent();
    }
    public MenuSorteioFinalizarEdicao(ViewModelTime viewModelatual)
    {
        BindingContext = viewModelatual;
        InitializeComponent();
    }
    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        ModelJogador jogador = (ModelJogador)checkBox.BindingContext;

        //Preenche lista de troca 2
        ModelJogador modelJogador = new ModelJogador();
        modelJogador.Id = jogador.Id;
        modelJogador.Nome = jogador.Nome;
        modelJogador.Telefone = jogador.Telefone;
        modelJogador.Nota = jogador.Nota;
        modelJogador.Posicao = jogador.Posicao;
        modelJogador.Status = jogador.Status;

        viewModel.ListaParaTroca2.Add(modelJogador);

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Atualizar os dados sempre que a p�gina for exibida
        viewModel.CarregarListaTroca();

        // Atualizar a exibi��o das checkboxes
        //_collectionView.ItemsSource = viewModelJogador.ListaCarregada;
    }
}

