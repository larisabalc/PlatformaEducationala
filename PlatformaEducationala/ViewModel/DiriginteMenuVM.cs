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
    public class DiriginteMenuVM
    {
        private ICommand _OpenAbsente;
        public ICommand OpenAbsente
        {
            get
            {
                if (_OpenAbsente == null)
                    _OpenAbsente = new RelayCommands(OpenAbsenteMethod);
                return _OpenAbsente;
            }
        }
        private void OpenAbsenteMethod(object parameter)
        {
            DiriginteAbsenteVM diriginteAbsenteVM = new DiriginteAbsenteVM(idDiriginte);
            DiriginteAbsenteWindow window = new DiriginteAbsenteWindow();
            window.DataContext = diriginteAbsenteVM;
            window.Show();
        }

        private ICommand _openMedii;
        public ICommand OpenMedii
        {
            get
            {
                if (_openMedii == null)
                    _openMedii = new RelayCommands(OpenMediiMethod);
                return _openMedii;
            }
        }
        private void OpenMediiMethod(object parameter)
        {
            DiriginteMediiVM diriginteMediiVM = new DiriginteMediiVM(idDiriginte);
            DiriginteMediiWindow window = new DiriginteMediiWindow();
            window.DataContext = diriginteMediiVM;

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

        private int idDiriginte;

        public DiriginteMenuVM(int idDiriginte)
        {
            this.idDiriginte = idDiriginte;
        }

        public DiriginteMenuVM()
        {
        }
    }
}
