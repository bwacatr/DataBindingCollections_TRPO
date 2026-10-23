using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingCollections_TRPO
{
    public class BudgetEstimate : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }

        int id = 0;
        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }


        string name = "Название";
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EstimateTotal));
                }
            }
        }

        string project = "Название проекта";
        public string Project
        {
            get => project;
            set
            {
                if (project != value)
                {
                    project = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EstimateTotal));
                }
            }
        }

        string responsible = "Имя ответственного";
        public string Responsible
        {
            get => responsible;
            set
            {
                if (responsible != value)
                {
                    responsible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(EstimateTotal));
                }
            }
        }

        public ObservableCollection<ExpenseItem> Expenses { get; set; } = new();

        void NotifyCollectionChangedEventHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var item in Expenses)
            {
                estimateTotal += item.ExpenseTotal;
            }
        }

        decimal estimateTotal;
        public decimal EstimateTotal
        {
            get 
            {
                foreach (var item in Expenses) 
                {
                    estimateTotal += item.ExpenseTotal;
                }
                
                return estimateTotal;
            }
            set 
            {
                if (estimateTotal != value)
                {
                    estimateTotal = value;
                    OnPropertyChanged();
                    
                }
            }
            
        }
    }
}

