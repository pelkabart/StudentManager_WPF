using Caliburn.Micro;
using StudentManager.Models;
using StudentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace StudentManager.ViewModels
{
    public class AddPanelViewModel : Screen
    {
        public StudentsRepository repository;
        private List<Student> students;
        private string currentIdNumber;
        private string newStudentName;
        private string newStudentSurname;
        private string newStudentYear;

        public AddPanelViewModel(StudentsRepository repository)
        {
            this.repository = repository;
            CurrentIdNumber = repository.nextId();
        }

        public List<Student> Students
        {
            get { students = repository.GetStudents(); return students; }
        }

        public string CurrentIdNumber
        {
            get { return currentIdNumber; }
            set { currentIdNumber = value; NotifyOfPropertyChange(() => CurrentIdNumber); }
        }

        

        public string NewStudentName
        {
            get { return newStudentName; }
            set { newStudentName = value; NotifyOfPropertyChange(() => NewStudentName); NotifyOfPropertyChange(() => CanAddStudentButton); }
        }

        public string NewStudentSurname
        {
            get { return newStudentSurname; }
            set { newStudentSurname = value; NotifyOfPropertyChange(() => NewStudentSurname); NotifyOfPropertyChange(() => CanAddStudentButton); }
        }

        public string NewStudentYear
        {
            get { return newStudentYear; }
            set { newStudentYear = value; NotifyOfPropertyChange(() => NewStudentYear); NotifyOfPropertyChange(() => CanAddStudentButton); }
        }


        public bool CanAddStudentButton =>
            !string.IsNullOrWhiteSpace(NewStudentName)
            && !string.IsNullOrWhiteSpace(NewStudentSurname)
            && new[] { "1", "2", "3", "4", "5" }.Contains(NewStudentYear);

        public void AddStudentButton()
        {
            if (repository.AddStudent(NewStudentName, NewStudentSurname, NewStudentYear))
            {
                MessageBox.Show($"Dodano studenta {NewStudentName} {NewStudentSurname}", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
                
                CurrentIdNumber = repository.nextId();
                NewStudentName = "";
                NewStudentSurname = "";
                NewStudentYear = "";
            }
            else
            {
                MessageBox.Show($"Nie udało się dodać studenta", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



    }
}
