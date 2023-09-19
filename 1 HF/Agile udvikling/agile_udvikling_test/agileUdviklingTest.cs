using System;
using Xunit;

namespace Agile_udvikling.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void perfektData()
        {
            // Arrange
            string fornavn = "mads";
            string efternavn = "andersen";

            // Act
            string medarbejderID = Program.GenerateMedarbejderID(fornavn, efternavn);

            // Assert
            Assert.NotNull(medarbejderID);
            Assert.StartsWith(fornavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(0, 4));
            Assert.StartsWith(efternavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(4, 4));
            Assert.True(int.TryParse(medarbejderID.Substring(8), out _));
        }

        [Fact]
        public void uperfektData()
        {
            // Arrange
            string fornavn = "bo";
            string efternavn = "P";

            // Act
            string medarbejderID = Program.GenerateMedarbejderID(fornavn, efternavn);

            // Assert
            Assert.NotNull(medarbejderID);
            Assert.StartsWith(fornavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(0, 4));
            Assert.StartsWith(efternavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(4, 4));
            Assert.True(int.TryParse(medarbejderID.Substring(8), out _));
        }
    }
}
