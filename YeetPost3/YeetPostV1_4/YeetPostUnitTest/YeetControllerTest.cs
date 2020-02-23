using System;
using System.Collections.Generic;
using Xunit;
using YeetPostV1_4.Data;
using YeetPostV1_4.DataModels;
using YeetPostV1_4.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using FluentAssertions;
using System.IO;

namespace YeetPostUnitTest
{


    public class YeetControllerTest
    {
        private readonly ILogger<YeetController> _logger;


        /// <summary>
        /// testing a user id
        /// </summary>
        [Fact]
        public void ThrowEmptyUser()
        {
            // Arrange
            var controller = new YeetController(_logger);
            // Act
            // Expecting it to throw because the user inside Index is null and not set to anything
            Assert.Throws<NullReferenceException>(

                () => controller.Index() as ViewResult

                );
        }


        [Fact]
        public void PassWithUser()
        {
            // Arrange
            var username = "fakeUserName";

            var identity = new GenericIdentity(username, "");
            MemoryStream memStream = new MemoryStream();

            var mockPrincipal = new Mock<ClaimsPrincipal>();

            BinaryReader binReader = new BinaryReader(memStream);

            mockPrincipal.Setup(x => x.Identity).Returns(new ClaimsIdentity("tess"));
            mockPrincipal.Setup(x => x.Identity.IsAuthenticated).Returns(true);


            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User.Identity).Returns(new ClaimsIdentity("tess"));

            var mockYeets = new Mock<YeetController>();

            mockYeets.Setup(x => x.getUserId().Returns("12334545");
            



            Mock<ILogger<YeetController>> mockRepo = new Mock<ILogger<YeetController>>();

            //var controller = new YeetController(mockRepo.Object);

            var controller = new YeetController(mockRepo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object

                }
            };

            //act

            var viewResult = controller.Index() as ViewResult;


            //assert
            Assert.NotNull(viewResult);
        }

    }
}
