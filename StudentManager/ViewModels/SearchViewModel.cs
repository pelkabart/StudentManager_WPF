using Caliburn.Micro;
using StudentManager.Models;
using StudentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.ViewModels
{
    public class SearchViewModel : Screen
    {
		private string searchValue;
		private List<Student> searchResult = new List<Student>();
		private List<Student> found = new List<Student>();
        public List<Student> SearchResult { get; set; }
		public StudentsRepository repository;



		public List<Student> Found
		{
			get { return found; }
			set { found = value; NotifyOfPropertyChange(() => Found); }
		}

		public SearchViewModel(StudentsRepository repo)
		{
			repository = repo;
		}

        public string SearchValue
		{
			get { return searchValue; }
			set 
			{ 
				searchValue = value;
				NotifyOfPropertyChange(() => SearchValue);

				FilterStudents(searchValue);
			}
		}

		public void FilterStudents(string value)
		{
			if(value == "")
			{
				Found = null;
				return;
			}

			Found = repository.Search(value);


		}


	}
}
