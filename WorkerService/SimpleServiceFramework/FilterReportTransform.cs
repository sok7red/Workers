using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleServiceFramework
{
    public class FilterReportTransform : IDataTransform
    {
        private readonly ILogger<FilterReportTransform> _logger;

        public FilterReportTransform(ILogger<FilterReportTransform> logger)
        {
            _logger = logger;
        }

        public void TransformResults(string subJobId)
        {
            throw new NotImplementedException();
        }
    }
}
