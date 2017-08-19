using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HolidayMangement
{
    [MetadataType(typeof(HolidayMetadata))]
    public partial class Holiday
    {

    }

    public class HolidayMetadata
    {
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide holiday Type.")]
        public long HolidayType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide holiday name.")]
        public string HolidayName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide holiday Date.")]
        [DataType(DataType.Date)]
        public string HolidayDate { get; set; }
    }
}