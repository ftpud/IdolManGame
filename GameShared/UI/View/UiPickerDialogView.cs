using GameShared.UI.Elements;
using Telegram.Bot.Types.ReplyMarkups;
using UglyAppFramework.DependencyManager.Attributes;
using UglyAppFramework.StateManage;
using UglyTgApplication.View;

namespace GameShared.UI.View;

[Managed]
public class UiPickerDialogView : IView
{
    public ViewResponse Display(IState viewModel)
    {
        UiPickerDialog testViewModel = (UiPickerDialog)viewModel;
        ViewResponse response = new ViewResponse();
        List<ResponseData> data = new List<ResponseData>();
        
        data.Add(new ResponseData()
            {
                text = "Выберите один из вариантов:",
                replyMarkup = ViewHelper.ButtonBuilder.BuildBackButton("Назад")
            }
        );
        
        int i = 0;
        foreach (var element in testViewModel.Model)
        {
            InlineKeyboardButton selectButton = new InlineKeyboardButton("Выбрать");
            selectButton.CallbackData = $"action_{i}";

            data.Add(new ResponseData()
                {
                    text = element.Key,
                    replyMarkup = new InlineKeyboardMarkup(
                        new[] { selectButton }
                    )
                }
            );
            i++;
        }
        
        

        response.ResponseMessages = data.ToArray();
        return response;
    }
}