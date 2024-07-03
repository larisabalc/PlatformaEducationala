using PlatformaEducationala.Commands;
using PlatformaEducationala.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace PlatformaEducationala.ViewModel
{
    public class AdministratorAsocieriVM : BaseVM
    {
        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

        private int _an;
        public int An
        {
            get { return _an; }
            set
            {
                if (_an != value)
                {
                    _an = value;
                    OnPropertyChanged(nameof(An));
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

        private ObservableCollection<string> _specializari;
        public ObservableCollection<string> Specializari
        {
            get { return _specializari; }
            set
            {
                _specializari = value;
                OnPropertyChanged(nameof(Specializari));
            }
        }

        private string _selectedSpecializare;
        public string SelectedSpecializare
        {
            get { return _selectedSpecializare; }
            set
            {
                _selectedSpecializare = value;
                OnPropertyChanged(nameof(SelectedSpecializare));
            }
        }

        private void LoadSpecializari()
        {
            var rezult = _context.GetAllSpecializares();
            Specializari = new ObservableCollection<string>(rezult.Select(r => r.Nume));
        }

        private ObservableCollection<string> _materii;
        public ObservableCollection<string> Materii
        {
            get { return _materii; }
            set
            {
                _materii = value;
                OnPropertyChanged(nameof(Materii));
            }
        }

        private string _selectedMaterie;
        public string SelectedMaterie
        {
            get { return _selectedMaterie; }
            set
            {
                _selectedMaterie = value;
                OnPropertyChanged(nameof(SelectedMaterie));
            }
        }

        private void LoadMaterii()
        {
            var rezult = _context.GetAllMateries();
            Materii = new ObservableCollection<string>(rezult.Select(r => r.Nume));
        }

        private ObservableCollection<Clasa> _clase;
        public ObservableCollection<Clasa> Clase
        {
            get { return _clase; }
            set
            {
                _clase = value;
                OnPropertyChanged(nameof(Clase));
            }
        }

        private Clasa _selectedClasa;
        public Clasa SelectedClasa
        {
            get { return _selectedClasa; }
            set
            {
                _selectedClasa = value;
                OnPropertyChanged(nameof(SelectedClasa));
            }
        }

        private void LoadClase()
        {
            var result = _context.GetAllClasas();
            Clase = new ObservableCollection<Clasa>(result.Select(r => new Clasa
            {
                ClasaId = r.ClasaId,
                SpecializareId = r.SpecializareId,
                Nume = r.Nume,
                An = r.An
            })); ;
        }

        private int _clasaId;
        public int ClasaId
        {
            get { return _clasaId; }
            set
            {
                if (_clasaId != value)
                {
                    _clasaId = value;
                    OnPropertyChanged(nameof(ClasaId));
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

        private ICommand _addClasa;
        public ICommand AddClasa
        {
            get
            {
                if (_addClasa == null)
                    _addClasa = new RelayCommands(AddClasaMethod);
                return _addClasa;
            }
        }
        private void AddClasaMethod(object parameter)
        {
            var tmp = _context.GetAllSpecializares().Where(s => s.Nume == SelectedSpecializare).ToList();
            if (tmp.Count > 0)
            {
                var selectedSpecializare = tmp[0];
                _context.AddClasa(selectedSpecializare.SpecializareId, An, Nume);
                _context.SaveChanges();
                MessageBox.Show("Clasa added successfully!");
                LoadClase();
            }
        }

        private ICommand _addMaterieToClasa;
        public ICommand AddMaterieToClasa
        {
            get
            {
                if (_addMaterieToClasa == null)
                    _addMaterieToClasa = new RelayCommands(AddMaterieToClasaMethod);
                return _addMaterieToClasa;
            }
        }
        private void AddMaterieToClasaMethod(object parameter)
        {
            var tmp = _context.GetAllMateries().Where(s => s.Nume == SelectedMaterie).ToList();
            if (SelectedClasa != null && SelectedMaterie != null)
            {
                _context.AddMaterieClasaLink(tmp[0].MaterieId, SelectedClasa.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Materie added to Clasa!");
            }
        }

        private ICommand _deleteClasa;
        public ICommand DeleteClasa
        {
            get
            {
                if (_deleteClasa == null)
                    _deleteClasa = new RelayCommands(DeleteClasaMethod);
                return _deleteClasa;
            }
        }

        private void DeleteClasaMethod(object parameter)
        {
            if (SelectedClasa != null)
            {
                _context.DeleteClasa(SelectedClasa.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Clasa deleted successfully!");
                LoadClase();
            }
        }

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

        private string _numeProfesor;
        public string NumeProfesor
        {
            get { return _numeProfesor; }
            set
            {
                if (_numeProfesor != value)
                {
                    _numeProfesor = value;
                    OnPropertyChanged(nameof(NumeProfesor));
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

        private ObservableCollection<Utilizator> _profesori;
        public ObservableCollection<Utilizator> Profesori
        {
            get { return _profesori; }
            set
            {
                _profesori = value;
                OnPropertyChanged(nameof(Profesori));
            }
        }

        private Utilizator _selectedProfesor;
        public Utilizator SelectedProfesor
        {
            get { return _selectedProfesor; }
            set
            {
                _selectedProfesor = value;
                OnPropertyChanged(nameof(SelectedProfesor));
            }
        }

        private void LoadProfesori()
        {
            var result = _context.GetAllUtilizators().Where(s => s.TipUtilizatorId == 4).ToList(); ;
            Profesori = new ObservableCollection<Utilizator>(result.Select(r => new Utilizator
            {
                UtilizatorId = r.UtilizatorId,
                Nume = r.Nume,
                NumeUtilizator = r.NumeUtilizator
            })); ;
        }

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

        private string _numeMaterie;
        public string NumeMaterie
        {
            get { return _numeMaterie; }
            set
            {
                if (_numeMaterie != value)
                {
                    _numeMaterie = value;
                    OnPropertyChanged(nameof(NumeMaterie));
                }
            }
        }

        private ObservableCollection<Materie> _materiiDataGrid;
        public ObservableCollection<Materie> MateriiDataGrid
        {
            get { return _materiiDataGrid; }
            set
            {
                _materiiDataGrid = value;
                OnPropertyChanged(nameof(MateriiDataGrid));
            }
        }

        private Materie _selectedMaterieDataGrid;
        public Materie SelectedMaterieDataGrid
        {
            get { return _selectedMaterieDataGrid; }
            set
            {
                _selectedMaterieDataGrid = value;
                OnPropertyChanged(nameof(SelectedMaterieDataGrid));
            }
        }

        private void LoadMateriiDataGrid()
        {
            var result = _context.GetAllMateries();
            MateriiDataGrid = new ObservableCollection<Materie>(result.Select(r => new Materie
            {
                MaterieId = r.MaterieId,
                Nume = r.Nume
            })); ;
        }

        private ICommand _addProfesorMaterieClasa;
        public ICommand AddProfesorMaterieClasa
        {
            get
            {
                if (_addProfesorMaterieClasa == null)
                    _addProfesorMaterieClasa = new RelayCommands(AddProfesorMaterieClasaMethod);
                return _addProfesorMaterieClasa;
            }
        }
        private void AddProfesorMaterieClasaMethod(object parameter)
        {
            if (SelectedClasa != null && SelectedProfesor != null)
            {
                _context.AddProfesorClasaLink(SelectedProfesor.UtilizatorId,SelectedClasa.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Profesor added to Clasa!");
                SelectedClasa = null;
                SelectedProfesor = null;
            }
            if (SelectedMaterieDataGrid != null && SelectedProfesor != null)
            {
                _context.AddMaterieProfesorLink(SelectedMaterieDataGrid.MaterieId, SelectedProfesor.UtilizatorId);
                _context.SaveChanges();
                MessageBox.Show("Materie added to Profesor!");
                SelectedProfesor = null;
                SelectedMaterieDataGrid = null;
            }
        }

        private ObservableCollection<Utilizator> _studenti;
        public ObservableCollection<Utilizator> Studenti
        {
            get { return _studenti; }
            set
            {
                _studenti = value;
                OnPropertyChanged(nameof(Studenti));
            }
        }

        private Utilizator _selectedStudent;
        public Utilizator SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        private void LoadStudents()
        {
            var result = _context.GetAllUtilizators().Where(s => s.TipUtilizatorId == 2).ToList(); ;
            Studenti = new ObservableCollection<Utilizator>(result.Select(r => new Utilizator
            {
                UtilizatorId = r.UtilizatorId,
                Nume = r.Nume,
                NumeUtilizator = r.NumeUtilizator
            })); ;
        }

        private ICommand _addElevClasa;
        public ICommand AddStudentClasa
        {
            get
            {
                if (_addElevClasa == null)
                    _addElevClasa = new RelayCommands(AddStudentClasaMethod);
                return _addElevClasa;
            }
        }
        private void AddStudentClasaMethod(object parameter)
        {
            if (SelectedClasa != null && SelectedStudent != null)
            {
                
                _context.AddStudentClasaLink(SelectedStudent.UtilizatorId, SelectedClasa.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Student added to Clasa!");
                SelectedClasa = null;
                SelectedStudent = null;
               
            }
        }


        private ICommand _cancel;
        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                    _cancel = new RelayCommands(CancelMethod);
                return _cancel;
            }
        }
        private void CancelMethod(object parameter)
        {
            SelectedMaterieDataGrid = null;
            SelectedClasa = null;
            SelectedStudent = null;
        }

        public AdministratorAsocieriVM()
        {
            LoadSpecializari();
            LoadMaterii();
            LoadClase();
            LoadProfesori();
            LoadMateriiDataGrid();
            LoadStudents();
        }
    }
}
