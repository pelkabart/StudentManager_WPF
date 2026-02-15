using Caliburn.Micro;
using StudentManager.Models;
using StudentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentManager.ViewModels
{
    class DeletePanelViewModel : Screen
    {
        private StudentsRepository repository;
        private string toDeleteIdValue;

        public DeletePanelViewModel(StudentsRepository repository)
        {
            this.repository = repository;
        }

        public string ToDeleteIdValue
        {
            get { return toDeleteIdValue; }
            set { toDeleteIdValue = value; NotifyOfPropertyChange(() => ToDeleteIdValue); }
        }

        public void DeleteStudentButton()
        {
            if(ToDeleteIdValue.Length == 4)
            {
                ToDeleteIdValue = ToDeleteIdValue.ToUpper();

                Student student = repository.GetStudent(ToDeleteIdValue);

                if(student != null)
                {
                    var result = MessageBox.Show($"Czy na pewno chcesz usunąć studenta {student.Name} {student.Surname}", "Ostrzeżenie", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    


                    if (repository.DeleteStudent(ToDeleteIdValue))
                    {
                        MessageBox.Show($"Usunięto studenta o Id {ToDeleteIdValue}", "Komunikat", MessageBoxButton.OK, MessageBoxImage.Information);
                        ToDeleteIdValue = "";
                    }
                }
                else
                {
                    MessageBox.Show($"NIe udało się usunąć studenta o Id {ToDeleteIdValue}", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ToDeleteIdValue = "";
                }


            }
            else
            {
                MessageBox.Show($"Wprowadź poprawny numer indeksu", "Ostrzeżenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                ToDeleteIdValue = "";
            }

        }

    }
}
