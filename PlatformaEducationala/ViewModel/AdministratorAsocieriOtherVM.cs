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
    public class AdministratorAsocieriOtherVM : BaseVM
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
                if (value != null)
                {
                    LoadStudentiFromClasa();
                }
            }
        }

        private ObservableCollection<Clasa> _claseForProfesor;
        public ObservableCollection<Clasa> ClaseForProfesor
        {
            get { return _claseForProfesor; }
            set
            {
                _claseForProfesor = value;
                OnPropertyChanged(nameof(ClaseForProfesor));
            }
        }

        private Clasa _selectedClasaForProfesor;
        public Clasa SelectedClasaForProfesor
        {
            get { return _selectedClasaForProfesor; }
            set
            {
                _selectedClasaForProfesor = value;
                OnPropertyChanged(nameof(SelectedClasaForProfesor));

            }
        }

        private void LoadClaseForProfesor()
        {
            var result = _context.GetAllProfesorClasaLinks()
                .Where(s => s.ProfesorId == SelectedProfesor.UtilizatorId)
                .ToList();

            if (result != null && result.Count > 0)
            {
                var result1 = _context.GetAllClasas()
                    .Where(s => result.Any(r => r.ClasaId == s.ClasaId))
                    .ToList();

                ClaseForProfesor = new ObservableCollection<Clasa>();

                foreach (var clasaResult in result1)
                {
                    ClaseForProfesor.Add(new Clasa
                    {
                        ClasaId = clasaResult.ClasaId,
                        SpecializareId = clasaResult.SpecializareId,
                        Nume = clasaResult.Nume,
                        An = clasaResult.An
                    });
                }
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
                if (value != null)
                {
                   if(SelectedProfesor != null)
                   {
                    LoadClaseForProfesor();
                    LoadMateriiDataGridForProfesor();
                   }
                }
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

        private void LoadMateriiDataGridForProfesor()
        {
            var result = _context.GetAllMaterieProfesorLinks()
                .Where(s => s.ProfesorId == SelectedProfesor.UtilizatorId)
                .ToList();

            if (result != null && result.Count > 0)
            {
                var result1 = _context.GetAllMateries()
                    .Where(s => result.Any(r => r.MaterieId == s.MaterieId))
                    .ToList();

                MateriiDataGrid = new ObservableCollection<Materie>();

                foreach (var materieResult in result1)
                {
                    MateriiDataGrid.Add(new Materie
                    {
                        MaterieId = materieResult.MaterieId,
                        Nume = materieResult.Nume,  
                    });
                }
     
            }
        }


        private ICommand _deleteProfesorMaterieClasa;
        public ICommand DeleteProfesorMaterieClasa
        {
            get
            {
                if (_deleteProfesorMaterieClasa == null)
                    _deleteProfesorMaterieClasa = new RelayCommands(DeleteProfesorMaterieClasaMethod);
                return _deleteProfesorMaterieClasa;
            }
        }
        private void DeleteProfesorMaterieClasaMethod(object parameter)
        {
            if (SelectedClasaForProfesor != null && SelectedProfesor != null)
            {
                _context.DeleteProfesorClasaLink(SelectedProfesor.UtilizatorId, SelectedClasaForProfesor.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Profesor deleted from Clasa!");
                ClaseForProfesor.Clear();
                SelectedClasa = null;
                SelectedProfesor = null;
            }
            if (SelectedMaterieDataGrid != null && SelectedProfesor != null)
            {
                _context.DeleteMaterieProfesorLink(SelectedMaterieDataGrid.MaterieId, SelectedProfesor.UtilizatorId);
                _context.SaveChanges();
                MessageBox.Show("Materie deleted from Profesor!");
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

        private void LoadStudentiFromClasa()
        {
            var result = _context.GetStudentsFromClasa(SelectedClasa.ClasaId);
            Studenti = new ObservableCollection<Utilizator>(result.Select(r => new Utilizator
            {
                UtilizatorId = r.UtilizatorId,
                Nume = r.Nume,
                NumeUtilizator = r.NumeUtilizator
            })); ;
        }

        private ICommand _deleteElevClasa;
        public ICommand DeleteStudentClasa
        {
            get
            {
                if (_deleteElevClasa == null)
                    _deleteElevClasa = new RelayCommands(DeleteStudentClasaMethod);
                return _deleteElevClasa;
            }
        }
        private void DeleteStudentClasaMethod(object parameter)
        {
            if (SelectedClasa != null && SelectedStudent != null)
            {
                _context.DeleteStudentClasaLink(SelectedStudent.UtilizatorId, SelectedClasa.ClasaId);
                _context.SaveChanges();
                MessageBox.Show("Student deleted from Clasa!");
                Studenti.Clear();
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

        private ObservableCollection<Utilizator> _diriginti;
        public ObservableCollection<Utilizator> Diriginti
        {
            get { return _diriginti; }
            set
            {
                _diriginti = value;
                OnPropertyChanged(nameof(Diriginti));
            }
        }

        private Utilizator _selectedDiriginte;
        public Utilizator SelectedDiriginte
        {
            get { return _selectedDiriginte; }
            set
            {
                _selectedDiriginte = value;
                OnPropertyChanged(nameof(SelectedDiriginte));
            }
        }

        private void LoadDiriginti()
        {
            var result1 = _context.GetAllDiriginteClasaLinks().ToList();
            var result = _context.GetAllUtilizators()
                .Where(s => !result1.Any(r => r.DiriginteId == s.UtilizatorId) && s.TipUtilizatorId == 3)
                .ToList();

          
            Diriginti = new ObservableCollection<Utilizator>(result.Select(r => new Utilizator
            {
                UtilizatorId = r.UtilizatorId,
                Nume = r.Nume,
                NumeUtilizator = r.NumeUtilizator
            })); ;
            
        }

        private ICommand _addDiriginteClasa;
        public ICommand AddDiriginteClasa
        {
            get
            {
                if (_addDiriginteClasa == null)
                    _addDiriginteClasa = new RelayCommands(AddDiriginteClasaMethod);
                return _addDiriginteClasa;
            }
        }
        private void AddDiriginteClasaMethod(object parameter)
        {
            _context.AddDiriginteClasaLink(SelectedDiriginte.UtilizatorId, SelectedClasaFaraDiriginte.ClasaId);
            _context.SaveChanges();
            MessageBox.Show("Diriginte added to Clasa!");
            LoadClaseFaraDiriginte();
            LoadDiriginti();
        }

        private ObservableCollection<Clasa> _claseFaraDiriginte;
        public ObservableCollection<Clasa> ClaseFaraDiriginte
        {
            get { return _claseFaraDiriginte; }
            set
            {
                _claseFaraDiriginte = value;
                OnPropertyChanged(nameof(ClaseFaraDiriginte));
            }
        }

        private Clasa _selectedClasaFaraDiriginte;
        public Clasa SelectedClasaFaraDiriginte
        {
            get { return _selectedClasaFaraDiriginte; }
            set
            {
                _selectedClasaFaraDiriginte = value;
                OnPropertyChanged(nameof(SelectedClasaFaraDiriginte));

            }
        }

        private void LoadClaseFaraDiriginte()
        {
            var result1 = _context.GetAllDiriginteClasaLinks().ToList();
            var result = _context.GetAllClasas()
                .Where(s => !result1.Any(r => r.ClasaId == s.ClasaId))
                .ToList();

            if (result.Any())
            {
                ClaseFaraDiriginte = new ObservableCollection<Clasa>(result.Select(r => new Clasa
                {
                    ClasaId = r.ClasaId,
                    SpecializareId = r.SpecializareId,
                    Nume = r.Nume,
                    An = r.An
                }));
            }

        }

        public AdministratorAsocieriOtherVM()
        {
            LoadClase();
            LoadProfesori();
            LoadDiriginti();
            LoadClaseFaraDiriginte();
        }
    }
}
