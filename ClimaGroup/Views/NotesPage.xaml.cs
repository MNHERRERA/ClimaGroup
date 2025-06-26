namespace ClimaGroup.Views;

public partial class NotesPage : ContentPage
{
    public NotesPage()
    {
        InitializeComponent();
        BindingContext = new NotesViewModel();

    }
}