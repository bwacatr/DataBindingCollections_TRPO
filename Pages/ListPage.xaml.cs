using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBindingCollections_TRPO.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }

        private int currentId = 0;

        public BudgetEstimateList estimateList = new BudgetEstimateList();

        private BudgetEstimate _budgetEstimate = new BudgetEstimate();
        public BudgetEstimate budgetEstimate
        {
            get { return _budgetEstimate; }

            set
            {
                if (_budgetEstimate != value)
                {
                    _budgetEstimate = value;
                    OnPropertyChanged();

                }


            }
        }





        public ExpenseItem? SelectedItem { get; set; }

        public ListPage()
        {
            InitializeComponent();


            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    estimateList.Load(openFileDialog.FileName);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ошибка загрузки: "+ex.Message);
                }
                
            }

            if (estimateList.budgetEstimates.Count > 0)
            {
                budgetEstimate = estimateList.budgetEstimates[0];
            }
            else
            {
                budgetEstimate.Expenses.Add(new ExpenseItem() { Name = "карандаш", Count = 3, UnitPrice = 10 });

                estimateList.budgetEstimates.Add(budgetEstimate);
            }
            DataContext = this;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    estimateList.Save(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ошибка сохранения: " + ex.Message);
                }

            }
        }

        private void NewBudgetEstimateButton_Click(object sender, RoutedEventArgs e)
        {
            int count = estimateList.budgetEstimates.Count;
            estimateList.budgetEstimates.Add(new BudgetEstimate { Id = count });
            budgetEstimate = estimateList.budgetEstimates[count];

            currentId = count;

        }

        private void NextBudgetEstimateButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentId < estimateList.budgetEstimates.Count - 1)
            {
                currentId++;

            }
            else
            {
                currentId = 0;

            }

            budgetEstimate = estimateList.budgetEstimates[currentId];

        }

        private void PreviousBudgetEstimateButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentId <= estimateList.budgetEstimates.Count - 1 && currentId > 0)
            {
                currentId--;

            }
            else
            {
                currentId = estimateList.budgetEstimates.Count - 1;

            }

            budgetEstimate = estimateList.budgetEstimates[currentId];
        }
        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Предмет не выбран");
                return;
            }


            bool confirm = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

            if (confirm)
            {
                budgetEstimate.Expenses.Remove(SelectedItem);
            }
        }

        private void EditItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new ItemFormPage(budgetEstimate.Expenses, SelectedItem));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ItemFormPage(budgetEstimate.Expenses));
        }
    }
}
