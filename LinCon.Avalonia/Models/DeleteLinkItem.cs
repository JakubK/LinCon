using LinCon.Core.Models;

namespace LinCon.Avalonia.Models
{
    public class DeleteLinkItem
    {
      public string Url {get;set;}
      public string Name {get;set;}
      public bool IsChecked {get;set;} = false;
    }
}