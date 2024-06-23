using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace calculator.DataVault
{
    public class Function : INotifyPropertyChanged
    {
        private string name;
        private string expression;
        private ObservableCollection<Parameter> parameters;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Expression
        {
            get => expression;
            set
            {
                if (expression != value)
                {
                    expression = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Parameter> Parameters
        {
            get => parameters;
            set
            {
                if (parameters != value)
                {
                    parameters = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Function()
        {
            Parameters = new ObservableCollection<Parameter>();
        }
    }
}
