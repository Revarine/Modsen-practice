using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace calculator.DataVault
{
   public  class InputField : INotifyPropertyChanged
    {
        private string _myText;
        public string MyText
        {
            get { return _myText; }
            set
            {
                _myText = value;
                OnPropertyChanged(nameof(MyText));
           
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
