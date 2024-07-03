using PlatformaEducationala.Commands;
using PlatformaEducationala.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace PlatformaEducationala.ViewModel
{
    public class DiriginteAbsenteVM : BaseVM
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
                if(value != null)
                {
                    if (SelectedSemester != null)
                        LoadAbsente();
                }
            }
        }

        private void LoadStudentiForDiriginte()
        {
            var result = _context.GetAllDiriginteClasaLinks()
                .Where(s => s.DiriginteId == idDiriginte)
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
            if (selectedSemesterIndex == 0)
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

        private ObservableCollection<Absenta> _absenteMotivate;
        public ObservableCollection<Absenta> AbsenteMotivate
        {
            get { return _absenteMotivate; }
            set
            {
                _absenteMotivate = value;
                OnPropertyChanged(nameof(AbsenteMotivate));
            }
        }

        private Absenta _selectedAbsentaMotivata;
        public Absenta SelectedAbsentaMotivata
        {
            get { return _selectedAbsentaMotivata; }
            set
            {
                _selectedAbsentaMotivata = value;
                OnPropertyChanged(nameof(SelectedAbsentaMotivata));
            }
        }

        private void LoadAbsenteMotivate()
        {
            int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
            if (selectedSemesterIndex == 0)
            {
                MessageBox.Show("Please select semestru!");
                return;
            }
            var result = _context.GetAbsentasForAMaterie(SelectedStudent.UtilizatorId, SelectedMaterie.MaterieId)
            .Where(a => a.Motivata == true);


            AbsenteMotivate = new ObservableCollection<Absenta>(result.Select(r => new Absenta
            {
                AbsentaId = r.AbsentaId,
                MaterieId = r.MaterieId,
                Data = r.Data,
                Semestru = r.Semestru,
                Motivata = r.Motivata,
                Nemotivata = r.Nemotivata
            }));
        }

        private ObservableCollection<Absenta> _absenteNemotivate;
        public ObservableCollection<Absenta> AbsenteNemotivate
        {
            get { return _absenteNemotivate; }
            set
            {
                _absenteNemotivate = value;
                OnPropertyChanged(nameof(AbsenteNemotivate));
            }
        }

        private Absenta _selectedAbsentaNemotivata;
        public Absenta SelectedAbsentaNemotivata
        {
            get { return _selectedAbsentaNemotivata; }
            set
            {
                _selectedAbsentaNemotivata = value;
                OnPropertyChanged(nameof(SelectedAbsentaNemotivata));
            }
        }

        private void LoadAbsenteNemotivate()
        {
            int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
            if (selectedSemesterIndex == 0)
            {
                MessageBox.Show("Please select semestru!");
                return;
            }
            var result = _context.GetAbsentasForAMaterie(SelectedStudent.UtilizatorId, SelectedMaterie.MaterieId)
            .Where(a => a.Motivata == false);


            AbsenteNemotivate = new ObservableCollection<Absenta>(result.Select(r => new Absenta
            {
                AbsentaId = r.AbsentaId,
                MaterieId = r.MaterieId,
                Data = r.Data,
                Semestru = r.Semestru,
                Motivata = r.Motivata,
                Nemotivata = r.Nemotivata
            }));
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
                if( value != null)
                {
                    LoadAbsenteMotivate();
                    LoadAbsenteNemotivate();
                }
            }
        }

        private void LoadMaterii()
        {
            var result = _context.GetAllDiriginteClasaLinks()
              .Where(s => s.DiriginteId == idDiriginte)
              .ToList();

            if (result.Count > 0)
            {
                var classroomsIds = result.Select(s => s.ClasaId).ToList();
                var result1 = _context.GetAllMaterieClasaLinks()
                    .Where(s => classroomsIds.Contains(s.ClasaId))
                    .ToList();

                if (result1.Count > 0)
                {
                    var subjectIds = result1.Select(s => s.MaterieId).ToList();
                    var result2 = _context.GetAllMateries()
                        .Where(s => subjectIds.Contains(s.MaterieId))
                    .ToList();

                    Materii = new ObservableCollection<Materie>();

                    foreach (var subject in result2)
                    {
                        Materii.Add(new Materie
                        {
                            MaterieId = subject.MaterieId,
                            Nume = subject.Nume,
                        });
                    }
                }
            }
        }

        private int idDiriginte;

        public DiriginteAbsenteVM(int idDiriginte)
        {
            this.idDiriginte = idDiriginte;
            LoadStudentiForDiriginte();
            LoadSemestre();
            LoadMaterii();
        }

        public DiriginteAbsenteVM()
        {
        }
    }
}
