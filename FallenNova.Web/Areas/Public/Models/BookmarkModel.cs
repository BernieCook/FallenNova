using System.ComponentModel.DataAnnotations;

namespace FallenNova.Web.Areas.Public.Models
{
    public class BookmarkModel
    {
        public bool AddBookmark { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }
    }
}