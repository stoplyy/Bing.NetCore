using Bing.Modularity;
using Bing.Samples.Data;
using Bing.Samples.Domain;

namespace Bing.Samples.Service
{
    /// <summary>
    /// Sample 服务模块
    /// </summary>
    [DependsOn(typeof(SamplesDataModule), typeof(SamplesDomainModule))]
    public class SamplesServiceModule : BingModule
    {
    }
}
