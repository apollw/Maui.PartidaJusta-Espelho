using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;

namespace Maui.PartidaJusta_Espelho;

public partial class MenuEdicaoTime : ContentPage
{
    ViewModelPartida viewModel = new ViewModelPartida();
    ModelTime TimeParaEdicao = new ModelTime();
    public MenuEdicaoTime(ModelTime timeParaEdicao)
	{
		InitializeComponent();
        BindingContext = viewModel;
        TimeParaEdicao = timeParaEdicao;
    }
    async void SalvarTime(object sender, EventArgs e)
    {
        //Acessar o método de Adição de Jogadores
        viewModel.EditarTime(TimeParaEdicao);
        viewModel.ListaDeAdicao.Clear();
        await Navigation.PopAsync();

    }
    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var meuItem = (ModelJogador)checkBox.BindingContext;
        //Cria lista de adição ao time incompleto
        viewModel.ListaDeAdicao.Add(meuItem);
    }

}