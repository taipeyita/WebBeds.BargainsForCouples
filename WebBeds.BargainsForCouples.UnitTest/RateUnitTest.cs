using WebBeds.BargainsForCouples.Core.Helpers;
using Xunit;

namespace WebBeds.BargainsForCouples.UnitTest
{
    public class RateUnitTest
    {
        [Fact]
        public void Test_With_False_RateType()
        {

           var result = RateHelper.Calculate("FalseRateTYPE", 1, 200);
            Assert.Equal<double>(200, result);
        }

        [Fact]
        public void Test_With_False_RateType_And_4_Nights()
        {
            var result = RateHelper.Calculate("FalseRateTYPE", 4, 200);
            Assert.Equal<double>(200, result);
        }

        [Fact]
        public void Test_With_Correct_RateType_1_Night()
        {
            var result = RateHelper.Calculate("PerNight", 1, 200);
            Assert.Equal<double>(200, result);
        }

        [Fact]
        public void Test_With_Correct_RateType_3_Nights()
        {
            var result = RateHelper.Calculate("PerNight", 3, 200);
            Assert.Equal<double>(600, result);
        }

        [Fact]
        public void Test_With_Correct_RateType_With_Different_Camelcase()
        {
            var result = RateHelper.Calculate("PERNight", 3, 200);
            Assert.Equal<double>(600, result);
        }
    }
}
