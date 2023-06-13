using Maui.PartidaJusta_Espelho.Model;
using Maui.PartidaJusta_Espelho.ViewModel;
using Newtonsoft.Json;
using System.Linq;


namespace Maui.PartidaJusta_Espelho.View;

public partial class MenuSorteioEditar : ContentPage
{
    ViewModelTime viewModel = new ViewModelTime();
    public MenuSorteioEditar()
    {
        InitializeComponent();
    }
    public MenuSorteioEditar(ModelTime modelTime)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.ObjTimeParaEditar = modelTime;

    }
    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        ModelJogador jogador = (ModelJogador)checkBox.BindingContext;
        bool exists = false;

        if (checkBox.IsChecked == true)
        {
            //Preenche lista de troca 1
            ModelJogador modelJogador = new ModelJogador();
            modelJogador.Id = jogador.Id;
            modelJogador.Nome = jogador.Nome;
            modelJogador.Telefone = jogador.Telefone;
            modelJogador.Nota = jogador.Nota;
            modelJogador.Posicao = jogador.Posicao;
            modelJogador.Status = jogador.Status;

            //Verificar pela Id se o jogador já existe na lista 1 de troca

            foreach (ModelJogador element in viewModel.ListaParaTroca1)
            {
                if (element.Id == modelJogador.Id)
                    exists = true;
            }

            if (!exists)
                viewModel.ListaParaTroca1.Add(modelJogador);
        }
    }

    void OnCheckBoxCheckedChanged2(object sender, CheckedChangedEventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        ModelJogador jogador = (ModelJogador)checkBox.BindingContext;
        bool exists = false;

        if (checkBox.IsChecked == true)
        {
            //Preenche lista de troca 1
            ModelJogador modelJogador = new ModelJogador();
            modelJogador.Id = jogador.Id;
            modelJogador.Nome = jogador.Nome;
            modelJogador.Telefone = jogador.Telefone;
            modelJogador.Nota = jogador.Nota;
            modelJogador.Posicao = jogador.Posicao;
            modelJogador.Status = jogador.Status;

            //Verificar pela Id se o jogador já existe na lista 1 de troca

            foreach (ModelJogador element in viewModel.ListaParaTroca2)
            {
                if (element.Id == modelJogador.Id)
                    exists = true;
            }

            if (!exists)
                viewModel.ListaParaTroca2.Add(modelJogador);
        }
    }


    private void TrocarJogadores(object sender, EventArgs e)
    {
        // Verificar se o número de jogadores selecionados nas listas de troca não excede o número de jogadores nos times
        //if (viewModel.ListaParaTroca1.Count > viewModel.ObjTimeParaEditar.ListaJogador.Count ||
        //    viewModel.ListaParaTroca2.Count > viewModel.ObjTimeParaEditar2.ListaJogador.Count)
        //{
        //    // Exibir mensagem de erro informando que a troca excede o número de jogadores nos times
        //    DisplayAlert("Erro", "A troca excede o número de jogadores nos times.", "OK");
        //    return;
        //}

        // Realizar troca entre os jogadores selecionados nas listas de troca
        //TrocarJogadores(viewModel.ObjTimeParaEditar, viewModel.ObjTimeParaEditar2);

        // Atualizar a lista geral de times
        //AtualizarListaTimes();

        // Navegar para a página de finalização da edição passando o ViewModelTime atualizado
        Navigation.PushAsync(new MenuSorteioFinalizarEdicao(viewModel));
    }



    //private async void TrocarJogadores(object sender, EventArgs e)
    //{
    //    viewModel.CarregarListaTroca();
    //    await Navigation.PushAsync(new MenuSorteioFinalizarEdicao(viewModel));
    //}

}
