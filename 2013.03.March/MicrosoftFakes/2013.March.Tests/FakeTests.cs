using System;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2013.March.Domain;

namespace _2013.March.Tests
{
    [TestClass]
    public class FakeTests
    {
        [TestMethod]
        public void CallingDateProvider_ShouldReturn1970()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => { return new DateTime(1970, 1, 1); };

                var date = MyDateProvider.GetCurrentDateTime();

                Assert.IsTrue(date == new DateTime(1970, 1, 1));
            }
        }
    }


    public class MyDateProvider
    {
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
 