using Caliburn.Micro;
using StudentManager.Models;
using StudentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.ViewModels
{
    public class ListViewModel : Screen
    {
		private List<Student> students;
        public StudentsRepository repository;

        public ListViewModel(StudentsRepository repository)
        {
            this.repository = repository;
        }

        public List<Student> Students
		{
			get {students = repository.GetStudents(); return students; }
		}

	}
}
