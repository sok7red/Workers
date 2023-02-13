using Microsoft.Extensions.Logging;

namespace SimpleServiceFramework
{
    public class MergeReportTransform : IDataTransform
    {
        private readonly ILogger<MergeReportTransform> _logger;

        public MergeReportTransform(ILogger<MergeReportTransform> logger)
        {
            _logger = logger;
        }

        public void TransformResults(string subJobId)
        {
            throw new NotImplementedException();
        }
    }
}
