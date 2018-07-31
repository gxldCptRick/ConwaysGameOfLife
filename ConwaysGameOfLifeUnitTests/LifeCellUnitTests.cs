using System;
using ConwaysGameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysGameOfLifeUnitTests
{
    [TestClass]
    public class LifeCellUnitTests
    {
        public event EventHandler EvolutionEvent;
        [TestMethod]
        public void AliveCellStaysAliveWithTwoNeighborsWhoAreAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: true);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }
            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            //assert
            Assert.IsTrue(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void AliveCellStaysAliveWithThreeNeighborsWhoAreAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: true);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }
            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            
            //assert
            Assert.IsTrue(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void DeadCellComesBackToLifeWithThreeNeighborsWhoAreAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: false);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }
            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            //assert
            Assert.IsTrue(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void AliveCellDiesWithOnlyOneNeighborAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: true);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: false),
                new LifeCell(isAlive: false),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }

            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            //assert
            Assert.IsFalse(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void AliveCellDiesWithFourNeighborCellsWhoAreAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: true);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }

            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            
            //assert
            Assert.IsFalse(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void DeadCellStaysDeadWithOneNeighborAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: false);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: false),
                new LifeCell(isAlive: false),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }

            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            //assert
            Assert.IsFalse(cellInQuestion.IsAlive);
        }

        [TestMethod]
        public void DeadCellStaysDeadWithFourNeighborsWhoAreAlive()
        {
            //arrange
            EvolutionEvent = null;
            LifeCell cellInQuestion = new LifeCell(isAlive: false);
            LifeCell[] neighbors = {
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true),
                new LifeCell(isAlive: true)
            };

            cellInQuestion.SubscribeToEvolutionEvent(ref EvolutionEvent);
            foreach (var neighbor in neighbors)
            {
                neighbor.SubscribeToEvolutionEvent(ref EvolutionEvent);
                cellInQuestion.AddNeighbor(neighbor);
                neighbor.AddNeighbor(cellInQuestion);
            }

            //act
            cellInQuestion.CheckIfYouAreAlive();

            foreach (var neighbor in neighbors)
            {
                neighbor.CheckIfYouAreAlive();
            }

            EvolutionEvent?.Invoke(this, EventArgs.Empty);
            
            //assert
            Assert.IsFalse(cellInQuestion.IsAlive);
        }
    }
}
