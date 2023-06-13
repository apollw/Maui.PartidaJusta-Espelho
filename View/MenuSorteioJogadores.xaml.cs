using Maui.PartidaJusta_Espelho.ViewModel;

namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuSorteioJogadores : ContentPage
{


    ViewModelTime viewModel = new ViewModelTime();
    public MenuSorteioJogadores()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void SortearTimes(object sender, EventArgs e)
    {
        if (viewModel.Criado == true)
        {
            await DisplayAlert("Alerta", "Times criados com sucesso!", "Concluir");
            await Navigation.PushAsync(new MenuSorteioLista());
        }
        else
        {
            await DisplayAlert("Alerta", "N�mero Insuficiente de Jogadores", "Fechar");
            await Navigation.PopAsync();
        }
    }

    void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
    {
        viewModel.TamanhoEquipe = (int)e.NewValue;
    }

}

