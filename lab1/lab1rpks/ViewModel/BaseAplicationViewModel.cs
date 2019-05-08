using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab1rpks.ViewModel
{
    public abstract class BaseAplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
