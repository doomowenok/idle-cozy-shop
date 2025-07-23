using System;
using UnityEngine;

namespace Infrastructure.Logger
{
    public struct LogMessage
    {
        public LogType Type;
        public DateTime Time;
        public string Message;
    }
}