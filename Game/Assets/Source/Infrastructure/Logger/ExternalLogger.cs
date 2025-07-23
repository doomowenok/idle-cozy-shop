using System;
using System.IO;
using UnityEngine;

namespace Infrastructure.Logger
{
    public class ExternalLogger : IDisposable
    {
        private FileWriter _writer;
        private string _logsDirectory;
        
        public void Initialize()
        {
            _logsDirectory = $"{Application.persistentDataPath}/Logs";

            if (!Directory.Exists(_logsDirectory))
            {
                Directory.CreateDirectory(_logsDirectory);
            }
            
            _writer = new FileWriter(_logsDirectory);
            
#pragma warning disable UDR0005
            Application.logMessageReceivedThreaded += WriteLogsToFile;
#pragma warning restore UDR0005
        }

        private void WriteLogsToFile(string condition, string stackTrace, LogType type)
        {
            _writer.Write(new LogMessage() { Type = type, Message = condition, Time = DateTime.UtcNow });
            
            if (type == LogType.Exception)
            {
                _writer.Write(new LogMessage() { Type = type, Message = stackTrace, Time = DateTime.UtcNow });
            }
        }

        public void Dispose()
        {
            _writer.Dispose();
            
            Application.logMessageReceived -= WriteLogsToFile;
        }
    }
}