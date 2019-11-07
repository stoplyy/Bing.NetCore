using Xunit;

namespace System
{
    /// <summary>
    /// 整数(<see cref="Int32"/>) 扩展
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class IntegerExtensions_Tests
    {
        [Theory(DisplayName = nameof(IntegerExtensions.ToArray))]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(-1, true)]
        public void ToArray(int? count, bool throws = false)
        {
            if (throws)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => count.ToArray());

                return;
            }

            var array = count.ToArray(ToArrayOptions.UseIndex);

            if (count == null)
            {
                Assert.Null(array);
            }
            else
            {
                Assert.NotNull(array);
                Assert.Equal(count.Value, array.Length);

                for (var i = 0; i < array.Length; i++)
                {
                    Assert.Equal(i, array[i]);
                }
            }
        }
    }
}
