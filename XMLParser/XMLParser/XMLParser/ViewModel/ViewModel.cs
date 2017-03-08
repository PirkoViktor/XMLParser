
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
namespace XMLParser.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
    public class ViewModelWindow: ViewModelBase
    {   
        public ViewModelWindow()
        {
            this.TableHeaders= new ObservableCollection<string> { "a","b","c","d"};
        }
        public DataSet TableData { get; set; }
        public ObservableCollection<string> TableHeaders { get; set; }
    }
}
