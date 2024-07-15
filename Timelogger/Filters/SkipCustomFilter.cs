using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Timelogger.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SkipCustomFilterAttribute : Attribute, IFilterMetadata
    {
        // This attribute doesn't need to do anything,
        // it just needs to be present on the actions you want to skip the filter.
    }
}
