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

namespace PlatformaEducationala.ViewModel
{
    public class CalculareMediiVM : BaseVM
    {

        MVP_PlatformaEducationalaEntities _context = new MVP_PlatformaEducationalaEntities();

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

        private string _labelText;

        public string LabelText
        {
            get { return _labelText; }
            set
            {
                if (_labelText != value)
                {
                    _labelText = value;
                    OnPropertyChanged(nameof(LabelText));
                }
            }
        }

        private ICommand _calculeazaMedie;
        public ICommand CalculeazaMedie
        {
            get
            {
                if (_calculeazaMedie == null)
                    _calculeazaMedie = new RelayCommands(CalculeazaMedieMethod);
                return _calculeazaMedie;
            }
        }

        private void CalculeazaMedieMethod(object parameter)
        {
            int selectedSemesterIndex = Semestre.IndexOf(SelectedSemester) + 1;
            if (selectedSemesterIndex == 0)
            {
                MessageBox.Show("Please select semestru!");
                return;
            }
            int medie = _context.MakeMedie(selectedSemesterIndex, SelectedMaterie.MaterieId, SelectedStudent.UtilizatorId);
            var result = _context.GetMediesForStudent(SelectedStudent.UtilizatorId);
            var selectedMedie = result.FirstOrDefault(m => m.Semestru == selectedSemesterIndex && m.MaterieId == SelectedMaterie.MaterieId);

            if (medie == -1)
            {
                MessageBox.Show("Not enough note!");
                return;
            }
            _context.SaveChanges();
            MessageBox.Show("Medie was calculated");
            if (selectedMedie != null)
            {
               
                float medieValue = (float)selectedMedie.Valoare;
                LabelText = "Medie is " + medieValue.ToString() + "!";
            }
    
        }

        private int idProfesor;

        public CalculareMediiVM(int idProfesor)
        {
            this.idProfesor = idProfesor;

            LoadMateriiForProfesor();
            LoadStudentiForProfesor();
            LoadSemestre();
        }

        public CalculareMediiVM()
        {
            
        }

    }
}
