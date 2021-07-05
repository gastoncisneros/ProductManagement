using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Extensions.Logging;
using System;

namespace Product_Management_Client.Logging
{
    internal class LoggingClientMessageInspector : IClientMessageInspector
    {
        private readonly ILogger<object> _logger;

        public LoggingClientMessageInspector(ILogger<object> logger)
        {
            _logger = logger;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var correlationId = Guid.NewGuid();
            this.SaveLog(ref request, correlationId, "REQUEST");

            return correlationId;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var correlationId = (Guid)correlationState;
            this.SaveLog(ref reply, correlationId, "RESPONSE");
        }

        private void SaveLog(ref Message request, Guid correlationId, string suffix)
        {
            _logger.LogDebug($"{suffix + request}");
            _logger.LogTrace($"{suffix + request}");

            #region Write in file
            //var outputPath = "C:\\Users\\Juan Ignacio\\Documents\\Log.txt";
            //using (var buffer = request.CreateBufferedCopy(int.MaxValue))
            //{
            //    var directoryName = Path.GetDirectoryName(outputPath);
            //    if (directoryName != null)
            //    {
            //        Directory.CreateDirectory(directoryName);
            //    }
            //    using (var stream = File.Open(outputPath, FileMode.Append))
            //    {
            //        using (var message = buffer.CreateMessage())
            //        {
            //            using (var writer = XmlWriter.Create(stream))
            //            {
            //                message.WriteMessage(writer);
            //            }
            //        }
            //        var separador = "\r\n--------------------------------------------------------------------------------------------\r\n";
            //        stream.Write(Encoding.ASCII.GetBytes(separador), 0, separador.Length);
            //    }

            //    request = buffer.CreateMessage();
            //}
            #endregion
        }
    }
}