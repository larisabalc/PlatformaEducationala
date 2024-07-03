using PlatformaEducationala.Commands;
using PlatformaEducationala.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlatformaEducationala.ViewModel
{
    public class ProfesorMenuVM
    {

        private ICommand _openProfesor;
        public ICommand OpenProfesor
        {
            get
            {
                if (_openProfesor == null)
                    _openProfesor = new RelayCommands(OpenProfesorMethod);
                return _openProfesor;
            }
        }
        private void OpenProfesorMethod(object parameter)
        {
            ProfesorVM profesorVM = new ProfesorVM(idProfesor);
            ProfesorWindow window = new ProfesorWindow();
            window.DataContext = profesorVM;
            window.Show();
        }

        private ICommand _openCalculareMedii;
        public ICommand OpenCalculareMedii
        {
            get
            {
                if (_openCalculareMedii == null)
                    _openCalculareMedii = new RelayCommands(OpenCalculareMediiMethod);
                return _openCalculareMedii;
            }
        }
        private void OpenCalculareMediiMethod(object parameter)
        {
            CalculareMediiVM calculareMediiVM = new CalculareMediiVM(idProfesor);
            CalculareMediiWindow window = new CalculareMediiWindow();
            window.DataContext = calculareMediiVM;
            window.Show();
        }

        private ICommand _LogOut;
        public ICommand LogOut
        {
            get
            {
                if (_LogOut == null)
                    _LogOut = new RelayCommands(LogOutMethod);
                return _LogOut;
            }
        }
        private void LogOutMethod(object parameter)
        {
            Window currentWindow = Application.Current.MainWindow;
            currentWindow.Hide();
            LoginWindow loginWindow = new LoginWindow();
            Application.Current.MainWindow = loginWindow;
            currentWindow = Application.Current.MainWindow;
            currentWindow.Show();
        }

        private int idProfesor;

        public ProfesorMenuVM(int idProfesor)
        {
            this.idProfesor = idProfesor;
        }

        public ProfesorMenuVM()
        {
        }
    }
}
