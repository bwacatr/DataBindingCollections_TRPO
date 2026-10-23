using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingCollections_TRPO
{
    public class ExpenseItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            
        }

        int count;
        public int Count
        {
            get => count;
            set
            {
                if (count != value)
                {
                    count = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ExpenseTotal));
                }
            }
        }

        decimal unitPrice;
        public decimal UnitPrice
        {
            get => unitPrice;
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ExpenseTotal));
                }
            }
        }

        decimal expenseTotal;
        public decimal ExpenseTotal
        {
            get => unitPrice * count;
            
        }




        string name = "";
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
    }
}
