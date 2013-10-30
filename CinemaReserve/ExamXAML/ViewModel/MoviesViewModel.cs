using CinemaReserve.ResponseModels;
using ExamXAML.Commands;
using ExamXAML.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamXAML.ViewModel
{
    public class MoviesViewModel:ViewModelBase,IPageViewModel
    {
        public string Name
        {
            get { return "Movies page"; }
        }

        //which is the selected movie
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
            if (this.Movies.Count() > 0)
            {
                this.ChangeSelection(selected[0]);
            }
        }
        //attach
        public void ChangeSelection(object store)
        {
            //this must be category viewmodel
            this.SelectedMovie = store as MovieModel;
            //change cinema than change movies list
             ChangeMoviesDetail();
            // MessageBox.Show(this.SelectedMovie.Title);
        }

        private void ChangeMoviesDetail()
        {
            this.MovieDetail = DataPersister.GetDetails(this.SelectedMovie.Id);
        }

        private ObservableCollection<MovieModel> movies;

        public IEnumerable<MovieModel> Movies
        {
            get
            {
                if (this.movies == null)
                {
                    this.Movies = DataPersister.GetAllMovies();
                }
                return this.movies;
            }
            set
            {
                if (this.movies == null)
                {
                    this.movies = new ObservableCollection<MovieModel>();
                }
                this.movies.Clear();
                foreach (var item in value)
                {
                    this.movies.Add(item);
                }
            }
        }

        private MovieDetailsModel movieDetail;

        public MovieDetailsModel MovieDetail 
        {
            get 
            { 
                return this.movieDetail; 
            }
            set
            {
                this.movieDetail = value;
                this.OnPropertyChanged("MovieDetail");
            }
        }

        private string searchKeyword;
        public string SearchKeyword 
        {
            get { return this.searchKeyword; }
            set { this.searchKeyword = value; this.OnPropertyChanged("SearchKeyword"); }
        }

        private ICommand search;
        public ICommand Search
        {
            get
            {
                if (this.search == null)
                {
                    this.search = new RelayCommand(this.HandleSearchCommand);
                }
                return this.search;
            }
        }

        public void HandleSearchCommand(object obj)
        {
            this.FoundMovies = Data.DataPersister.SearchForMovie(this.SearchKeyword);
        }

        private ObservableCollection<MovieModel> foundMovies;

        public IEnumerable<MovieModel> FoundMovies
        {
            get
            {
                if (this.foundMovies == null)
                {
                    this.FoundMovies = DataPersister.SearchForMovie(this.SearchKeyword);
                }
                return this.foundMovies;
            }
            set
            {
                if (this.foundMovies == null)
                {
                    this.foundMovies = new ObservableCollection<MovieModel>();
                }
                this.foundMovies.Clear();
                foreach (var item in value)
                {
                    this.foundMovies.Add(item);
                }
            }
        }

        public MoviesViewModel()
        {
            this.FoundMovies = new ObservableCollection<MovieModel>();
        }
    }
}
