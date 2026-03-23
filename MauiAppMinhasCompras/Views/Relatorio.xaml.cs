using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class Relatorio : ContentPage
{
    ObservableCollection<Produto> lista_filtrada = new ObservableCollection<Produto>();

    public Relatorio()
    {
        InitializeComponent();
        lst_relatorio.ItemsSource = lista_filtrada;

        // Define datas padrão (início do mês até hoje)
        dtp_inicio.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        pck_categoria_filtro.SelectedIndex = 0; // "Todas"
    }

    protected async override void OnAppearing()
    {
        await CarregarDados();
    }

    private async void FiltroChanged(object sender, EventArgs e)
    {
        await CarregarDados();
    }

    private async Task CarregarDados()
    {
        try
        {
            List<Produto> todosProdutos = await App.Db.GetAll();

            // Aplicando Filtros com LINQ
            var query = todosProdutos.Where(p => p.DataCadastro.Date >= dtp_inicio.Date &&
                                                p.DataCadastro.Date <= dtp_fim.Date);

            if (pck_categoria_filtro.SelectedItem?.ToString() != "Todas")
            {
                string cat = pck_categoria_filtro.SelectedItem.ToString();
                query = query.Where(p => p.Categoria == cat);
            }

            // Atualiza a lista na tela
            lista_filtrada.Clear();
            foreach (var p in query.ToList())
            {
                lista_filtrada.Add(p);
            }


            double soma = lista_filtrada.Sum(i => i.Total);
            lbl_total_filtrado.Text = $"Total no Período: {soma:C}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}