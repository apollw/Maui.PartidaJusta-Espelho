namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuSorteio : ContentPage
{
	public MenuSorteio()
	{
		InitializeComponent();
	}
    private async void SorteioStatus(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuSorteioStatus());
    }

    private async void MenuListaSorteio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuSorteioLista());

    }

}
