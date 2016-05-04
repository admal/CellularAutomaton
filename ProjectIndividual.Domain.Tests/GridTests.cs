using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.Domain.Tests
{
    [TestClass]
    public class GridTests
    {
        private Grid complexGrid;
        private Grid simpleGrid;
        public GridTests()
        {
            var initCells = new List<Cell>();
            initCells.Add(new Cell(new Position(7, 3), CellState.Alive));
            initCells.Add(new Cell(new Position(10, 4), CellState.Alive));
            initCells.Add(new Cell(new Position(11, 4), CellState.Alive));
            initCells.Add(new Cell(new Position(11, 5), CellState.Dead));
            initCells.Add(new Cell(new Position(10, 5), CellState.Dead));
            initCells.Add(new Cell(new Position(12, 5), CellState.Dead));

            //sentence1: 3 dead cells in the whole neighbourhood
            Sentence sentence1 = new Sentence(3, CellState.Dead, Area.Neghbourhood, 0);
            //sentence2: 2 alive cells in the 2nd row
            Sentence sentence2 = new Sentence(2, CellState.Alive, Area.Row, 2);
            //sentence2: 1 alive cell in the 1st column
            Sentence sentence3 = new Sentence(1, CellState.Alive, Area.Column, 1);

            var statements = new List<Statement>();
            statements.Add(new Statement(null, sentence1));
            statements.Add(new Statement(LogicalConnector.Or, sentence2));
            statements.Add(new Statement(LogicalConnector.Or, sentence3));
            var rule1 = new Rule(statements, CellState.Dead, 1,CellState.Any );
            var rule2 = new Rule(statements, CellState.Alive, 2,CellState.Any);
            var rules = new List<Rule>();
            rules.Add(rule2);
            rules.Add(rule1);
            complexGrid = new Grid(initCells, new RulesSet(rules));



            //24 unvisited cells in neighbourhood
            Sentence simpleSentence = new Sentence(24,CellState.Unvisited, Area.Neghbourhood, 0);
            var simpleStatements = new List<Statement>()
            {
                new Statement(null, simpleSentence)
            };
            var simpleRule = new Rule(simpleStatements,CellState.Alive, 1, CellState.Any);
            simpleGrid = new Grid(
                new List<Cell>() {new Cell(new Position(0, 0), CellState.Alive)}
                ,new RulesSet(new List<Rule>() { simpleRule }) );
        }
        [TestMethod]
        public void GetWidth()
        {
            long width1 = simpleGrid.Width;
            long width2 = complexGrid.Width;

            Assert.AreEqual(0,width1);
            Assert.AreEqual(5, width2);
        }
        [TestMethod]
        public void GetHeight()
        {
            long height1 = simpleGrid.Height;
            long height2 = complexGrid.Height;

            Assert.AreEqual(0, height1);
            Assert.AreEqual(2, height2);
        }
        [TestMethod]
        public void AddNeighboursSimpleGrid()
        {

            simpleGrid.AddNeighbours();
            Assert.AreEqual(25, simpleGrid.VisitedCells.Count);
        }
        [TestMethod]
        public void AddNeighboursComplexGrid()
        {
            complexGrid.AddNeighbours();
            Assert.AreEqual(58,complexGrid.VisitedCells.Count); //52 - unvisited, 6 - visited cells (added in constructor)
        }
        [TestMethod]
        public void UpdateSimpleGrid()
        {
            simpleGrid.UpdateGrid();
            Assert.AreEqual(CellState.Alive, simpleGrid.GetCellState(0,0));
        }
        [TestMethod]
        public void UpdateComplexGrid()
        {
            foreach (var visitedCell in complexGrid.VisitedCells)
            {
                Console.WriteLine(visitedCell.Value.ToString());
            }
            Console.WriteLine("Update");
            Console.WriteLine();
            complexGrid.UpdateGrid();
            foreach (var visitedCell in complexGrid.VisitedCells)
            {
                Console.WriteLine(visitedCell.Value.ToString());
            }
            //Assert.AreEqual();
        }
        [TestMethod]
        public void UpdateGridWithRowRules()
        {
            var initCells = new List<Cell>()
            {
                new Cell(new Position(0,0),CellState.Alive )
            };
            Sentence sentence = new Sentence(1,CellState.Alive, Area.Row, 1);
            var statements = new List<Statement>()
            {
                new Statement(null,sentence)
            };
            var rules = new List<Rule>() {new Rule(statements, CellState.Dead, 1, CellState.Any)};

            Grid grid = new Grid(initCells,new RulesSet(rules));

            grid.UpdateGrid();
            Assert.AreEqual(CellState.Dead, grid.GetCellState(-2,2));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(-1, 2));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(0, 2));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(1, 2));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(2, 2));
        }
        [TestMethod]
        public void UpdateGridWithColumnRules()
        {
            var initCells = new List<Cell>()
            {
                new Cell(new Position(0,0),CellState.Alive )
            };
            Sentence sentence = new Sentence(1, CellState.Alive, Area.Column, 3);
            var statements = new List<Statement>()
            {
                new Statement(null,sentence)
            };
            var rules = new List<Rule>() { new Rule(statements, CellState.Dead, 1, CellState.Any) };

            Grid grid = new Grid(initCells, new RulesSet(rules));

            grid.UpdateGrid();
            Assert.AreEqual(CellState.Dead, grid.GetCellState(0, -2));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(0, -1));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(0, 1));
            Assert.AreEqual(CellState.Dead, grid.GetCellState(0, 2));
            Assert.AreEqual(CellState.Alive,grid.GetCellState(0,0));
        }

    }
}
