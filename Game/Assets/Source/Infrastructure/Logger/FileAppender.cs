using System;
using System.IO;
using System.Text;

namespace Infrastructure.Logger
{
    public class FileAppender
    {
        private readonly object _lock = new object();

        public string FileName { get; private set; }

        public FileAppender(string fileName)
        {
            FileName = fileName;
        }

        public bool Append(string content)
        {
            try
            {
                lock (_lock)
                {
                    using (var stream = File.Open(FileName, FileMode.Append, FileAccess.Write, FileShare.Read))
                    {
                        var bytes = Encoding.UTF8.GetBytes(content);
                        stream.Write(bytes, 0, bytes.Length);
                    } 
                }
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}