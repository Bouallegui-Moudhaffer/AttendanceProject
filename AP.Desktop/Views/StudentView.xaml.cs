using AP.Desktop.ViewModels;
using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
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

namespace AP.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
            DataContext = new StudentViewModel();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Attendance Status")
            {
                var student = e.Row.Item as Student;
                if (student != null)
                {
                    var editingElement = e.EditingElement as TextBox;
                    string statusText = editingElement.Text;

                    // Assuming AttendanceStatus is an enum and statusText matches the enum names
                    if (Enum.TryParse(statusText, out AttendanceStatus newStatus))
                    {
                        (DataContext as StudentViewModel)?.UpdateAttendanceStatus(student, newStatus);
                    }
                    else
                    {
                        // Handle the case where the text doesn't match any enum value
                        // This could be logging the error, showing a message to the user, etc.
                    }
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MyTextBox.Text))
            {
                PlaceholderTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is DataGrid grid)
            {
                grid.CommitEdit(); // Commits the current edit on the cell
                grid.CommitEdit(DataGridEditingUnit.Row, true); // Commits the edit on the row

                // Force update to the JSON file
                (DataContext as StudentViewModel)?.SaveChanges();
            }
        }
    }
}
