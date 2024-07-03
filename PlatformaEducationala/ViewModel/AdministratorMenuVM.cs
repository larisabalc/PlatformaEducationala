using PlatformaEducationala.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PlatformaEducationala.View;

namespace PlatformaEducationala.ViewModel
{
    public class AdministratorMenuVM
    {
        private ICommand _openAdministratorUser;
        public ICommand OpenAdministratorUser
        {
            get
            {
                if (_openAdministratorUser == null)
                    _openAdministratorUser = new RelayCommands(OpenAdministratorUserMethod);
                return _openAdministratorUser;
            }
        }
        private void OpenAdministratorUserMethod(object parameter)
        {
            AdministratorWindowUsers administratorWindowUsers = new AdministratorWindowUsers();
            administratorWindowUsers.Show();
        }

        private ICommand _openAdministratorOther;
        public ICommand OpenAdministratorOther
        {
            get
            {
                if (_openAdministratorOther == null)
                    _openAdministratorOther = new RelayCommands(OpenAdministratorOtherMethod);
                return _openAdministratorOther;
            }
        }
        private void OpenAdministratorOtherMethod(object parameter)
        {
            AdministratorWindowOther administratorWindowUsers = new AdministratorWindowOther();
            administratorWindowUsers.Show();
        }

        private ICommand _OpenAdministratorAsocieri;
        public ICommand OpenAdministratorAsocieri
        {
            get
            {
                if (_OpenAdministratorAsocieri == null)
                    _OpenAdministratorAsocieri = new RelayCommands(OpenAdministratorAsocieriMethod);
                return _OpenAdministratorAsocieri;
            }
        }
        private void OpenAdministratorAsocieriMethod(object parameter)
        {
            AdministratorAsocieriWindow administratorAsocieriWindow = new AdministratorAsocieriWindow();
            administratorAsocieriWindow.Show();
        }

        private ICommand _openAdministratorAsocieriOther;
        public ICommand OpenAdministratorAsocieriOther
        {
            get
            {
                if (_openAdministratorAsocieriOther == null)
                    _openAdministratorAsocieriOther = new RelayCommands(OpenAdministratorAsocieriOtherMethod);
                return _openAdministratorAsocieriOther;
            }
        }
        private void OpenAdministratorAsocieriOtherMethod(object parameter)
        {
            AdministratorAsocieriOtherWindow administratorAsocieriOtherWindow = new AdministratorAsocieriOtherWindow();
            administratorAsocieriOtherWindow.Show();
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
    }
}
