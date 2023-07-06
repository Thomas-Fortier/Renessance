using System.Reflection;
using log4net;
using log4net.Config;
using Renessance.Hardware;

namespace Renessance.Emulator;

internal static class Program
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
  
  private static void Main()
  {
    InitializeLogger();

    var finishedWithErrors = false;

    try
    {
      var system = NesSystemFactory.CreateNesSystem();
      var rom = Rom.LoadRomFromDisk(@"C:\Users\Thomas\Desktop\cpu_dummy_reads.nes");
      
      system.LoadRom(rom.Data);

      while (true)
      {
        system.ExecuteCycle();
      }
    }
    catch (Exception exception)
    {
      _log.Error("An unhandled exception has occured. See exception for details.", exception);
      finishedWithErrors = true;
    }

    if (finishedWithErrors)
      _log.Warn("Finished execution with error.");
    else
      _log.Info("Finished execution.");
  }

  private static void InitializeLogger()
  {
    const string LOG4NET_CONFIG = "log4net.config";
    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

    if (!File.Exists(LOG4NET_CONFIG))
      throw new FileNotFoundException("Could not find the log4net configuration file.", LOG4NET_CONFIG);
    
    XmlConfigurator.Configure(logRepository, new FileInfo(LOG4NET_CONFIG));
  }
}