using Maui.PartidaJusta_Espelho.Model;

namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuSorteioLista : ContentPage
{
    public MenuSorteioLista()
    {
        InitializeComponent();
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = sender as Button;
        var time = button?.BindingContext as ModelTime;

        if (time != null)
        {
            // Navegar para a tela de edi��o, passando o objeto de dados do time como par�metro
            Navigation.PushAsync(new MenuSorteioEditar(time));
        }
    }
}