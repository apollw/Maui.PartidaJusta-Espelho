namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuPartida : ContentPage
{
	public MenuPartida()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPartidaRegistro());
    }
}
