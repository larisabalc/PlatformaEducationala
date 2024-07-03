using PlatformaEducationala.Commands;
using PlatformaEducationala.Model;
using PlatformaEducationala.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PlatformaEducationala.ViewModel
{
    public class LoginVM : BaseVM
    {
        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private ICommand _loginUser;
        public ICommand LoginUser
        {
            get
            {
                if (_loginUser == null)
                    _loginUser = new RelayCommands(SelectUser);
                return _loginUser;
            }
        }
        private void SelectUser(object parameter)
        {
            var user = _context.GetUtilizator(Username,Password).ToList().FirstOrDefault();

            if(user != null)
            {
                MessageBox.Show("Loggged in successfully!");
            }
            else
            {
                MessageBox.Show("NumeUtilizator or Parola are wrong!");
                return;
            }

            
            switch (user.TipUtilizatorId)
            {
                case 1:
                    //Admin
                    AdministratorMenuWindow windowAdministrator = new AdministratorMenuWindow();
                    Application.Current.MainWindow.Close();
                    windowAdministrator.Show();
                    break;
                case 2:
                    //Student
                    ElevVM elevVM = new ElevVM(user.UtilizatorId);
                    ElevWindow windowElev = new ElevWindow();
                    windowElev.DataContext = elevVM;
                    Application.Current.MainWindow.Close();
                    windowElev.Show();
                    break;
                case 3:
                    //Diriginte
                    DiriginteMenuVM diriginteMenuVM = new DiriginteMenuVM(user.UtilizatorId);
                    DiriginteMenuWindow diriginteMenuWindow = new DiriginteMenuWindow();
                    diriginteMenuWindow.DataContext = diriginteMenuVM;
                    Application.Current.MainWindow.Close();
                    diriginteMenuWindow.Show();
                    break;
                case 4:
                    //Profesor
                    ProfesorMenuVM profesorMenuVM = new ProfesorMenuVM(user.UtilizatorId);
                    ProfesorMenuWindow profesorMenuWindow = new ProfesorMenuWindow();
                    profesorMenuWindow.DataContext= profesorMenuVM;
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = profesorMenuWindow;
                    profesorMenuWindow.Show();
                    break;
                default:
                    // Handle unrecognized user types
                    return;
            }
        }



    }
}
