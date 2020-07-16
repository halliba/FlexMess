using System.Windows.Input;
using FlexMess.ViewModels;

namespace FlexMess
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width - 20;
            Top = desktopWorkingArea.Bottom - Height - 20;
            
            DataContext = new MainViewModel();
        }

        private MainViewModel ViewModel => DataContext as MainViewModel;

        private void InputBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            ViewModel?.SendMessageCommand.Execute(InputBox.Text);
            InputBox.Clear();
        }
    }
}