using System;
using System.Collections.Generic;
using Xunit;
using YeetPostV1_4.Data;
using YeetPostV1_4.DataModels;

namespace YeetPostUnitTest
{
    public class AccountServiceTest
    {

        private readonly AccountServices _yeetServices = new AccountServices();
        [Fact]
        public void GetCorrectLocation()
        {

            string actualLocation = _yeetServices.getLocation("9187c9cd-cd64-496b-ad1b-9a0ca01bfc38");

            Assert.Equal("chattanooga",actualLocation);
        }

        [Fact]
        public void LocationPassNothing()
        {
            //testing to see if argument throws out of range exception.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => _yeetServices.getLocation("")
                );
        }

        


    }
}
