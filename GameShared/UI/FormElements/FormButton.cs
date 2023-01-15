using UglyTgApplication.States;

namespace GameShared.UI.FormElements;

public class FormButton<T> : IFormElement
{
    private ViewModel _viewModel;
    
    public String Label { get; set; }

    public T Data { get; set; }
    
    private String ActionString { get; set; }
    public FormButton(ViewModel viewModel, String label, Action<T> callBack, T data)
    {
        Data = data;
        var actionCounter = ElementHelper.uid;
        Label = label;

        _viewModel = viewModel;
        ActionString = $"/action_{actionCounter}";
        
        viewModel.RegisterCallback(ActionString,
            update => callBack(Data) );
    }

    public string Display()
    {
        return $"{Label} \t {ActionString}";
    }

    public override string ToString()
    {
        return Display();
    }
}