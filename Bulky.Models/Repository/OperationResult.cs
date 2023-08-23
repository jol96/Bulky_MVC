using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookWeb.Repository.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        //public T Result { get; set; }
        public ErrorModel Error { get; set; }
    }
}
