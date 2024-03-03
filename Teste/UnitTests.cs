using CakeMakerProj;
using System.IO;

namespace Teste
{
    public class UnitTests
    {
        // Cake
        [TestFixture]
        public class CakeTests
        {

            [Test]
            public void Cake_Constructor_Succeeded()
            {
                // Act
                Cake cake = new Cake("Cheesecake");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Cheesecake"));
            }

            [Test]
            public void Cake_Constructor_Failed()
            {
                // Act
                Cake cake = new Cake("Cheesecake");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Chocolate"));
            }

            [Test]
            public void Cake_DefaultConstructor_Succeeded()
            {
                // Act
                Cake cake = new Cake();
                // Assert
                Assert.IsTrue(string.IsNullOrEmpty(cake.GetName()));
            }

            [Test]
            public void Cake_ConstructorLongName_Succeeded()
            {
                // Act
                Cake cake = new Cake("Cake with strawberries and whipped cream");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Cake with strawberries and whipped cream"));
            }

            [Test]
            public void Cake_SetName_Succeeded()
            {
                // Arrange
                Cake cake = new Cake();
                // Act
                cake.SetName("ChocolateCake");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("ChocolateCake"));
            }
        }

        // RecipeCake
        [TestFixture]
        public class RecipeCakeTests
        {
            private StringWriter stringWriter;
            private TextWriter originalConsoleOut;

            [SetUp]
            public void RedirectConsoleOutput()
            {
                // Redirect Console.Out to a StringWriter
                stringWriter = new StringWriter();
                originalConsoleOut = Console.Out;
                Console.SetOut(stringWriter);
            }

            [TearDown]
            public void RestoreConsoleOutput()
            {
                // Restore Console.Out to its original value
                Console.SetOut(originalConsoleOut);
                stringWriter.Dispose();
            }

            [Test]
            public void RecipeCake_DefaultConstructor_Succeeded()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Assert
                Assert.IsTrue(string.IsNullOrEmpty(cake.GetName()));
                Assert.That(cake.GetTime(), Is.EqualTo(0));
            }

            [Test]
            public void RecipeCake_Constructor_Succeeded()
            {
                // Arrange
                RecipeCake cake = new RecipeCake("Tiramisu", 5);

                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Tiramisu"));
                Assert.That(cake.GetTime(), Is.EqualTo(5));
            }

            [Test]
            public void RecipeCake_Constructor_Failed1()
            {
                // Arrange
                RecipeCake cake = new RecipeCake("Tiramisu", 5);

                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Velvet Vanilla Dream"));
                Assert.That(cake.GetTime(), Is.EqualTo(5));
            }

            [Test]
            public void RecipeCake_Constructor_Failed2()
            {
                // Arrange
                RecipeCake cake = new RecipeCake("Tiramisu", 5);

                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Tiramisu"));
                Assert.That(cake.GetTime(), Is.EqualTo(10));
            }

            [Test]
            public void RecipeCake_SetName_Succeeded()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Act
                cake.SetName("Berry Bliss");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Berry Bliss"));
            }

            [Test]
            public void RecipeCake_SetName_Failed()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Act
                cake.SetName("Berry Bliss");
                // Assert
                Assert.That(cake.GetName(), Is.EqualTo("Choco Delight"));
            }

            [Test]
            public void RecipeCake_SetTime_Succeeded()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Act
                cake.SetTime(45);
                // Assert
                Assert.That(cake.GetTime(), Is.EqualTo(45));
            }

            [Test]
            public void RecipeCake_SetTime_Failed()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Act
                cake.SetTime(15);
                // Assert
                Assert.That(cake.GetTime(), Is.EqualTo(20));
            }

            [Test]
            public void RecipeCake_SetNegativeTime_Succeeded()
            {
                // Arrange
                RecipeCake cake = new RecipeCake();
                // Act
                cake.SetTime(-10);
                // Assert
                Assert.GreaterOrEqual(cake.GetTime(), 0);
            }
        }

        // CakeMaker
        [TestFixture]
        public class CakeMakerTests
        {
            private StringWriter stringWriter;
            private TextWriter originalConsoleOut;

            [SetUp]
            public void RedirectConsoleOutput()
            {
                // Redirect Console.Out to a StringWriter
                stringWriter = new StringWriter();
                originalConsoleOut = Console.Out;
                Console.SetOut(stringWriter);
            }

            [TearDown]
            public void RestoreConsoleOutput()
            {
                // Restore Console.Out to its original value
                Console.SetOut(originalConsoleOut);
                stringWriter.Dispose();
            }

            [Test]
            public void CakeMaker_TakeCommand_Succeeded()
            {
                // Arrange
                RecipeCake recipe = new RecipeCake("Peanut Paradise", 8);
                CakeMaker cakeMaker = new CakeMaker();
                // Act
                Cake orderedCake = cakeMaker.TakeCommand(recipe);
                // Assert
                Assert.That(orderedCake.GetName(), Is.EqualTo("Peanut Paradise"));
            }

            [Test]
            public void CakeMaker_TimeInTakeCommand_Succeeded()
            {
                // Arrange
                RecipeCake recipe = new RecipeCake("Strawberry Cheesecake", 6);
                CakeMaker cakeMaker = new CakeMaker();
                var start = DateTime.Now;
                // Act
                Cake orderedCake = cakeMaker.TakeCommand(recipe);
                var end = DateTime.Now;
                var duration = (end - start).Seconds;
                // Assert
                Assert.That(duration, Is.EqualTo(6));
            }

            [Test]
            public void CakeMaker_NegativeTimeInTakeCommand_Succeeded()
            {
                // Arrange
                RecipeCake recipe = new RecipeCake("Negative Time Cake", -5);
                CakeMaker cakeMaker = new CakeMaker();
                // Act
                Cake orderedCake = cakeMaker.TakeCommand(recipe);
                // Assert
                Assert.That(orderedCake.GetName(), Is.EqualTo("Negative Time Cake"));
                Assert.That(recipe.GetTime(), Is.EqualTo(0));
            }

            [Test]
            public void CakeMaker_MessagesInTakeCommand_Succeeded()
            {
                // Arrange
                RecipeCake recipe = new RecipeCake("Vanilla Cake", 10);
                CakeMaker cakeMaker = new CakeMaker();
                // Capture the standard output to check the displayed messages
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    // Act
                    Cake orderedCake = cakeMaker.TakeCommand(recipe);
                    // Assert
                    Assert.That(sw.ToString(), Is.EqualTo("The dessert Vanilla Cake will be ready in 10 seconds!\r\nThe dessert Vanilla Cake is ready!\r\n\r\n"));
                }
            }
        }

        // CarouselOfCakes
        [TestFixture]
        public class CarouselOfCakesTests
        {
            private StringWriter stringWriter;
            private TextWriter originalConsoleOut;

            [SetUp]
            public void RedirectConsoleOutput()
            {
                // Redirect Console.Out to a StringWriter
                stringWriter = new StringWriter();
                originalConsoleOut = Console.Out;
                Console.SetOut(stringWriter);
            }

            [TearDown]
            public void RestoreConsoleOutput()
            {
                // Restore Console.Out to its original value
                Console.SetOut(originalConsoleOut);
                stringWriter.Dispose();
            }

            [Test]
            public void CarouselOfCakes_DefaultConstructor_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                // Act
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(currentCapacity, Is.EqualTo(0));
            }

            [Test]
            public void CarouselOfCakes_GetMaxCapacity_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                // Act
                uint maxCapacity = carousel.GetMaxCapacity();
                // Assert
                Assert.That(maxCapacity, Is.EqualTo(12));
            }

            [Test]
            public void CarouselOfCakes_GetLowLimit_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                // Act
                uint lowLimit = carousel.GetLowLimit();
                // Assert
                Assert.That(lowLimit, Is.EqualTo(3));
            }

            [Test]
            public void CarouselOfCakes_GetStorage_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                Cake[] cakes = new Cake[] { new Cake("Chocolate Cake"), new Cake("Vanilla Cake") };
                foreach (Cake cake in cakes)
                {
                    carousel.AddCake(cake);
                }
                // Act
                Cake[] storage = carousel.GetStorage();
                // Assert
                Assert.That(carousel.GetCurrentCapacity(), Is.EqualTo(cakes.Length));
                for (int i = 0; i < cakes.Length; i++)
                {
                    Assert.That(storage[i].GetName(), Is.EqualTo(cakes[i].GetName()));
                }
            }

            [Test]
            public void CarouselOfCakes_AddCakeInCarousel_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                Cake cake = new Cake("Chocolate Cake");
                // Act
                carousel.AddCake(cake);
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(currentCapacity, Is.EqualTo(1));
            }

            [Test]
            public void CarouselOfCakes_TakeCakeFromCarousel_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                Cake cake = new Cake("Pistachio Perfection");
                carousel.AddCake(cake);
                // Act
                Cake retrievedCake = carousel.GetCake("Pistachio Perfection");
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(retrievedCake.GetName(), Is.EqualTo("Pistachio Perfection"));
                Assert.That(currentCapacity, Is.EqualTo(0));
            }

            [Test]
            public void CarouselOfCakes_SearchNonexistentCakeInCarousel_Succeeded()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                Cake cake = new Cake("Coconut Dream");
                carousel.AddCake(cake);
                // Act
                Cake retrievedCake = carousel.GetCake("Mocha Marvel");
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(retrievedCake.GetName(), Is.EqualTo("empty"));
                Assert.That(currentCapacity, Is.EqualTo(1));
            }

            [Test]
            public void CarouselOfCakes_MaxCapacityInCarousel_Succeeded1()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                int maxCapacity = 12;
                // Act
                for (int i = 0; i < maxCapacity; i++)
                {
                    Cake cake = new Cake("Cake" + i);
                    carousel.AddCake(cake);
                }
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(currentCapacity, Is.EqualTo(maxCapacity));
            }

            [Test]
            public void CarouselOfCakes_MaxCapacityInCarousel_Succeeded2()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                int maxCapacity = 12;
                // Act
                for (int i = 0; i < maxCapacity + 1; i++)
                {
                    Cake cake = new Cake("Cake" + i);
                    carousel.AddCake(cake);
                }
                int currentCapacity = carousel.GetCurrentCapacity();
                // Assert
                Assert.That(currentCapacity, Is.EqualTo(maxCapacity));
            }

            [Test]
            public void CarouselOfCakes_MaxCapacityInCarousel_Succeeded3()
            {
                // Arrange
                CarouselOfCakes carousel = new CarouselOfCakes();
                int maxCapacity = 12;

                // Act
                for (int i = 0; i < maxCapacity; i++)
                {
                    Cake cake = new Cake("Cake" + i);
                    carousel.AddCake(cake);
                }

                // Capture the standard output to check the displayed error message
                using (StringWriter errorBuffer = new StringWriter())
                {
                    Console.SetOut(errorBuffer);

                    // Act
                    Cake extraCake = new Cake("Extra Cake");
                    carousel.AddCake(extraCake);

                    // Assert
                    Assert.That(errorBuffer.ToString().Trim(), Is.EqualTo("The carousel is already full!"));
                }
            }
        }
    }
}