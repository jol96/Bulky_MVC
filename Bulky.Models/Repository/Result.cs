using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookWeb.Repository.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? InnerResult { get; set; }
        public ErrorModel? Error { get; set; }
    }
}
