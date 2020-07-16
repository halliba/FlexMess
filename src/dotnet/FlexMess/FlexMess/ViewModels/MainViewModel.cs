using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using FlexMess.Core;

namespace FlexMess.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly MessageHandler _messageHandler;
        private ICommand _sendMessageCommand;

        public ICommand SendMessageCommand => _sendMessageCommand ??
                                              (_sendMessageCommand =
                                                  new DelegateCommand(o => SendMessage(o as string)));
        
        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();
        
        public MainViewModel()
        {
            _messageHandler = new MessageHandler(8888);
            _messageHandler.Logger += s => Debug.Print(s);
            _messageHandler.MessageReceived += MessageHandlerOnMessageReceived;
            _messageHandler.KeepAliveReceived += MessageHandlerOnKeepAliveReceived;
        }

        private void MessageHandlerOnKeepAliveReceived(object sender, KeepAlive keepAlive)
        {
            
        }

        private void MessageHandlerOnMessageReceived(object sender, Message message)
        {
            if (message == null || message.Sender.UserId == Globals.UserId) return;
            Messages.Add(message);
        }
        
        private void SendMessage(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return;
            var message = _messageHandler.Send(Globals.UserId, Globals.UserName, s.Trim());
            Messages.Add(message);
        }
    }
}