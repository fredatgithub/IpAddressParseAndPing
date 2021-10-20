using System;
using System.Diagnostics;
using System.IO;

namespace ParseIPAddress
{
  class Program
  {
    static void Main(string[] arguments)
    {
      Action<string> Display = Console.WriteLine;
      if (arguments.Length == 0)
      {
        Display("You need to enter a file name as the first argument.");
        Display("Example:");
        Display("ParseIPAddress myTextFile.txt");
        return;
      }

      // reading the file which is in the first argument
      // text file is like this:
      // IP Address: x.x.x.x with subnet: x.x.x.x
      string fileContent = string.Empty;
      try
      {
        using (StreamReader streamReader = new StreamReader(arguments[0]))
        {
          fileContent = streamReader.ReadToEnd();
        }
      }
      catch (Exception)
      {
        Display("there was an error while trying to read the file");
      }

      string[] fileContentArray = fileContent.Split(':');
      string ipAddress = fileContentArray[1];
      ipAddress = ipAddress.Replace("with subnet", "").Trim();
      Display($"The IP address is {ipAddress}");
      Display("Now let's ping this IP address in a new window");
      StartProcess("ping", ipAddress);
      Display("Press any key to exit:");
      Console.ReadKey();
    }

    public static void StartProcess(string dosScript, string arguments = "", bool useShellExecute = true, bool createNoWindow = false)
    {
      Process task = new Process
      {
        StartInfo =
        {
          UseShellExecute = useShellExecute,
          FileName = dosScript,
          Arguments = arguments,
          CreateNoWindow = createNoWindow
        }
      };

      task.Start();
    }
  }
}
