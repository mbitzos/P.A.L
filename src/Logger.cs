using System;
using System.IO;
using Newtonsoft.Json;

namespace AI {

    /// <summary>
    /// Provides indepth logging mecanisms for AI program
    /// </summary>
    public class Logger {

        const string LogFilePathPrefix = "./logs/log";
        const string LogFilePathPostFix = ".log";

        string currentLog ="";

        public LogObject Buffer;

        // gets the total path of the log file
        string LogFilePath {get {
            return LogFilePathPrefix + "-" + currentLog + LogFilePathPostFix;
        }}

        /// <summary>
        /// Inits Logger
        /// </summary>
        public Logger() {
            currentLog = Guid.NewGuid().ToString();
            Buffer = new LogObject();
            Console.WriteLine("Finished Setting Up Log: " + LogFilePath);
        }


        /// <summary>
        /// Logs the buffer to the file
        /// </summary>
        /// <param name="clearBuffer">If the buffer should be cleared after</param>
        public void LogBuffer(bool clearBuffer=true) {

            // log the serialized json
            LogLine(Buffer.ToString());

            if (clearBuffer)
                ClearBuffer();
        }

        /// <summary>
        /// Clear the current log buffer line
        /// </summary>
        public void ClearBuffer() {
            Buffer = new LogObject();
        }

        /// <summary>
        /// Writes text to the log file (not new line)
        /// </summary>
        /// <param name="text">The text to write</param>
        public void Log(string text) {
            using (var file = new StreamWriter(LogFilePath, true))
                {
                    file.Write(text);
                }
        }

        /// <summary>
        /// Writes on a new line to the log file
        /// </summary>
        /// <param name="text">The text to write</param>
        public void LogLine(string text) {
            using (var file = new StreamWriter(LogFilePath, true))
                {
                    file.WriteLine(text);
                }
        }

        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="error">The error to log</param>
        public void LogError(Exception error) {
            LogLine("ERROR:" + error.ToString() + "," + error.Message);
        }
    }
}