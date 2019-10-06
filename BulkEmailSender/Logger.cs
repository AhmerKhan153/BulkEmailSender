using System;
using System.IO;

namespace BulkEmailSender
{
    public static class Logger
    {
        public static void Write(string message, string stackTrace, LogType type)
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            string error = Path.Combine(baseDirectory, "Errors");
            string info = Path.Combine(baseDirectory, "Logs");
            ValidateDirectories(new string[] { error, info });
            error = Path.Combine(error, $"{DateTime.Now.Date.ToString("dd-MM-yyyy")}.txt");
            info = Path.Combine(info, $"{DateTime.Now.Date.ToString("dd-MM-yyyy")}.txt");
            switch (type)
            {
                case LogType.Error:

                    if (!File.Exists(error))
                    {
                        File.Create(error).Dispose();
                    }
                    File.AppendAllText(error, $"{DateTime.Now.TimeOfDay} {message} {Environment.NewLine} {stackTrace} {Environment.NewLine}");
                    break;
                case LogType.Information:
                    if (!File.Exists(info))
                    {
                        File.Create(info).Dispose();
                    }
                    File.AppendAllText(info, $"{DateTime.Now.TimeOfDay} {message} {Environment.NewLine} ");

                    break;
                default:
                    break;
            }
        }

        public static void ValidateDirectories(string[] path)
        {
            foreach (var item in path)
            {
                if (Directory.Exists(item)) { continue; }
                Directory.CreateDirectory(item);

            }
        }
    }

    public enum LogType
    {
        Error = 0,
        Information = 1,
    }
}
