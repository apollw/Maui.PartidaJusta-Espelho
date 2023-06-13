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

    async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var item = swipeItem.BindingContext as ModelJogador;

        string message = "Deseja excluir " + item.Nome + "?";

        bool confirmar = await DisplayAlert("Aviso", message, "Sim", "N�o");

        if (item != null && confirmar)
        {
            viewModel.ListaCarregada.Remove(item);
            var filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
            string json = JsonConvert.SerializeObject(viewModel.ListaCarregada);
            File.WriteAllText(filePath, json);
        }
    }

    async void OnEditSwipeItemInvoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var item = swipeItem.BindingContext as ModelJogador;

        viewModel.ObjJogador = item;

        //Passo a viewModel inteira como par�metro
        await Navigation.PushAsync(new MenuJogadorCadastro(viewModel));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.AtualizarListaCarregada();
    }

    private bool isButtonClicked = false;
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (!isButtonClicked)
        {
            isButtonClicked = true;
            await Navigation.PushAsync(new MenuJogadorCadastro());
            // Aguardar a conclusao da navegacao
            isButtonClicked = false;
        }
    }


}




