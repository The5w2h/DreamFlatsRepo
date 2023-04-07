using System;
namespace DreamFlats.Logging
{
    public class Logging : ILogging
    {
        public Logging()
        {
        }

        public void Log(string message, string type)
        {
            //throw new NotImplementedException();
            if (type == "error")
            {
                Console.WriteLine("ERROR - " + message);
            }

            else
            {
                Console.WriteLine(message);
            }
        }
    }
}

