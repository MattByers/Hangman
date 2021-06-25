using System.IO;

namespace Hangman.Models
{
  public abstract class PersistableModel
  {
    private readonly string _filePath;
    
    protected PersistableModel(string filePath)
    {
      _filePath = filePath;
    }
    
    protected void Persist()
    {
      File.WriteAllText(_filePath, GetState());
    }

    protected abstract string GetState();
  }
}