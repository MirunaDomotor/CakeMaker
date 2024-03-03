using CakeMakerProj;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste
{
    public class StubAndMockTests
    {

        // CommandTaker
        [TestFixture]
        public class CommandTakerTests
        {
            private StringWriter stringWriter;
            private TextWriter originalConsoleOut;
            private ICarouselOfCakes carouselEmptyStub;
            private ICarouselOfCakes carouselFullStub;

            [SetUp]
            public void RedirectConsoleOutput()
            {
                // Redirect Console.Out to a StringWriter
                stringWriter = new StringWriter();
                originalConsoleOut = Console.Out;
                Console.SetOut(stringWriter);
            }

            [SetUp]
            public void SetUp()
            {
                Cake[] cakes1 = new Cake[12];
                Cake[] cakes2 = new Cake[12];
                for (int i = 0; i < 12; i++)
                {
                    cakes1[i] = new Cake("empty");
                    cakes2[i] = new Cake("Cheesecake");
                }
                carouselEmptyStub = new CarouselOfCakesStub(cakes1);
                carouselFullStub = new CarouselOfCakesStub(cakes2);
            }

            [TearDown]
            public void RestoreConsoleOutput()
            {
                // Restore Console.Out to its original value
                Console.SetOut(originalConsoleOut);
                stringWriter.Dispose();
            }

            [Test]
            public void CommandTaker_CheckCarouselOfCakesIsNotEmpty_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                commandTaker.SetCarouselOfCakes(carouselFullStub);
                // Act
                bool result = commandTaker.CheckCarouselOfCakes();
                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void CommandTaker_CheckCarouselOfCakesIsEmpty_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                commandTaker.SetCarouselOfCakes(carouselEmptyStub);
                // Act
                bool result = commandTaker.CheckCarouselOfCakes();
                // Assert
                Assert.IsFalse(result);
            }

            [Test]
            public void CommandTaker_TakeCommandWithAvailableCakeInCarousel_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                RecipeCake recipe = new RecipeCake("Cheesecake", 2);
                commandTaker.SetCarouselOfCakes(carouselFullStub); // Set the stub instead of the actual CarouselOfCakes object
                // Act
                Cake result = commandTaker.TakeCommand(recipe);
                // Assert
                Assert.That(result.GetName(), Is.EqualTo("Cheesecake"));
            }

            [Test]
            public void CommandTaker_TakeCommandWithNoAvailableCakeInCarousel_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                RecipeCake recipe = new RecipeCake("Cheesecake", 2);
                commandTaker.SetCarouselOfCakes(carouselEmptyStub);   // Set the stub instead of the actual CarouselOfCakes object
                // Act
                Cake result = commandTaker.TakeCommand(recipe);
                // Assert
                Assert.That(result.GetName(), Is.EqualTo("Cheesecake"));
            }

            [Test]
            public void CommandTaker_TakeCommandMultipleCakes_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                RecipeCake recipe = new RecipeCake("Cheesecake", 2);
                commandTaker.SetCarouselOfCakes(carouselFullStub);
                int nrOfCakes = 3;
                int countCakes = 0;
                // Act
                Cake[] result = commandTaker.TakeCommand(recipe, nrOfCakes);
                foreach (Cake cake in result)
                {
                    if (cake.GetName() != "empty")
                    {
                        countCakes++;
                    }
                }
                // Assert
                Assert.That(countCakes, Is.EqualTo(nrOfCakes));
                for (int i = 0; i < nrOfCakes; i++)
                {
                    var cake = result[i];
                    Assert.That(cake.GetName(), Is.EqualTo("Cheesecake"));
                }
            }

            [Test]
            public void CommandTaker_RefillCarousel_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                commandTaker.SetCarouselOfCakes(carouselEmptyStub);
                // Act
                commandTaker.RefillCarousel();
                // Assert
                Cake[] storage = commandTaker.GetCarouselOfCakes().GetStorage();
                int nonEmptyCakes = Array.FindAll(storage, cake => cake.GetName() != "empty").Length;
                Assert.GreaterOrEqual(nonEmptyCakes, carouselEmptyStub.GetMaxCapacity());
            }

            [Test]
            public void CommandTaker_DontRefillCarousel_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                commandTaker.SetCarouselOfCakes(carouselFullStub);
                int capacityBefore = carouselFullStub.GetCurrentCapacity();
                // Act
                commandTaker.RefillCarousel();
                // Assert
                Assert.That(carouselFullStub.GetCurrentCapacity(), Is.EqualTo(capacityBefore));
            }

            [Test]
            public void CommandTaker_GetCakesFromCarousel_Succeeded()
            {
                // Arrange
                CommandTaker commandTaker = new CommandTaker();
                commandTaker.SetCarouselOfCakes(carouselFullStub);
                // Act
                Cake[] resultCakes = commandTaker.GetCakesFromCarousel();
                // Assert
                Assert.That(resultCakes.Length, Is.EqualTo(carouselFullStub.GetCurrentCapacity()));
                for (int i = 0; i < carouselFullStub.GetCurrentCapacity(); i++)
                {
                    Assert.That(resultCakes[i].GetName(), Is.EqualTo(carouselFullStub.GetStorage()[i].GetName()));
                }
            }
        }

        [Test]
        public void RecipeCake_NegativeTimeInput_SetsTimeToZero()
        {
            // Arrange
            var recipe = new RecipeCake();

            // Mock for Console
            var consoleMock = new Mock<TextWriter>();
            Console.SetOut(consoleMock.Object);

            // Act
            recipe.SetTime(-5);

            // Assert
            Assert.That(recipe.GetTime(), Is.EqualTo(0));

            // Check if Console.WriteLine was called with a specific message
            consoleMock.Verify(w => w.WriteLine("Error: Preparation time cannot be negative. It will be set to 0."), Times.Once);
        }

        [Test]
        public void CakeMaker_TakeCommand_WritesCorrectOutput()
        {
            // Arrange
            var recipe = new RecipeCake("ChocolateCake", 3);

            // Mock for Console
            var consoleMock = new Mock<TextWriter>();
            Console.SetOut(consoleMock.Object);

            // Mock for Thread
            var threadMock = new Mock<CakeMakerProj.IThread>();
            threadMock.Setup(t => t.Sleep(It.IsAny<int>())).Verifiable();

            // Act
            var cakeMaker = new CakeMaker(threadMock.Object);
            var result = cakeMaker.TakeCommand2(recipe);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.GetName(), Is.EqualTo("ChocolateCake"));

            // Check if the Sleep method was called with the correct argument
            threadMock.Verify(t => t.Sleep(3000), Times.Once);

            // Check if the Console.WriteLine method has been called with certain messages
            consoleMock.Verify(w => w.WriteLine($"The dessert {recipe.GetName()} will be ready in {recipe.GetTime()} seconds!"), Times.Once);
            consoleMock.Verify(w => w.WriteLine($"The dessert {recipe.GetName()} is ready!"), Times.Once);
            consoleMock.Verify(w => w.WriteLine(), Times.Exactly(1));
        }


        [Test]
        public void CommandPanel_ShowProductsFromMenu_WritesCorrectOutput()
        {
            // Arrange
            CommandPanel commandPanel = new CommandPanel();
            List<RecipeCake> menu = new List<RecipeCake>
            {
                new RecipeCake("Cheesecake", 2),
                new RecipeCake("ChocolateCake", 3)
            };
            commandPanel.SetMenu(menu);

            // Mock for Console
            var consoleMock = new Mock<TextWriter>();
            Console.SetOut(consoleMock.Object);

            // Act
            commandPanel.ShowProducts();

            // Assert
            // Check that Console.WriteLine was called with the correct arguments
            consoleMock.Verify(
                // Console.WriteLine must be called four times
                x => x.WriteLine(It.IsAny<string>()), Times.Exactly(4)
            );
            // Check the contents of each call to Console.WriteLine
            consoleMock.Verify(
                x => x.WriteLine("Product Name: Cheesecake"), Times.Once
            );
            consoleMock.Verify(
               x => x.WriteLine("Preparation Time (in seconds): 2"), Times.Once
           );
            consoleMock.Verify(
               x => x.WriteLine("Product Name: ChocolateCake"), Times.Once
           );
            consoleMock.Verify(
                x => x.WriteLine("Preparation Time (in seconds): 3"), Times.Once
            );
        }

        [Test]
        public void CommandPanel_EmptyMenu_NoOutput()
        {
            // Arrange
            CommandPanel commandPanel = new CommandPanel();
            List<RecipeCake> emptyMenu = new List<RecipeCake>();
            commandPanel.SetMenu(emptyMenu);

            // Mock for Console
            var consoleMock = new Mock<TextWriter>();
            Console.SetOut(consoleMock.Object);

            // Act
            commandPanel.ShowProducts();

            // Assert
            // Check if Console.WriteLine was not called at all
            consoleMock.Verify(
                x => x.WriteLine(It.IsAny<string>()), Times.Never
            );
        }

        [Test]
        public void CommandPanel_SelectProduct_PrintsCorrectMessage()
        {
            // Arrange
            CommandPanel commandPanel = new CommandPanel();
            List<RecipeCake> menu = new List<RecipeCake>
            {
                new RecipeCake("Cheesecake", 2)
            };
            commandPanel.SetMenu(menu);

            // Stub for CommandTaker
            var commandTakerStub = new Mock<ICommandTaker>();
            commandTakerStub.Setup(taker => taker.TakeCommand(It.IsAny<RecipeCake>())).Returns(new Cake("Cheesecake"));
            commandPanel.SetCommandTaker(commandTakerStub.Object);

            // Redirect Console.Out to a StringWriter
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                commandPanel.SelectProduct("Cheesecake");

                // Assert
                string expectedOutput = "The cake has been served!\r\n";
                Assert.That(sw.ToString(), Is.EqualTo(expectedOutput));
            }
        }

        [Test]
        public void CommandPanel_SelectProductNotInMenu_PrintsErrorMessage()
        {
            // Arrange
            CommandPanel commandPanel = new CommandPanel();
            List<RecipeCake> menu = new List<RecipeCake>
            {
                new RecipeCake("Cheesecake", 2)
            };
            commandPanel.SetMenu(menu);

            // Stub for ICommandTaker
            var commandTakerStub = new Mock<ICommandTaker>();
            commandPanel.SetCommandTaker(commandTakerStub.Object);

            // Redirect Console.Out to a StringWriter
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                commandPanel.SelectProduct("NonexistentCake");

                // Assert
                string expectedOutput = "The product does not exist in the menu!\r\n";
                Assert.That(sw.ToString(), Is.EqualTo(expectedOutput));
            }
        }

        [Test]
        public void CommandPanel_ShowProductsInCarousel_PrintsCorrectMessage()
        {
            // Arrange
            CommandPanel commandPanel = new CommandPanel();

            // Stub for ICommandTaker
            var commandTakerStub = new Mock<ICommandTaker>();
            commandTakerStub.Setup(taker => taker.GetCakesFromCarousel())
                            .Returns(() => Enumerable.Range(0, 12)
                                                     .Select(i => i < 3 ? new Cake($"Cake{i}") : new Cake("empty"))
                                                     .ToArray())
                            .Verifiable();
            commandPanel.SetCommandTaker(commandTakerStub.Object);

            // Redirect Console.Out to a StringWriter
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                commandPanel.ShowProductsInCarousel();

                // Assert
                string expectedOutput = "The products in the carousel are:\r\nCake0 | Cake1 | Cake2 | \r\n";
                Assert.That(sw.ToString(), Is.EqualTo(expectedOutput));

                // Check if the GetCakesFromCarousel method has been called exactly once
                commandTakerStub.Verify(taker => taker.GetCakesFromCarousel(), Times.Once);
            }
        }

    }
}
