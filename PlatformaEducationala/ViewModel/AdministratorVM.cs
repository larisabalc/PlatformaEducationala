using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformaEducationala.Commands;
using System.Windows.Input;
using System.Windows;
using PlatformaEducationala.Model;

namespace PlatformaEducationala.ViewModel
{
    public class AdministratorVM : BaseVM
    {
        private int _utilizatorId;
        public int UtilizatorId
        {
            get { return _utilizatorId; }
            set
            {
                if (_utilizatorId != value)
                {
                    _utilizatorId = value;
                    OnPropertyChanged(nameof(UtilizatorId));
                }
            }
        }

        private string _nume;
        public string Nume
        {
            get { return _nume; }
            set
            {
                if (_nume != value)
                {
                    _nume = value;
                    OnPropertyChanged(nameof(Nume));
                }
            }
        }

        private string _numeUtilizator;
        public string NumeUtilizator
        {
            get { return _numeUtilizator; }
            set
            {
                if (_numeUtilizator != value)
                {
                    _numeUtilizator = value;
                    OnPropertyChanged(nameof(NumeUtilizator));
                }
            }
        }

        private string _parola;
        public string Parola
        {
            get { return _parola; }
            set
            {
                if (_parola != value)
                {
                    _parola = value;
                    OnPropertyChanged(nameof(Parola));
                }
            }
        }

        private ObservableCollection<string> _userTypes;
        public ObservableCollection<string> UserTypes
        {
            get { return _userTypes; }
            set
            {
                _userTypes = value;
                OnPropertyChanged(nameof(UserTypes));
            }
        }

        private string _selectedUserType;
        public string SelectedUserType
        {
            get { return _selectedUserType; }
            set
            {
                _selectedUserType = value;
                OnPropertyChanged(nameof(SelectedUserType));
            }
        }

        private int _tipUtilizatorId;
        public int TipUtilizatorId
        {
            get { return _tipUtilizatorId; }
            set
            {
                if (_tipUtilizatorId != value)
                {
                    _tipUtilizatorId = value;
                    OnPropertyChanged(nameof(TipUtilizatorId));
                }
            }
        }

        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

        private ObservableCollection<Utilizator> _users;
        public ObservableCollection<Utilizator> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private Utilizator _selectedUser;
        public Utilizator SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));

                    if (_selectedUser != null)
                    {
                        Nume = _selectedUser.Nume;
                        NumeUtilizator = _selectedUser.NumeUtilizator;
                        Parola = _selectedUser.Parola;
                        TipUtilizatorId = _selectedUser.TipUtilizatorId;
                    }
                    else
                    {
                        Nume = string.Empty;
                        NumeUtilizator = string.Empty;
                        Parola = string.Empty;
                        TipUtilizatorId = 0;
                    }
                }
            }
        }

        public AdministratorVM()
        {
            LoadUsers();
            LoadUserTypes();
        }

        private void LoadUsers()
        {
            ObjectResult<GetAllUtilizators_Result> result = _context.GetAllUtilizators();

            Users = new ObservableCollection<Utilizator>(result.Select(r => new Utilizator
            {
                UtilizatorId = r.UtilizatorId,
                Nume = r.Nume,
                NumeUtilizator = r.NumeUtilizator,
                Parola = r.Parola,
                TipUtilizatorId = r.TipUtilizatorId
            }));
        }

        private void LoadUserTypes()
        {
            UserTypes = new ObservableCollection<string>
            {
                "Administrator",
                "Student",
                "Diriginte",
                "Profesor"
            };
        }


        private ICommand _modifyUser;
        public ICommand ModifyUser
        {
            get
            {
                if (_modifyUser == null)
                    _modifyUser = new RelayCommands(UpdateUser);
                return _modifyUser;
            }
        }
        private void UpdateUser(object parameter)
        {
            if (SelectedUser != null)
            {
                _context.ModifyUtilizator(SelectedUser.UtilizatorId, SelectedUser.Nume, SelectedUser.NumeUtilizator, SelectedUser.Parola);
                _context.SaveChanges();
                MessageBox.Show("User modified successfully!");
            }
        }

        private ICommand _deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (_deleteUser == null)
                    _deleteUser = new RelayCommands(DeleteUserAfterId);
                return _deleteUser;
            }
        }
        private void DeleteUserAfterId(object parameter)
        {
            if (SelectedUser != null)
            {
                _context.DeleteUtilizator(SelectedUser.UtilizatorId);
                _context.SaveChanges();
                MessageBox.Show("User deleted successfully!");
                LoadUsers();
            }
        }

        private ICommand _addUser;
        public ICommand AddUser
        {
            get
            {
                if (_addUser == null)
                    _addUser = new RelayCommands(AddUserMethod);
                return _addUser;
            }
        }
        private void AddUserMethod(object parameter)
        {
           string userType = SelectedUserType;

           int userTypeValue;

           switch (userType)
           {
               case "Administrator":
                   userTypeValue = 1;
                   break;
               case "Student":
                   userTypeValue = 2;
                   break;
               case "Diriginte":
                   userTypeValue = 3;
                   break;
               case "Profesor":
                   userTypeValue = 4;
                   break;
               default:
                   return;
           }

            _context.AddUtilizator(Nume, NumeUtilizator, Parola, userTypeValue);
            _context.SaveChanges();
            MessageBox.Show("User added successfully!");
            Nume = string.Empty;
            NumeUtilizator = string.Empty;
            Parola = string.Empty;
            SelectedUserType = null;
            LoadUsers();
        }

    }
}
