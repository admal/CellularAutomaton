using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectIndividual.Domain.FileManagment;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.Domain.Tests
{
    [TestClass]
    public class LoadingCreatingFileTests
    {
        private Grid testGrid;

        public LoadingCreatingFileTests()
        {
            var initCells = new List<Cell>();
            initCells.Add(new Cell(new Position(7, 3), CellState.Alive));
            initCells.Add(new Cell(new Position(12, 5), CellState.Dead));

            testGrid = new Grid(initCells);
        }
        [TestMethod]
        public void CreateFile()
        {
            FileCreator.WriteToBinaryFile(
                @"C:\Users\Adam\Documents\visual studio 2015\Projects\ProjectIndividual\Grids\grid1.grid", testGrid);

        }
        [TestMethod]

        [DeploymentItem(@"C:\Users\Adam\Documents\visual studio 2015\Projects\ProjectIndividual\Grids\grid1.grid","targetFolder")]
        public void LoadFile()
        {
            var ret = FileLoader.ReadFromBinaryFile<Grid>(
                @"C:\Users\Adam\Documents\visual studio 2015\Projects\ProjectIndividual\Grids\grid1.grid");
            Assert.AreEqual(testGrid.VisitedCells.Count, ret.VisitedCells.Count);
            Assert.AreEqual(testGrid.Height, ret.Height);
        }
    }
}
