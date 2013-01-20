using System.Collections.Generic;

namespace FallenNova.Web.Areas.Secure.Models.HomeModel
{
    public class HomeModel
    {
        public IEnumerable<UsersModel.UsersModel> LatestUsers { get; set; }
        public IEnumerable<UsersModel.UsersModel> LatestShips { get; set; }
        public IEnumerable<UsersModel.UsersModel> LatestItems { get; set; }
    }
}