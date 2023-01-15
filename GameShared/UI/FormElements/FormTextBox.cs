using GameShared.UI.Elements;
using UglyTgApplication.States;

namespace GameShared.UI.FormElements;

public class FormTextBox : IFormElement
{
    private ViewModel _viewModel;
    
    public String Label { get; set; }
    public String Text { get; set; } = "";
    
    private String ActionString { get; set; }
    public FormTextBox(ViewModel viewModel, String label)
    {
        var actionCounter = ElementHelper.uid;
        Label = label;

        _viewModel = viewModel;
        ActionString = $"/action_{actionCounter}";
        
        viewModel.RegisterCallback(ActionString,
            update => viewModel.Push(new UiTextDialog("Enter text:",
                s =>
                {
                    Text = s.Replace("\n","");
                    viewModel.UpdateView();
                },
                s => String.Empty)));
    }

    public string Display()
    {
        return $"{Label} \t [<code>{Text}</code>] \t {ActionString}";
    }

    public override string ToString()
    {
        return Display();
    }
}