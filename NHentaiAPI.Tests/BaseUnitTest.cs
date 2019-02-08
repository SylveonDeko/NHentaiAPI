using System;
using System.Collections.Generic;
using System.Text;

namespace NHentaiAPI.Tests
{
    public class BaseUnitTest
    {
        protected NHentaiClient CreateNHentaiClient()
        {
            return new TestNHentaiClient();
        }
    }

    public class TestNHentaiClient : NHentaiClient
    {
        protected override string ApiRootUrl => "https://nhent.ai";
    }
}
