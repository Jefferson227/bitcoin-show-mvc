using System.ComponentModel.DataAnnotations;

namespace BitcoinShow.Web.Models
{
    public class OptionViewModel
    {
        /// <summary>
        ///     Option Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Option text
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(200)]
        public string Text { get; set; }

        public int QuestionId { get; set; }
    }
}
