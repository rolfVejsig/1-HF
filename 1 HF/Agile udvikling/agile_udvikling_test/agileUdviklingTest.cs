using System;
using Xunit;
using MedarbejderIDLibrary;

namespace Agile_udvikling.Tests
{
    public class MedarbejderIDGeneratorTests
    {
        [Fact]
        public void perfektData()
        {
            // Arrange
            string fornavn = "Mads";
            string efternavn = "Andersen";

            // Act
            string medarbejderID = MedarbejderIDGenerator.GenerateMedarbejderID(fornavn, efternavn);

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
            string fornavn = "Bo";
            string efternavn = "Å";

            // Act
            string medarbejderID = MedarbejderIDGenerator.GenerateMedarbejderID(fornavn, efternavn);

            // Assert
            Assert.NotNull(medarbejderID);
            Assert.StartsWith(fornavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(0, 4));
            Assert.StartsWith(efternavn.Substring(0, 4).ToUpper(), medarbejderID.Substring(4, 4));
            Assert.True(int.TryParse(medarbejderID.Substring(8), out _));
        }
    }
}
