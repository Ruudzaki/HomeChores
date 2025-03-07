using HomeChores.UI.ViewModels;

namespace HomeChores.UI;

public partial class MainPage : ContentPage
{
    private readonly ChoreViewModel _viewModel;

    public MainPage(ChoreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadChores();
    }

    private async void AddChore_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ChoreTitleEntry.Text))
        {
            await _viewModel.AddChore(ChoreTitleEntry.Text);
            ChoreTitleEntry.Text = string.Empty;
            await _viewModel.LoadChores(); // If you add a method to refresh
        }
    }
}