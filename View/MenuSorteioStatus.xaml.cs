using Maui.PartidaJusta_Espelho.Model;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Runtime.InteropServices.ObjectiveC;
using Maui.PartidaJusta_Espelho.ViewModel;

namespace Maui.PartidaJusta_Espelho.View;
public partial class MenuSorteioStatus : ContentPage
{

    ViewModelJogador viewModelJogador = new ViewModelJogador();
    public MenuSorteioStatus()
    {
        BindingContext = viewModelJogador;
        InitializeComponent();
    }
    async void IniciarSorteio(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuSorteioJogadores());
    }
    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var dados = viewModelJogador.ListaCarregada;
        //var dados = _collectionView.ItemsSource;
        // Salvar as informa��es atualizadas no arquivo JSON
        var filePath = Path.Combine(FileSystem.AppDataDirectory, "jogadores.json");
        string json = JsonConvert.SerializeObject(dados);
        File.WriteAllText(filePath, json);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Atualizar os dados sempre que a p�gina for exibida
        // viewModelJogador.AtualizarListaCarregada();

        // Atualizar a exibi��o das checkboxes
        _collectionView.ItemsSource = viewModelJogador.ListaCarregada;
    }
    public void DesmarcarTodos(object sender, EventArgs e)
    {
        foreach (var jogador in viewModelJogador.ListaCarregada)
        {
            jogador.Status = 0;
        }
        AtualizarExibicaoCheckBoxes();
    }
    public void SelecionarTodos(object sender, EventArgs e)
    {
        // Verifica se todos os jogadores j� est�o selecionados
        bool todosSelecionados = viewModelJogador.ListaCarregada.All(jogador => jogador.Status == 1);

        // Define o status de cada jogador para 1 (selecionado) ou 0 (n�o selecionado)
        foreach (var jogador in viewModelJogador.ListaCarregada)
        {
            jogador.Status = todosSelecionados ? 0 : 1;
        }
        AtualizarExibicaoCheckBoxes();
    }

    private void AtualizarExibicaoCheckBoxes()
    {
        // Atualizar a exibi��o das checkboxes na tela
        _collectionView.ItemsSource = null;
        _collectionView.ItemsSource = viewModelJogador.ListaCarregada;
    }

}

