using Bing.Datas.Dapper.MySql;
using Bing.Datas.Sql;
using Bing.Datas.Sql.Builders.Core;
using Bing.Datas.Sql.Matedatas;
using Bing.Helpers;
using Bing.Tests.Samples;
using Xunit;
using Xunit.Abstractions;

namespace Bing.Datas.Test.Integration.Sql.Builders.MySql
{
    /// <summary>
    /// MySql Sql生成器例子测试
    /// </summary>
    public class MySqlBuilderSampleTest : TestBase
    {
        /// <summary>
        /// Sql生成器
        /// </summary>
        private readonly ISqlBuilder _builder;

        /// <summary>
        /// 初始化一个<see cref="MySqlBuilderSampleTest"/>类型的实例
        /// </summary>
        public MySqlBuilderSampleTest(ITestOutputHelper output) : base(output)
        {
            _builder = new MySqlBuilder(new DefaultEntityMatedata(), new DefaultTableDatabase(),
                new ParameterManager(new MySqlDialect()));
        }

        /// <summary>
        /// 查询名称中包含 "Ja" 并且年龄小于30的用户
        /// </summary>
        [Fact]
        public void Test_1()
        {
            // 结果
            var result = new Str();
            result.AppendLine("Select `UserQuerySample`.`Id`,`UserQuerySample`.`Name`,`UserQuerySample`.`Age`,`UserQuerySample`.`Email` ");
            result.AppendLine("From `UserQuerySample` ");
            result.Append("Where `UserQuerySample`.`Name` Like '%Ja%' And `UserQuerySample`.`Age`<=30");

            //执行
            _builder.Select<UserQuerySample>()
                .From<UserQuerySample>()
                .Contains<UserQuerySample>(x => x.Name, "Ja")
                .Less<UserQuerySample>(x => x.Age, 30);

            // 验证
            var debugSql = _builder.ToDebugSql();
            Output.WriteLine(debugSql);
            Assert.Equal(result.ToString(), debugSql);
        }

        /// <summary>
        /// 查询名称中包含 "a" 并且年龄大于等于15且年龄小于等于25，且邮箱不能为空
        /// </summary>
        [Fact]
        public void Test_2()
        {
            // 结果
            var result = new Str();
            result.AppendLine("Select `UserQuerySample`.`Id`,`UserQuerySample`.`Name`,`UserQuerySample`.`Age`,`UserQuerySample`.`Email` ");
            result.AppendLine("From `UserQuerySample` ");
            result.Append("Where `UserQuerySample`.`Name` Like '%a%' And `UserQuerySample`.`Age`>=15 And `UserQuerySample`.`Age`<=25 And `UserQuerySample`.`Email` Is Not Null");

            //执行
            _builder.Select<UserQuerySample>()
                .From<UserQuerySample>()
                .Contains<UserQuerySample>(x => x.Name, "a")
                .Between<UserQuerySample>(x => x.Age, 15, 25)
                .IsNotNull<UserQuerySample>(x => x.Email);

            // 验证
            var debugSql = _builder.ToDebugSql();
            Output.WriteLine(debugSql);
            Assert.Equal(result.ToString(), debugSql);
        }

        /// <summary>
        /// 查询名字中 "J" 开头并且年龄大于26，按照年龄降序排列，年龄相同按照id升序排列
        /// </summary>
        [Fact]
        public void Test_3()
        {
            // 结果
            var result = new Str();
            result.AppendLine("Select `UserQuerySample`.`Id`,`UserQuerySample`.`Name`,`UserQuerySample`.`Age`,`UserQuerySample`.`Email` ");
            result.AppendLine("From `UserQuerySample` ");
            result.AppendLine("Where `UserQuerySample`.`Name` Like 'J%' And `UserQuerySample`.`Age`>=26 ");
            result.Append("Order By `UserQuerySample`.`Age` Desc,`UserQuerySample`.`Id`");

            //执行
            _builder.Select<UserQuerySample>()
                .From<UserQuerySample>()
                .Starts<UserQuerySample>(x => x.Name, "J")
                .GreaterEqual<UserQuerySample>(x => x.Age, 26)
                .OrderBy<UserQuerySample>(x => x.Age, true)
                .OrderBy<UserQuerySample>(x => x.Id);

            // 验证
            var debugSql = _builder.ToDebugSql();
            Output.WriteLine(debugSql);
            Assert.Equal(result.ToString(), debugSql);
        }
    }
}
