using ExamXAML.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamXAML.ViewModel
{
    public class AppViewModel : ViewModelBase
    {
        private ICommand changeViewModelCommand;

        private IPageViewModel currentViewModel;

        //current view model
        public IPageViewModel CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                this.currentViewModel = value;
                this.OnPropertyChanged("CurrentViewModel");
            }
        }

        public List<IPageViewModel> AllViewModels { get; set; }

        public ICommand ChangeViewModel
        {
            get
            {
                if (this.changeViewModelCommand == null)
                {
                    this.changeViewModelCommand =
                        new RelayCommand(this.HandleChangeViewModelCommand);
                }
                return this.changeViewModelCommand;
            }
        }

        private void HandleChangeViewModelCommand(object parameter)
        {
            var newCurrentViewModel = parameter as IPageViewModel;
            this.CurrentViewModel = newCurrentViewModel;
        }

        public AppViewModel()
        {
            this.AllViewModels = new List<IPageViewModel>();
            this.AllViewModels.Add(new CinemaViewModel());
            this.AllViewModels.Add(new MoviesViewModel());
           // this.AllViewModels.Add(new ProjectionsViewModel());


            // this.CurrentViewModel = this.LoginRegisterVM;
        }
    }
}
