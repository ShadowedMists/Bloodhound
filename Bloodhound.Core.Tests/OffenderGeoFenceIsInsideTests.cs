using Bloodhound.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bloodhound.Core.Tests
{
    [TestClass]
    public class OffenderGeoFenceIsInsideTests
    {
        [TestMethod]
        public void InsideTests()
        {
            OffenderGeoFence fence = new OffenderGeoFence()
            {
                NorthEastLatitude = 35.843014M,
                NorthEastLongitude = -83.340230M,
                SouthWestLatitude = 35.575827M,
                SouthWestLongitude = -83.815701M
            };

            Assert.IsTrue(fence.IsInside(35.702380M, -83.541691M));
            Assert.IsTrue(fence.IsInside(fence.NorthEastLatitude, fence.NorthEastLongitude));
            Assert.IsTrue(fence.IsInside(fence.SouthWestLatitude, fence.SouthWestLongitude));

            Assert.IsFalse(fence.IsInside(36.496245M, -83.556681M));
            Assert.IsFalse(fence.IsInside(34.613328M, -83.511595M));
            Assert.IsFalse(fence.IsInside(35.719336M, -86.137600M));
            Assert.IsFalse(fence.IsInside(35.724679M, -80.630775M));
        }
    }
}
