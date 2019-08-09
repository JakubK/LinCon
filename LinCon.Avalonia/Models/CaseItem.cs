using ReactiveUI;

namespace LinCon.Avalonia.Models
{
    public class CaseItem : ReactiveObject
    {
      public int ID {get;set;}

      string name;
      public string Name
      {
        get => name;
        set => this.RaiseAndSetIfChanged(ref name,value);
      }
    }
}