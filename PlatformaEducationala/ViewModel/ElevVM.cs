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
    public class ElevVM : BaseVM
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
                if(value != null)
                {
                    LoadNote();
                    LoadAbsente();
                    LoadMedii();
                }
            }
        }

        private void LoadMaterii()
        {
            var result = _context.GetAllMateries();
            Materii = new ObservableCollection<Materie>(result.Select(r => new Materie
            {
                MaterieId = r.MaterieId,
                Nume = r.Nume
            })); ;
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
            Note = null;
            var result = _context.GetAllNotas()
                .Where(n => n.StudentId == IdStudent && n.MaterieId == SelectedMaterie.MaterieId)
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
            Absente = null;

            var result = _context.GetAllAbsentasForStudent(IdStudent, 1).Where(s => s.MaterieId == SelectedMaterie.MaterieId);

            Absente = new ObservableCollection<Absenta>(result.Select(r => new Absenta
            {
                AbsentaId = r.AbsentaId,
                MaterieId = r.MaterieId,
                Data = r.Data,
                Semestru = r.Semestru,
                Motivata = r.Motivata,
                Nemotivata = r.Nemotivata

            }));

            result = _context.GetAllAbsentasForStudent(IdStudent, 2).Where(s => s.MaterieId == SelectedMaterie.MaterieId);

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
            Medii = null;
            var result = _context.GetMediesForStudent(IdStudent).Where(s => s.MaterieId == SelectedMaterie.MaterieId);
            Medii = new ObservableCollection<Medie>(result.Select(r => new Medie
            {
                MedieId = r.MedieId,
                Valoare = r.Valoare,
                Semestru = r.Semestru,
                MaterieId = r.MaterieId,
                StudentId = r.StudentId,
            }));
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
            var result = _context.GetMediesForStudent(IdStudent).ToList();

            if (result.Count() == 0)
            {
                MessageBox.Show("Not enough note!");
                return;
            }

            double medie = result.Average(m => m.Valoare);
            _context.SaveChanges();
            LabelText = "Medie is " + medie.ToString() + "!";
        }

        private int IdStudent;

        public ElevVM(int IdStudent)
        {
            this.IdStudent = IdStudent;
            LoadMaterii();   
        }

        public ElevVM()
        {
        }

    }
}
