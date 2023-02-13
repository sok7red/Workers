using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SimpleServiceFramework;


namespace Rabobank.RiskAnalytics.Common.Tests.Transforms
{

    public class TransformTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.test.json", optional: false)
                .Build();

            var loggerMergeReport = TestLogger.Create<MergeReportTransform>();
            var loggerFilterReport = TestLogger.Create<FilterReportTransform>();

            var services = new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton(loggerMergeReport)
                .AddSingleton(loggerFilterReport)
                .AddSingleton<MergeReportTransform>()
                .AddSingleton<FilterReportTransform>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void TransformResults()
        {
            var transforms = new List<IDataTransform>
            {
                _serviceProvider.GetService<MergeReportTransform>(),
                _serviceProvider.GetService<FilterReportTransform>()
            };

            foreach (var dataTransform in transforms)
            {
                Assert.Throws<NotImplementedException>(() =>
                    dataTransform.TransformResults("4095869"));
            }
        }
    }
}