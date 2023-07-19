using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHentaiAPI.Tests
{
    public class BaseUnitTest
    {
        protected NHentaiClient NHentaiClient { get; private set; }

        [TestInitialize]
        public void InitializeTest()
        {
            NHentaiClient = new TestNHentaiClient("a", new Dictionary<string, string>());
        }
    }

    public class TestNHentaiClient : NHentaiClient
    {
        #region Urls

        //protected override string ApiRootUrl => "https://nhent.ai";

        #endregion

        public TestNHentaiClient(string userAgent, Dictionary<string, string> cookies = null) : base(userAgent, cookies)
        {
        }
    }
}