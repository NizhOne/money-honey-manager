using API.Constants;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace API.Models
{
    public class OperationsRequest
    {
        [DataMember(Name ="category")]
        public IList<Guid> CategoriesIds { get; set; }

        public CategoryType? CategoryType { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public decimal? LessThen { get; set; }

        public decimal? MoreThen { get; set; }
    }
}
