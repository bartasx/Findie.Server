using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Findie.Common.Models.IdentityModels;
using FindieServer.DbModels;
using FindieServer.Managers;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Findie.Tests
{
    public class LocationManagerTests

    {
        private UserManager<AppUser> userManager;
        private Mock<DatabaseContext> databaseContext;

        [SetUp]
        public void SetUp()
        {
            var userStore = new Mock<IUserStore<AppUser>>();
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            this.databaseContext = new Mock<DatabaseContext>(optionsBuilder);

            this.userManager =
                new UserManager<AppUser>(userStore.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task GetSpecificUserLocationAsyncTest()
        {
            var user = new List<AppUser>().ToAsyncDbSetMock();

            this.databaseContext.Setup(db => db.Users).Returns(user.Object);

            var locationManager = new LocationManager(this.databaseContext.Object, userManager);

            var result = await locationManager.GetSpecificUserLocation(user.Object.First().UserName);

            result.Should().NotBeNull();
        }
    }
}