using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuJogadorEditar : ContentPage
{
    ViewModelJogador viewModel = new ViewModelJogador();
    public MenuJogadorEditar()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        //Limpar ObJogador caso jogador seja novo
        viewModel.ObjJogador = new ModelJogador();
        viewModel.CadastroConcluido = false;
        await Navigation.PushAsync(new MenuJogadorCadastro(viewModel));

    }
    async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var item = swipeItem.BindingContext as ModelJogador;

        string message = "Deseja excluir " + item.Nome + "?";

        bool confirmar = await DisplayAlert("Aviso", message, "Sim", "Nao");

        if (item != null && confirmar)
        { 
            viewModel.ListaCarregada.Remove(item);
        }
    }

    async void OnEditSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var item = swipeItem.BindingContext as ModelJogador;

        viewModel.ObjJogador = item;
        viewModel.CadastroConcluido= false;

        //Passo a viewModel inteira como par�metro        
        await Navigation.PushAsync(new MenuJogadorCadastro(viewModel));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //BindingContext = viewModel;
        viewModel.ListaCarregada = viewModel.CarregarLista();
    }

}




