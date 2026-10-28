using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class ListPage : Page
    {


        public BudgetEstimate budgetEstimate { get; } = new BudgetEstimate();

        public ObservableCollection<ExpenseItem> Expenses { get; set; }



        public ExpenseItem? SelectedItem { get; set; }

        public ListPage()
        {

            Expenses = budgetEstimate.Expenses;
            
            InitializeComponent();
            DataContext = this;
            Expenses.Add(new ExpenseItem() { Name = "карандаш", Count = 3, UnitPrice = 10 });


        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Предмет не выбран");
                return;
            }

            
            bool confirm = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления",MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            
            if (confirm)
            {
                Expenses.Remove(SelectedItem);
            }
        }

        private void EditItem_Click(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new ItemFormPage(Expenses, SelectedItem));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ItemFormPage(Expenses));
        }
    }
}
