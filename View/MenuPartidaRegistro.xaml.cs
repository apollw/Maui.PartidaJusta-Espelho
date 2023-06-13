using Maui.PartidaJusta_Espelho.ViewModel;

namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuPartidaRegistro : ContentPage
{
    ViewModelPartida viewModel = new ViewModelPartida();
    public MenuPartidaRegistro()
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    private void FinalizarPartida(object sender, EventArgs e)
    {
        viewModel.SalvarPartida();
    }

    void OnStartButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (ViewModelPartida)BindingContext;
        viewModel.StartTimer();
    }
    void OnStopButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (ViewModelPartida)BindingContext;
        viewModel.StopTimer();
    }
    void OnResetButtonClicked(object sender, EventArgs e)
    {
        var viewModel = (ViewModelPartida)BindingContext;
        viewModel.ResetTimer();
    }
}