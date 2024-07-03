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
    public class DiriginteMediiVM : BaseVM
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
                    LoadMedii();
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

        private ObservableCollection<Medie> _medii;
        public ObservableCollection<Medie> Medii
        {
            get { return _medii; }
            set
            {
                _medii = value;
                OnPropertyChanged(nameof(Medii));
            }
        }

        private Materie _selectedMedie;
        public Materie SelectedMedie
        {
            get { return _selectedMedie; }
            set
            {
                _selectedMedie = value;
                OnPropertyChanged(nameof(SelectedMedie));
            }
        }

        private void LoadMedii()
        {
            if (SelectedStudent != null)
            {
                var result = _context.GetMediesForStudent(SelectedStudent.UtilizatorId);
                Medii = new ObservableCollection<Medie>(result.Select(r => new Medie
                {
                    MedieId = r.MedieId,
                    Valoare = r.Valoare,
                    Semestru = r.Semestru,
                    MaterieId = r.MaterieId,
                    StudentId = r.StudentId,
                }));
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

            var result = _context.GetMediesForStudent(SelectedStudent.UtilizatorId).ToList();

            if (result.Count() == 0)
            {
                MessageBox.Show("Not enough note!");
                return;
            }

            double medie = result.Average(m => m.Valoare);
            _context.SaveChanges();
            MessageBox.Show("Medie Generala was calculated");
            LabelText = "Medie is " + medie.ToString() + "!";   
        }

        private ObservableCollection<Utilizator> _studentiExmatriculati;
        public ObservableCollection<Utilizator> StudentiExmatriculati
        {
            get { return _studentiExmatriculati; }
            set
            {
                _studentiExmatriculati = value;
                OnPropertyChanged(nameof(StudentiExmatriculati));
            }
        }

        private void LoadStudentiExmatriculati()
        {
            var result = _context.GetAllUtilizators()
                .Where(u => u.TipUtilizatorId == 2)
                .ToList();

            List<Utilizator> studentiExmatriculati = new List<Utilizator>();

            foreach (var student in result)
            {
                var unexcusedAbsentas = _context.GetUnexcusedAbsentasForStudent(student.UtilizatorId, 1);
                int unexcusedAbsencesCount = unexcusedAbsentas.Count();

                if (unexcusedAbsencesCount > 30 && !studentiExmatriculati.Any(s => s.UtilizatorId == student.UtilizatorId))
                {
                    studentiExmatriculati.Add(new Utilizator
                    {
                        UtilizatorId = student.UtilizatorId,
                        Nume = student.Nume
                    });
                }
            }

            foreach (var student in result)
            {
                var unexcusedAbsentas = _context.GetUnexcusedAbsentasForStudent(student.UtilizatorId, 2);
                int unexcusedAbsencesCount = unexcusedAbsentas.Count();

                if (unexcusedAbsencesCount > 30 && !studentiExmatriculati.Any(s => s.UtilizatorId == student.UtilizatorId))
                {
                    studentiExmatriculati.Add(new Utilizator
                    {
                        UtilizatorId = student.UtilizatorId,
                        Nume = student.Nume
                    });
                }
            }

            StudentiExmatriculati = new ObservableCollection<Utilizator>(studentiExmatriculati);
        }

        private int idDiriginte;

        public DiriginteMediiVM(int idDiriginte)
        {
            this.idDiriginte = idDiriginte;
            LoadStudentiExmatriculati();
            LoadStudentiForDiriginte();
        }

        public DiriginteMediiVM()
        {
            
        }
    }
}
