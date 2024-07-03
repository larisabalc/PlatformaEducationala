using PlatformaEducationala.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PlatformaEducationala.Model;
using System.Collections.ObjectModel;

namespace PlatformaEducationala.ViewModel
{
    public class AdministratorOtherVM : BaseVM
    {
        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

        private int _materieId;
        public int MaterieId
        {
            get { return _materieId; }
            set
            {
                if (_materieId != value)
                {
                    _materieId = value;
                    OnPropertyChanged(nameof(MaterieId));
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

        private int _teza;
        public int Teza
        {
            get { return _teza; }
            set
            {
                if (_teza != value)
                {
                    _teza = value;
                    OnPropertyChanged(nameof(Teza));
                }
            }
        }

        private string _materie;
        public string Materie
        {
            get { return _materie; }
            set
            {
                if (_materie != value)
                {
                    _materie = value;
                    OnPropertyChanged(nameof(Materie));
                }
            }
        }

        private string _clasa;
        public string Clasa
        {
            get { return _clasa; }
            set
            {
                if (_clasa != value)
                {
                    _clasa = value;
                    OnPropertyChanged(nameof(Clasa));
                }
            }
        }

        private bool _isTeza;

        public bool IsTeza
        {
            get { return _isTeza; }
            set
            {
                _isTeza = value;
                OnPropertyChanged(nameof(IsTeza));
            }
        }

        private ObservableCollection<Materie> _materii;
        public ObservableCollection<Materie> Materii
        {
            get { return _materii; }
            set
            {
                _materii = value;
                OnPropertyChanged(nameof(Materii));
            }
        }

        private Materie _selectedMaterie;
        public Materie SelectedMaterie
        {
            get { return _selectedMaterie; }
            set
            {
                if (_selectedMaterie != value)
                {
                    _selectedMaterie = value;
                    OnPropertyChanged(nameof(SelectedMaterie));

                    if (_selectedMaterie != null)
                    {
                        Materie = _selectedMaterie.Nume;
                        IsTeza = (bool)_selectedMaterie.Teza;
                    }
                    else
                    {
                        Materie = string.Empty;
                        IsTeza = false;
                    }
                }
            }
        }

        private void LoadMaterii()
        {
            var result = _context.GetAllMateries();
            Materii = new ObservableCollection<Materie>(result.Select(r => new Materie
            {
                MaterieId = r.MaterieId,
                Nume = r.Nume,
                Teza = r.Teza
            })); ;
        }

        private ICommand _addMaterie;
        public ICommand AddMaterie
        {
            get
            {
                if (_addMaterie == null)
                    _addMaterie = new RelayCommands(AddMaterieMethod);
                return _addMaterie;
            }
        }
        private void AddMaterieMethod(object parameter)
        {
   
            _context.AddMaterie(Materie, IsTeza);
            _context.SaveChanges();
            MessageBox.Show("Materie added successfully!");
            LoadMaterii();
        }

        private ICommand _editMaterie;
        public ICommand EditMaterie
        {
            get
            {
                if (_editMaterie == null)
                    _editMaterie = new RelayCommands(EditMaterieMethod);
                return _editMaterie;
            }
        }

        private void EditMaterieMethod(object parameter)
        {
            if (SelectedMaterie != null)
            {
                _context.ModifyMaterie(SelectedMaterie.MaterieId,SelectedMaterie.Nume,SelectedMaterie.Teza);
                _context.SaveChanges();
                MessageBox.Show("Materie edited successfully!");
            }
           
        }

        private ICommand _deleteMaterie;
        public ICommand DeleteMaterie
        {
            get
            {
                if (_deleteMaterie == null)
                    _deleteMaterie = new RelayCommands(DeleteMaterieMethod);
                return _deleteMaterie;
            }
        }

        private void DeleteMaterieMethod(object parameter)
        {
            if (SelectedMaterie != null)
            {
                _context.DeleteMaterie(SelectedMaterie.MaterieId);
                _context.SaveChanges();
                MessageBox.Show("Materie deleted successfully!");
                LoadMaterii();
            }
        }


        private string _specializare;
        public string Specializare
        {
            get { return _specializare; }
            set
            {
                if (_specializare != value)
                {
                    _specializare = value;
                    OnPropertyChanged(nameof(Specializare));
                }
            }
        }
        private int _specializareId;
        public int SpecializareId
        {
            get { return _specializareId; }
            set
            {
                if (_specializareId != value)
                {
                    _specializareId = value;
                    OnPropertyChanged(nameof(SpecializareId));
                }
            }
        }

        private string _numeSpecialiare;
        public string NumeSpecializare
        {
            get { return _numeSpecialiare; }
            set
            {
                if (_numeSpecialiare != value)
                {
                    _numeSpecialiare = value;
                    OnPropertyChanged(nameof(NumeSpecializare));
                }
            }
        }

        private ObservableCollection<Specializare> _specializari;
        public ObservableCollection<Specializare> Specializari
        {
            get { return _specializari; }
            set
            {
                _specializari = value;
                OnPropertyChanged(nameof(Specializari));
            }
        }

        private Specializare _selectedSpecializare;
        public Specializare SelectedSpecializare
        {
            get { return _selectedSpecializare; }
            set
            {
                if (_selectedSpecializare != value)
                {
                    _selectedSpecializare = value;
                    OnPropertyChanged(nameof(SelectedSpecializare));

                    if (_selectedSpecializare != null)
                    {
                        NumeSpecializare = _selectedSpecializare.Nume;
                      
                    }
                    else
                    {
                        NumeSpecializare = string.Empty;
                    }
                }
            }
        }

        private void LoadSpecializari()
        {
            var result = _context.GetAllSpecializares();
            Specializari = new ObservableCollection<Specializare>(result.Select(r => new Specializare
            {
                SpecializareId = r.SpecializareId,
                Nume = r.Nume
            }));
        }

        private ICommand _addSpecializare;
        public ICommand AddSpecializare
        {
            get
            {
                if (_addSpecializare == null)
                    _addSpecializare = new RelayCommands(AddSpecializareMethod);
                return _addSpecializare;
            }
        }
        private void AddSpecializareMethod(object parameter)
        {
           
            _context.AddSpecializare(Specializare);
            _context.SaveChanges();
            MessageBox.Show("Specializare added successfully!");
            LoadSpecializari();
     
        }


        private ICommand _editSpecializare;
        public ICommand EditSpecializare
        {
            get
            {
                if (_editSpecializare == null)
                    _editSpecializare = new RelayCommands(EditSpecializareMethod);
                return _editSpecializare;
            }
        }

        private void EditSpecializareMethod(object parameter)
        {
            if (SelectedSpecializare != null)
            {
                _context.ModifySpecializare(SelectedSpecializare.SpecializareId,SelectedSpecializare.Nume);
                _context.SaveChanges();
                MessageBox.Show("Specializare edited successfully!");
            }
        }

        private ICommand _deleteSpecializare;
        public ICommand DeleteSpecializare
        {
            get
            {
                if (_deleteSpecializare == null)
                    _deleteSpecializare = new RelayCommands(DeleteSpecializareMethod);
                return _deleteSpecializare;
            }
        }

        private void DeleteSpecializareMethod(object parameter)
        {
            if (SelectedSpecializare != null)
            {
                _context.DeleteSpecializare(SelectedSpecializare.SpecializareId);
                _context.SaveChanges();
                MessageBox.Show("Specializare deleted successfully!");
                LoadSpecializari();
            }
        }


        public AdministratorOtherVM()
        {
            LoadMaterii();
            LoadSpecializari();
        }
    }
}
