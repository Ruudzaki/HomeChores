using HomeChores.UI.ViewModels;

namespace HomeChores.UI;

public partial class DailyChoresPage : ContentPage
{
    private readonly DailyChoresViewModel _viewModel;

    public DailyChoresPage(DailyChoresViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
}