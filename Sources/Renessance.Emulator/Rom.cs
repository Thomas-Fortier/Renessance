using log4net;

namespace Renessance.Emulator;

public class Rom
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(Rom)); 
  
  public FileInfo RomInfo { get; private set; }
  public byte[] Data { get; private set; }
  
  private Rom(FileInfo fileInfo, byte[] data)
  {
    RomInfo = fileInfo;
    Data = data;
  }

  public static Rom LoadRomFromDisk(string path)
  {
    _log.Info($"Loading ROM file '{path}'...");

    if (File.Exists(path))
    {
      var rom = new Rom(new FileInfo(path), File.ReadAllBytes(path));
      _log.Info($"Loaded ROM ({rom.RomInfo.Name}) successfully.");
      
#if DEBUG
      _log.Debug($"ROM is {rom.Data.Length} bytes in length.");
#endif

      return rom;
    }
    
    const string MESSAGE = "Could not find the specified ROM file.";
    _log.Error(MESSAGE + $" '{path}'");
    throw new FileNotFoundException(MESSAGE, path);
  }
}