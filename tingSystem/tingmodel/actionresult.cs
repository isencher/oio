using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.bll
{
    public enum ActionResult
    {

        NoMethod = 0,
        None = 1,
        Success = 3,
        ParticalSuccess = 4,
        SuccessButNotLog = 5,
        PartialSuccessButNotLog = 6,
        Fail = 7,
        Error = 8,
        IsExist = 9
    }
}
