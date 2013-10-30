using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaReserve.ResponseModels;
using System.Net.Mail;

namespace ExamXAML.Data
{
    public class DataPersister
    {
        private const string BaseServicesUrl = "http://localhost:50971/api/";

        private static void ValidateEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Email is invalid", ex);
            }
        }

        internal static IEnumerable<CinemaModel> GetAllCinemas()
        {
            var allCinemas =
                 HttpRequester.Get<IEnumerable<CinemaModel>>(BaseServicesUrl + "cinemas");
            return allCinemas;
        }

        internal static IEnumerable<MovieModel> GetMoviesForCinema(int cinemaID)
        {
            var allMoviesInCinema =
                 HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "cinemas/" + cinemaID);
            return allMoviesInCinema;
        }

        internal static IEnumerable<ProjectionModel> GetMoviesProjections(int cinemaID, int movieID)
        {
            var allProjection =
                 HttpRequester.Get<IEnumerable<ProjectionModel>>(BaseServicesUrl + "cinemas/" + cinemaID + "/projections/" + movieID);
            return allProjection;
        }

        internal static IEnumerable<MovieModel> GetAllMovies()
        {
            var allMovies =
                 HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "movies");
            return allMovies;
        }

        internal static MovieDetailsModel GetDetails(int movieId)
        {
            var detail =
                 HttpRequester.Get<MovieDetailsModel>(BaseServicesUrl + "movies/" + movieId);
            return detail;
        }

        internal static IEnumerable<MovieModel> SearchForMovie(string searchKeyword)
        {
            //api/movies?keyword=pie
            var foundMovies =
                  HttpRequester.Get<IEnumerable<MovieModel>>(BaseServicesUrl + "movies?keyword=" + searchKeyword);
            return foundMovies;
        }

        internal static IEnumerable<ProjectionDetailsModel> GetMoviesProjectionsDetails(int p1, int p2, int p3)
        {
            var projectionDetails =
                  HttpRequester.Get<ProjectionDetailsModel>(BaseServicesUrl + "projections/" + p3);
            //I forget that it is not a collection
            var answer = new List<ProjectionDetailsModel>() { projectionDetails };
            return answer;
        }

        internal static string ReserveCall(object seats, string email, int projectionID)
        {
            ValidateEmail(email);

            var seatsCast = seats as IEnumerable<SeatModel>;
            CreateReservationModel model = new CreateReservationModel()
            {
                Seats = seatsCast,
                Email = email,
            };

            ReservationModel code = HttpRequester.Post<ReservationModel>(BaseServicesUrl + "projections/" + projectionID, model);
            return code.UserCode;
        }

        internal static void RemoveCommand(string email, string code, int projectionID)
        {
            ValidateEmail(email);
            RejectReservationModel removeModule = new RejectReservationModel()
            {
                Email = email,
                UserCode = code,
            };

            HttpRequester.Put(BaseServicesUrl + "projections/" + projectionID, removeModule);
         
        }
    }
}
