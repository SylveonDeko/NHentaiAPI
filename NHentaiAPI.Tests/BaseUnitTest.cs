using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHentaiAPI.Tests
{
    public class BaseUnitTest
    {
        protected NHentaiClient NHentaiClient { get; set; }

        [TestInitialize]
        public void InitializeTest()
        {
            NHentaiClient = new TestNHentaiClient();
        }

        [TestCleanup]
        public void CleanUp()
        {
            NHentaiClient.Dispose();
        }
    }

    public class TestNHentaiClient : NHentaiClient
    {
        #region Urls

        //protected override string ApiRootUrl => "https://nhent.ai";

        #endregion
    }
}