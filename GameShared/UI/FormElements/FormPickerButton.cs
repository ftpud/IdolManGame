using GameShared.UI.Elements;
using UglyTgApplication.States;

namespace GameShared.UI.FormElements;

public class FormPickerButton<T> : IFormElement
{
    private ViewModel _viewModel;
    
    public String Label { get; set; }

    public T SelectedEntity { get; set; }
    public String SelectedText { get; set; }
    
    public Dictionary<String, T> Model { get; set; }

    private String ActionString { get; set; }
    public FormPickerButton(ViewModel viewModel, String label, Action<T> callBack, Dictionary<String, T> data)
    {
        Model = data;
        var actionCounter = ElementHelper.uid;
        Label = label;

        _viewModel = viewModel;
        ActionString = $"/action_{actionCounter}";

        var adoptedModel = data.ToDictionary(i => i.Key, i => (object)i.Value);
        
        viewModel.RegisterCallback(ActionString,
            update => viewModel.Push(new UiPickerDialog(label, adoptedModel, (s, o) =>
            {
                SelectedText = s;
                SelectedEntity = (T)o;
                callBack.Invoke((T)o);
            })) );
    }

    public string Display()
    {
        return $"{Label} \t [<code>{SelectedText}</code>] \t {ActionString}";
    }

    public override string ToString()
    {
        return Display();
    }
}