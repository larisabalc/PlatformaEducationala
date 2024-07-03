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
    public class ProfesorVM : BaseVM
    {
        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

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
                if (value != null)
                {
                    LoadNote();
                    if(SelectedSemester != null)
                         LoadAbsente();
                }
            }
        }

        private void LoadStudentiForProfesor()
        {
            var result = _context.GetAllProfesorClasaLinks()
                .Where(s => s.ProfesorId == idProfesor)
                .ToList();

            if (result.Count > 0)
            {
                var classIds = result.Select(s => s.ClasaId).ToList();
                var result1 = _context.GetAllStudentClasaLinks()
                    .Where(s => classIds.Contains(s.ClasaId))
                    .ToList();

                if (result1.Count > 0)
                {
                    var studentIds = result1.Select(s => s.StudentId).ToList();
                    var result2 = _context.GetAllUtilizators()
                        .Where(s => studentIds.Contains(s.UtilizatorId) && s.TipUtilizatorId == 2)
                        .ToList();

                    Studenti = new ObservableCollection<Utilizator>();

                    foreach (var student in result2)
                    {
                        Studenti.Add(new Utilizator
                        {
                            UtilizatorId = student.UtilizatorId,
                            NumeUtilizator = student.NumeUtilizator,
                            Nume = student.Nume
                        });
                    }
                }
            }
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
                _selectedMaterie = value;
                OnPropertyChanged(nameof(SelectedMaterie));
            }
        }

        private void LoadMateriiForProfesor()
        {
            var result = _context.GetAllProfesorClasaLinks()
                .Where(s => s.ProfesorId == idProfesor)
                .ToList();

            if (result.Count > 0)
            {
                var classIds = result.Select(s => s.ClasaId).ToList();
                var result1 = _context.GetAllMaterieClasaLinks()
                    .Where(s => classIds.Contains(s.ClasaId))
                    .ToList();

                if (result1.Count > 0)
                {
                    var studentIds = result1.Select(s => s.MaterieId).ToList();
                    var result2 = _context.GetAllMateries()
                        .Where(s => studentIds.Contains(s.MaterieId))
                        .ToList();

                    Materii = new ObservableCollection<Materie>();

                    foreach (var student in result2)
                    {
                        Materii.Add(new Materie
                        {
                            MaterieId = student.MaterieId,
                            Nume = student.Nume
                        });
                    }
                }
            }
        }


        private int _valoare;
        public int Valoare
        {
            get { return _valoare; }
            set
            {
                _valoare = value;
                OnPropertyChanged(nameof(Valoare));
            }
        }

        private bool _teza;
        public bool Teza
        {
            get { return _teza; }
            set
            {
                _teza = value;
                OnPropertyChanged(nameof(Teza));
            }
        }

        private ObservableCollection<string> _semestre;
        public ObservableCollection<string> Semestre
        {
            get { return _semestre; }
            set
            {
                _semestre = value;
                OnPropertyChanged(nameof(Semestre));
            }
        }

        private string _selectedSemester;
        public string SelectedSemester
        {
            get { return _selectedSemester; }
            set
            {
                _selectedSemester = value;
                OnPropertyChanged(nameof(SelectedSemester));
            }
        }

        private void LoadSemestre()
        {
            Semestre = new ObservableCollection<string>
            {
                "Semestrul 1",
                "Semestrul 2"
            };
        }

        private bool _motivata;
        public bool Motivata
        {
            get { return _motivata; }
            set
            {
                _motivata = value;
                OnPropertyChanged(nameof(Motivata));
            }
        }

        private DateTime _data = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }


        private ICommand _addAbsenta;
        public ICommand AddAbsenta
        {
            get
            {
                if (_addAbsenta == null)
                    _addAbsenta = new RelayCommands(AddAbsentaMethod);
                return _addAbsenta;
            }
        }
        private void AddAbsentaMethod(object parameter)
        {
            if(SelectedStudent != null && SelectedMaterie != null)
            {
                int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
                if (selectedSemesterIndex != -1)
                {
                    if (Motivata == true)
                        _context.AddAbsenta(SelectedMaterie.MaterieId, Data.ToShortDateString(), selectedSemesterIndex, false, true, SelectedStudent.UtilizatorId);
                    else
                        _context.AddAbsenta(SelectedMaterie.MaterieId, Data.ToShortDateString(), selectedSemesterIndex, true, false, SelectedStudent.UtilizatorId);

                    _context.SaveChanges();
                    MessageBox.Show("Absenta added!");
                    SelectedStudent = null;
                    SelectedMaterie = null;
                }
                else
                {
                    MessageBox.Show("Invalid selected semesteru!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Student and Materie must be selected!");
                return;
            }
        }

        private ICommand _addNota;
        public ICommand AddNota
        {
            get
            {
                if (_addNota == null)
                    _addNota = new RelayCommands(AddNotaMethod);
                return _addNota;
            }
        }
        private void AddNotaMethod(object parameter)
        {
            if (SelectedStudent != null && SelectedMaterie != null)
            {
                int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
                if (selectedSemesterIndex != -1)
                {
                    _context.AddNota(SelectedMaterie.MaterieId, Valoare, Data.ToShortDateString(), selectedSemesterIndex, SelectedStudent.UtilizatorId,Teza);
                    _context.SaveChanges();
                    MessageBox.Show("Nota added!");
                    SelectedStudent = null;
                    SelectedMaterie = null;
                }
                else
                {
                    MessageBox.Show("Invalid selected semesteru!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Student and Materie must be selected!");
                return;
            }
        }

        private ObservableCollection<Nota> _note;
        public ObservableCollection<Nota> Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }

        private Nota _selectedNota;
        public Nota SelectedNota
        {
            get { return _selectedNota; }
            set
            {
                _selectedNota = value;
                OnPropertyChanged(nameof(SelectedNota));

            }
        }

        private void LoadNote()
        {

            var result = _context.GetAllNotas()
                .Where(n => n.StudentId == SelectedStudent.UtilizatorId)
                .ToList();

            if (result.Count > 0)
            {
                Note = new ObservableCollection<Nota>();

                foreach (var student in result)
                {
                    Note.Add(new Nota
                    {
                        NotaId = student.NotaId,
                        MaterieId = student.MaterieId,
                        Valoare = student.Valoare,
                        Semestru = student.Semestru,
                        StudentId = student.StudentId,
                        EsteTeza = student.EsteTeza,   

                    });
                }
            }
        }

        private ObservableCollection<Absenta> _absente;
        public ObservableCollection<Absenta> Absente
        {
            get { return _absente; }
            set
            {
                _absente = value;
                OnPropertyChanged(nameof(Absente));
            }
        }

        private Absenta _selectedAbsenta;
        public Absenta SelectedAbsenta
        {
            get { return _selectedAbsenta; }
            set
            {
                _selectedAbsenta = value;
                OnPropertyChanged(nameof(SelectedAbsenta));

            }
        }

        private void LoadAbsente()
        {
           
            int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
            if(selectedSemesterIndex == 0)
            {
                MessageBox.Show("Please select semestru!");
                return;
            }
            var result = _context.GetAllAbsentasForStudent(SelectedStudent.UtilizatorId, selectedSemesterIndex);

            Absente = new ObservableCollection<Absenta>(result.Select(r => new Absenta
            {
                AbsentaId = r.AbsentaId,
                MaterieId = r.MaterieId,
                Data = r.Data,
                Semestru = r.Semestru,
                Motivata = r.Motivata,
                Nemotivata = r.Nemotivata
               
            }));
        }

        private ICommand _deleteNota;
        public ICommand DeleteNota
        {
            get
            {
                if (_deleteNota == null)
                    _deleteNota = new RelayCommands(DeleteNotaMethod);
                return _deleteNota;
            }
        }
        private void DeleteNotaMethod(object parameter)
        {
            if(SelectedNota != null)
            {
                _context.DeleteNota(SelectedNota.NotaId);
                _context.SaveChanges();
                MessageBox.Show("Nota deleted!");
                LoadNote();
                SelectedNota = null;
            }
        }

        private ICommand _motiveazaAbsenta;
        public ICommand MotiveazaAbsenta
        {
            get
            {
                if (_motiveazaAbsenta == null)
                    _motiveazaAbsenta = new RelayCommands(MotiveazaAbsentaMethod);
                return _motiveazaAbsenta;
            }
        }

        private void MotiveazaAbsentaMethod(object parameter)
        {
            if (SelectedAbsenta != null)
            {
                if (SelectedAbsenta.Nemotivata == false)
                {
                    MessageBox.Show("Absenta is nemotivabila!");
                    return;
                }
                _context.ExcuseAbsenta(SelectedAbsenta.AbsentaId);
                _context.SaveChanges();
                MessageBox.Show("Absenta motivata!");
                LoadAbsente();
                SelectedAbsenta = null;
            }
        }

        private int idProfesor;

        public ProfesorVM(int idProfesor)
        {
            this.idProfesor = idProfesor;

            LoadStudentiForProfesor();
            LoadMateriiForProfesor();
            LoadSemestre();
        }

        public ProfesorVM()
        {

        }

    }
}
