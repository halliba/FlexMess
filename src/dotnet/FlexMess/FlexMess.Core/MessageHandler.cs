using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FlexMess.Core.Models;
using Newtonsoft.Json;

namespace FlexMess.Core
{
    public class MessageHandler : IDisposable
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _remoteEndPoint;
        private readonly Encoding _textEncoding = Encoding.UTF8;
        
        public Action<string> Logger { get; set; }

        public event EventHandler<Message> MessageReceived;
        public event EventHandler<KeepAlive> KeepAliveReceived;  

        public MessageHandler(int port)
        {
            if (port < 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
            _udpClient = new UdpClient(port);
            _remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, port);
            _listen();
        }

        public MessageHandler(int port, IPEndPoint remoteEndPoint)
        {
            if (port < 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
            _remoteEndPoint = remoteEndPoint ?? throw new ArgumentNullException(nameof(remoteEndPoint));
            _udpClient = new UdpClient(port);
            _listen();
        }

        private async void _listen()
        {
            while (_udpClient != null)
            {
                var udpResult = await _udpClient.ReceiveAsync();
                Log($"Received packet with length '{udpResult.Buffer.Length}' from '{udpResult.RemoteEndPoint.Address}'.");
                var stringResult = _textEncoding.GetString(udpResult.Buffer);
                Log($"Received packet '{stringResult}' from '{udpResult.RemoteEndPoint.Address}'.");
                ParseModel(stringResult);
            }
        }

        private void ParseModel(string modelText)
        {
            if (string.IsNullOrEmpty(modelText)) return;
            var modelBase = JsonConvert.DeserializeObject<ModelBase>(modelText);
            switch (modelBase.Type)
            {
                case ModelType.Message:
                    var messageModel = JsonConvert.DeserializeObject<MessageModel>(modelText);
                    var message = new Message(messageModel);
                    MessageReceived?.Invoke(this, message);
                    break;
                case ModelType.KeepAlive:
                    var keepAliveModel = JsonConvert.DeserializeObject<KeepAliveModel>(modelText);
                    var keepAlive = new KeepAlive(keepAliveModel);
                    KeepAliveReceived?.Invoke(this, keepAlive);
                    break;
                default:
                    return;
            }
        }

        public Message Send(Guid userId, string userName, string text)
        {
            var messageModel = new MessageModel
            {
                UserName = userName,
                Text = text,
                UserId = userId
            };
            SendInternalAsync(messageModel);
            var message = new Message(messageModel);
            return message;
        }

        private async void SendInternalAsync(MessageModel messageModel)
        {
            var stringData = JsonConvert.SerializeObject(messageModel);
            var byteData = _textEncoding.GetBytes(stringData);
            await _udpClient.SendAsync(byteData, byteData.Length, _remoteEndPoint);
            Log($"Message '{messageModel.Text}' send successfully.");
        }

        private void Log(string message)
        {
            try
            {
                Logger?.Invoke(message);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Dispose()
        {
            _udpClient?.Dispose();
        }
    }
}