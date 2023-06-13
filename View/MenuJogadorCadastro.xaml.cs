using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;


namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuJogadorCadastro : ContentPage
{

<<<<<<< HEAD
    ViewModelJogador viewModel = new ViewModelJogador();

=======
    ViewModelJogador viewModel;// = new ViewModelJogador();
>>>>>>> 8963500108d8283c9b49f492a2e02e9336ef3fe2
    public MenuJogadorCadastro()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    public MenuJogadorCadastro(ViewModelJogador viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = this.viewModel;

        FillPage();
    }
    public void FillPage()
    {
        _entryNome.Text = viewModel.ObjJogador.Nome;
        _entryTelefone.Text = viewModel.ObjJogador.Telefone;
        _stepper.Value = viewModel.ObjJogador.Nota;
    }
<<<<<<< HEAD
    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (viewModel.ObjJogador.Id == 0)
        {
            viewModel.SalvarJogador();
            if (viewModel.CadastroConcluido)
                await Navigation.PopAsync();
            //await Navigation.PopModalAsync();
        }
        else
        {
            viewModel.SalvarJogador();
            if (viewModel.CadastroConcluido)
                await Navigation.PopAsync();
            //await Navigation.PopModalAsync();
        }
    }
=======
>>>>>>> 8963500108d8283c9b49f492a2e02e9336ef3fe2
}


