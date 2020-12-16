using OverviewRkiData.Components.Ui.Eventbus;

namespace OverviewRkiData.Commands
{
    public class BaseMessage : IMessageContainer
    {
        public BaseMessage(object content)
        {
            this.Content = content;
        }
        public object Content { get; }
    }
}
