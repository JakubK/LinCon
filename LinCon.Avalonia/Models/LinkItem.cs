using ReactiveUI;

namespace LinCon.Avalonia.Models
{
    public class LinkItem : ReactiveObject
    {
      string name;
      public string Name
      {
        get => name;
        set => this.RaiseAndSetIfChanged(ref name, value);
      }

      string url;
      public string Url
      {
        get => url;
        set => this.RaiseAndSetIfChanged(ref url,value);
      }
    }
}