using Caliburn.Micro;
using StudentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;

namespace StudentManager.ViewModels
{
    public class ShellViewModel : Conductor<Object>
    {
        public StudentsRepository Repository { get; } //dostęp do mock-danych

        public ShellViewModel() 
        {
            Repository = new StudentsRepository(); //inicjalizacja mock-danych
        }
        public void ShowSearch()
        {
            ActivateItemAsync(new SearchViewModel(Repository));
        }

        public void ShowList()
        {
            ActivateItemAsync(new ListViewModel(Repository));
        }

        public void ShowAddPanel()
        {
            ActivateItemAsync(new AddPanelViewModel(Repository));
        }

        public void ShowDeletePanel()
        {
            ActivateItemAsync(new DeletePanelViewModel(Repository));
        }

    }
}
