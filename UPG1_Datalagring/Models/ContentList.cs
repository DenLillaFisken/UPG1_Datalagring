using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPG1_Datalagring.Models
{
    public class ContentList : ObservableCollection<Content>
    {

        public ContentList()
        {

        }
        public ContentList(string text)
        {
            Add(new Content(text));
        }
    }
}
