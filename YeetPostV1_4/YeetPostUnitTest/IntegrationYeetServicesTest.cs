using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using YeetPostV1_4.Data;
using YeetPostV1_4.DataModel;

namespace YeetPostUnitTest
{
    public class IntegrationYeetServicesTest
    {

        private readonly YeetServices _yeetService = new YeetServices();
        [Fact]
        public void integrateGetYeet()
        {
            Assert.True(true);
        }

        [Fact]
        public void getYeetNew()
        {
            //arrange
            string V = "chattanooga";
            string guid = "6bd95277-c879-4e9d-933c-08927274d329";

            //act
            List<Yeet> model = _yeetService.GetYeetsByNew(V, guid);


            //assert
            Assert.Equal(V, model[0].location);
        }

        [Fact]
        public void getYeetTrend()
        {

            //arrange
            string V = "chattanooga";
            string guid = "6bd95277-c879-4e9d-933c-08927274d329";

            //act
            List<Yeet> model = _yeetService.GetYeetsByNew(V, guid);


            //assert
            Assert.Equal(V, model[0].location);
        }
        
        [Fact]
        public void WrongValuePass()
        {
            //arrange
            string V = "bruno";
            string guid = "mars";

            //act
            List<Yeet> model = _yeetService.GetYeetsByNew(V, guid);
            var size = model.Count;

            //assert>
            Assert.Equal(0, size);

        }


    }
}
