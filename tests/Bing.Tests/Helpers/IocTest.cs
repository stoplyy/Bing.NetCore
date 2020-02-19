using System.Collections.Generic;
using Autofac;
using Bing.Dependency;
using Bing.Helpers;
using Bing.Tests.Samples;
using Xunit;

namespace Bing.Tests.Helpers
{
    /// <summary>
    /// Ioc测试
    /// </summary>
    public class IocTest
    {
        /// <summary>
        /// 初始化Ioc测试
        /// </summary>
        public IocTest()
        {
        }

        /// <summary>
        /// 测试创建实例
        /// </summary>
        [Fact]
        public void TestCreate()
        {
            var sample = Ioc.Create<ISample>();
            Assert.NotNull(sample);
        }

        /// <summary>
        /// 测试集合
        /// </summary>
        [Fact]
        public void TestCollection()
        {
            var samples = Ioc.Create<IEnumerable<ISample>>();
            Assert.NotNull(samples);
            Assert.Single(samples);
        }

        /// <summary>
        /// 创建集合
        /// </summary>
        [Fact]
        public void TestCreateList()
        {
            var samples = Ioc.CreateList<ISample>();
            Assert.NotNull(samples);
            Assert.Single(samples);
        }
    }
}
