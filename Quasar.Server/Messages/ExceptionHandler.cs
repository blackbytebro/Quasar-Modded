using Quasar.Common.Messages;
using Quasar.Common.Messages.other;
using Quasar.Common.Networking;
using Quasar.Server.Forms;
using Quasar.Server.Networking;
using System.Windows.Forms;

namespace Quasar.Server.Messages
{
    public class ExceptionHandler : MessageProcessorBase<object>
    {
        public delegate void ClientExceptionRaisedEventHandler(object sender, ExceptionMessage message);
        public event ClientExceptionRaisedEventHandler ClientExceptionRaised;
        public ExceptionHandler() : base(true)
        {

        }

        private void OnClientExceptionRaised(ExceptionMessage message)
        {
            SynchronizationContext.Post(e =>
            {
                var handler = ClientExceptionRaised;
                handler?.Invoke(this, (ExceptionMessage)e);
            }, message);
        }

        public override bool CanExecute(IMessage message) => message is ExceptionMessage;

        public override bool CanExecuteFrom(ISender sender) => true;

        public override void Execute(ISender sender, IMessage message)
        {
            switch (message)
            {
                case ExceptionMessage exception:
                    Execute(sender, exception);
                    break;
            }
        }

        private void Execute(ISender sender, ExceptionMessage exception)
        {
            MessageBox.Show($"Client Exception:\n{exception.Raw}", "Client Exception!");
            OnClientExceptionRaised(exception);
        }
    }
}
