using ExamXAML.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CinemaReserve.ResponseModels;
using System.Windows;
using System.Collections.ObjectModel;
using ExamXAML.Data;

namespace ExamXAML.ViewModel
{
    public class CinemaViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get { return "Cinema page"; }
        }


        //which is the selected movie
        private CinemaModel selectCinema;
        public CinemaModel SelectCinema
        {
            get { return this.selectCinema; }
            set
            {
                this.selectCinema = value;
                this.OnPropertyChanged("SelectCinema");
            }
        }

        //attach
        private ICommand selectionChange;
        //attach
        public ICommand SelectionChange
        {
            get
            {
                if (this.selectionChange == null)
                {
                    this.selectionChange = new RelayCommand(this.HandleSelectionChangeCommand);
                }
                return this.selectionChange;
            }
        }
        //attach
        private void HandleSelectionChangeCommand(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            var selected = args.AddedItems;
            if (this.Cinemas.Count() > 0)
            {
                this.ChangeSelection(selected[0]);
            }
        }
        //attach
        public void ChangeSelection(object store)
        {
            //this must be category viewmodel
            this.SelectCinema = store as CinemaModel;
            //change cinema than change movies list
            ChangeMovies();
            // MessageBox.Show(this.selectCinema.Name);
        }

        //-------------------------------------------------------------

        private MovieModel selectedMovie;
        public MovieModel SelectedMovie
        {
            get { return this.selectedMovie; }
            set
            {
                this.selectedMovie = value;
                this.OnPropertyChanged("SelectedMovie");
            }
        }


        //attach
        private ICommand selectionChangeMovie;
        //attach
        public ICommand SelectionChangeMovie
        {
            get
            {
                if (this.selectionChangeMovie == null)
                {
                    this.selectionChangeMovie = new RelayCommand(this.HandleSelectionChangeMovieCommand);
                }
                return this.selectionChangeMovie;
            }
        }
        //attach
        private void HandleSelectionChangeMovieCommand(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            var selected = args.AddedItems;
            if (this.MoviesInSelectedCinema.Count() > 0)
            {
                this.ChangeSelectionMovie(selected[0]);
            }
        }
        //attach
        public void ChangeSelectionMovie(object store)
        {
            //this must be category viewmodel
            this.SelectedMovie = store as MovieModel;
            //change cinema than change movies list
            ChangeProjectionForMovie();
            //  MessageBox.Show(this.SelectedMovie.Title);
        }

        private ObservableCollection<ProjectionModel> projections;

        public IEnumerable<ProjectionModel> Projections
        {
            get
            {
                if (this.projections == null)
                {

                    this.Projections = DataPersister.GetMoviesProjections(this.SelectCinema.Id, this.SelectedMovie.Id);


                }

                return this.projections;

            }
            set
            {
                if (this.projections == null)
                {
                    this.projections = new ObservableCollection<ProjectionModel>();
                }

                this.projections.Clear();
                foreach (var item in value)
                {
                    this.projections.Add(item);
                }
            }
        }

        public void ChangeProjectionForMovie()
        {
            this.Projections = DataPersister.GetMoviesProjections(this.SelectCinema.Id, this.SelectedMovie.Id);
        }
        //--------------------------------------------------------------------------




        private ObservableCollection<MovieModel> moviesInSelectedCinema;

        public IEnumerable<MovieModel> MoviesInSelectedCinema
        {
            get
            {
                if (this.moviesInSelectedCinema == null)
                {
                    this.MoviesInSelectedCinema = DataPersister.GetMoviesForCinema(this.SelectCinema.Id);
                }

                return this.moviesInSelectedCinema;
            }
            set
            {
                if (this.moviesInSelectedCinema == null)
                {
                    this.moviesInSelectedCinema = new ObservableCollection<MovieModel>();
                }

                this.moviesInSelectedCinema.Clear();
                foreach (var item in value)
                {
                    this.moviesInSelectedCinema.Add(item);
                }
            }
        }

        public void ChangeMovies()
        {
            this.MoviesInSelectedCinema = DataPersister.GetMoviesForCinema(this.SelectCinema.Id);
        }

        private ObservableCollection<CinemaModel> cinemas;

        public IEnumerable<CinemaModel> Cinemas
        {
            get
            {
                if (this.cinemas == null)
                {
                    this.Cinemas = DataPersister.GetAllCinemas();
                }
                return this.cinemas;
            }
            set
            {
                if (this.cinemas == null)
                {
                    this.cinemas = new ObservableCollection<CinemaModel>();
                }
                this.cinemas.Clear();
                foreach (var item in value)
                {
                    this.cinemas.Add(item);
                }
            }
        }

        //------------------------projection---------------------------


        private ProjectionModel selectedProjection;
        public ProjectionModel SelectedProjection
        {
            get { return this.selectedProjection; }
            set
            {
                this.selectedProjection = value;
                this.OnPropertyChanged("SelectedProjection");
            }
        }


        //attach
        private ICommand selectionChangeProjection;
        //attach
        public ICommand SelectionChangeProjection
        {
            get
            {
                if (this.selectionChangeProjection == null)
                {
                    this.selectionChangeProjection = new RelayCommand(this.HandleSelectionChangeProjectionCommand);
                }
                return this.selectionChangeProjection;
            }
        }
        //attach
        private void HandleSelectionChangeProjectionCommand(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            var selected = args.AddedItems;
            if (this.Projections.Count() > 0 && selected.Count > 0)
            {
                this.ChangeSelectionProjection(selected[0]);
            }
        }
        //attach
        public void ChangeSelectionProjection(object store)
        {
            //this must be category viewmodel
            this.SelectedProjection = store as ProjectionModel;
            //change cinema than change movies list
             ChangeProjectionDetailsForMovie();
            //  MessageBox.Show(this.SelectedMovie.Title);
        }


        public void ChangeProjectionDetailsForMovie()
        {
            this.ProjectionsDetails = DataPersister.GetMoviesProjectionsDetails(this.SelectCinema.Id, 
                this.SelectedMovie.Id, this.SelectedProjection.Id);
        }

        private ObservableCollection<ProjectionDetailsModel> projectionsDetails;

        public IEnumerable<ProjectionDetailsModel> ProjectionsDetails
        {
            get
            {
                if (this.projectionsDetails == null)
                {

                    this.ProjectionsDetails = DataPersister.GetMoviesProjectionsDetails(this.SelectCinema.Id, this.SelectedMovie.Id, this.SelectedProjection.Id);


                }

                return this.projectionsDetails;

            }
            set
            {
                if (this.projectionsDetails == null)
                {
                     this.projectionsDetails = new ObservableCollection<ProjectionDetailsModel>();
                }

                this.projectionsDetails.Clear();
                foreach (var item in value)
                {
                    this.projectionsDetails.Add(item);
                }
            }
        }

        //---------------reservation-------------------------------



        private List<SeatModel> seats;
        public List<SeatModel> Seats
        {
            get { return this.seats; }
            set
            {
                this.seats = value;
             //   this.OnPropertyChanged("Seats");
            }
        }

        //attach
        private ICommand reservationCommand;
        //attach
        public ICommand ReservationCommand
        {
            get
            {
                if (this.reservationCommand == null)
                {
                    this.reservationCommand = new RelayCommand(this.HandleReservationCommand);
                }
                return this.reservationCommand;
            }
        }
        //attach
        private void HandleReservationCommand(object parameter)
        {
            var args = parameter as SelectionChangedEventArgs;
            var selected = args.AddedItems;
            if (selected.Count > 0)
            {
                this.Seats.Add(selected[0] as SeatModel);
               
            }
        }
        //attach
       


        private string email;
        public string  Email 
        {
            get { return this.email; }
            set 
            { 
                this.email = value; 
                this.OnPropertyChanged("Email"); 
            }
        }

        private ICommand reservationCallCommand;
        //attach
        public ICommand ReservationCallCommand
        {
            get
            {
                if (this.reservationCallCommand == null)
                {
                    this.reservationCallCommand = new RelayCommand(this.HandleReservationCallCommand);
                }
                return this.reservationCallCommand;
            }
        }

        private void HandleReservationCallCommand(object parameter)
        {
            this.Code = DataPersister.ReserveCall(this.Seats, this.Email, this.SelectedProjection.Id);
        }

        public string Code { get; set; }

        //----------remove reservation 
        private ICommand remove;
        //attach
        public ICommand Remove
        {
            get
            {
                if (this.remove == null)
                {
                    this.remove = new RelayCommand(this.HandleRemoveCommand);
                }
                return this.remove;
            }
        }

        private void HandleRemoveCommand(object parameter)
        {
            DataPersister.RemoveCommand(this.Email, this.Code, this.SelectedProjection.Id);
        }

        public CinemaViewModel()
        {
            this.MoviesInSelectedCinema = new ObservableCollection<MovieModel>();
            this.Projections = new ObservableCollection<ProjectionModel>();
            this.ProjectionsDetails = new ObservableCollection<ProjectionDetailsModel>();
            this.Seats = new List<SeatModel>();
        }
    }
}
