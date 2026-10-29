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
    /// Логика взаимодействия для UserFormPage.xaml
    /// </summary>
    public partial class ItemFormPage : Page
    {
        private ExpenseItem _item;
        private ObservableCollection<ExpenseItem> _itemList;
        private ExpenseItem? _originalItem;

        public ItemFormPage(ObservableCollection<ExpenseItem> ItemList, ExpenseItem? item = null)
        {
            _originalItem = item;

            _item = item == null ? new ExpenseItem() : new ExpenseItem
            {
                Name = item.Name,
                Count = item.Count,
                UnitPrice = item.UnitPrice
            };

            _itemList = ItemList;
            InitializeComponent();

            DataContext = _item;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_originalItem == null)
            {
                _itemList.Add(_item);
                BackButton_Click(sender, e);
                return;
            }

            _originalItem.Name = _item.Name;
            _originalItem.Count = _item.Count;
            _originalItem.UnitPrice = _item.UnitPrice;
            BackButton_Click(sender, e);

        }
    }
}
