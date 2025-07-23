using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Infrastructure.Logger
{
    public class FileWriter : IDisposable
    {
        private readonly string _filePath;
        private readonly Thread _thread;

        private FileAppender _appender;

        private readonly ConcurrentQueue<LogMessage> _messages = new ConcurrentQueue<LogMessage>();
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(true);

        private bool _disposing;
        
        public FileWriter(string folder)
        {
            _filePath = $"{folder}/{DateTime.UtcNow.ToString(LoggerConstants.DateFormat)}.log";

            _thread = new Thread(StoreMessages)
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal,
            };
            _thread.Start();
        }

        private void StoreMessages()
        {
            while (!_disposing)
            {
                while (!_messages.IsEmpty)
                {
                    try
                    {
                        LogMessage message;
                        
                        if (!_messages.TryPeek(out message))
                        {
                            Thread.Sleep(5);
                        }

                        if (_appender == null || _appender.FileName != _filePath)
                        {
                            _appender = new FileAppender(_filePath);
                        }
                        
                        var messageToWrite = string.Format(
                            LoggerConstants.LogTimeFormat,
                            message.Time,
                            message.Type,
                            message.Message);

                        if (_appender.Append(messageToWrite))
                        {
                            _messages.TryDequeue(out message);
                        }
                        else
                        {
                            Thread.Sleep(5);
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                _resetEvent.Reset();
                _resetEvent.WaitOne(500);
            }
        }

        public void Write(LogMessage message)
        {
            try
            {
                if (message.Message.Length > LoggerConstants.MaxMessageLength)
                {
                    string preview = message.Message.Substring(0, LoggerConstants.MaxMessageLength);
                    LogMessage previewMessage = new LogMessage()
                    {
                        Type = message.Type,
                        Message = string.Format($"Message is too long {message.Message.Length}. Preview: {preview}"),
                        Time = message.Time,
                    };
                    _messages.Enqueue(previewMessage);
                }
                else
                {
                    _messages.Enqueue(message);
                }
                
                _resetEvent.Set();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void Dispose()
        {
            _disposing = true;
            
            _thread?.Abort();
            
            GC.SuppressFinalize(this);
        }
    }
}