using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;


namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuJogadorCadastro : ContentPage
{
    ViewModelJogador viewModel = new ViewModelJogador();

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
    private async void SavedButton(object sender, EventArgs e)
    {
        
            viewModel.SalvarJogador();            
            if (viewModel.CadastroConcluido)
                await Navigation.PopAsync();
     
    }
    private void _entryNome_Focused(object sender, FocusEventArgs e)
    {
        _entryNome.Completed += (s, e) =>
        {
            _entryTelefone.Focus();
        };
    }
    private void _entryTelefone_Focused(object sender, FocusEventArgs e)
    {
        _entryTelefone.Completed += (s, e) =>
        {
            _entryTelefone.IsEnabled = false;
            _entryTelefone.Unfocus();
            _entryTelefone.IsEnabled = true;
        };
    }
}

