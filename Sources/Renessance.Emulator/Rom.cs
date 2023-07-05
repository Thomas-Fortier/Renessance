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
    if (File.Exists(path))
      return new Rom(new FileInfo(path), File.ReadAllBytes(path));
    
    const string MESSAGE = "Could not find the specified ROM file.";
    _log.Error(MESSAGE + $" '{path}'");
    throw new FileNotFoundException(MESSAGE, path);
  }
}