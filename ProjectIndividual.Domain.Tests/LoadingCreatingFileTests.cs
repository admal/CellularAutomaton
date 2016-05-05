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
        //[TestMethod]
        //public void CreateFile()
        //{
        //    FileCreator.WriteToBinaryFile(
        //        @"C:\Users\Adam\Documents\visual studio 2015\Projects\ProjectIndividual\Grids\grid1.grid", testGrid);



        //    var initCells = new List<Cell>();
        //    initCells.Add(new Cell(new Position(7, 3), CellState.Alive));
        //    initCells.Add(new Cell(new Position(10, 4), CellState.Alive));
        //    initCells.Add(new Cell(new Position(11, 4), CellState.Alive));
        //    initCells.Add(new Cell(new Position(11, 5), CellState.Dead));
        //    initCells.Add(new Cell(new Position(10, 5), CellState.Dead));
        //    initCells.Add(new Cell(new Position(12, 5), CellState.Dead));

        //    //sentence1: 3 dead cells in the whole neighbourhood
        //    Sentence sentence1 = new Sentence(3, CellState.Dead, Area.Neghbourhood, 0);
        //    //sentence2: 2 alive cells in the 2nd row
        //    Sentence sentence2 = new Sentence(2, CellState.Alive, Area.Row, 2);
        //    //sentence2: 1 alive cell in the 1st column
        //    Sentence sentence3 = new Sentence(1, CellState.Alive, Area.Column, 1);

        //    var statements = new List<Statement>();
        //    statements.Add(new Statement(null, sentence1));
        //    statements.Add(new Statement(LogicalConnector.Or, sentence2));
        //    statements.Add(new Statement(LogicalConnector.Or, sentence3));
        //    var rule1 = new Rule(statements, CellState.Dead, 1, CellState.Any);
        //    var rule2 = new Rule(statements, CellState.Alive, 2, CellState.Any);
        //    var rules = new List<Rule>();
        //    rules.Add(rule2);
        //    rules.Add(rule1);
        //    var set = new RulesSet(rules);
        //    set.Name = "Test rules";
        //    Grid complexGrid = new Grid(initCells, set );
        //    FileCreator.WriteToBinaryFile(@"C:\Users\Adam\Documents\visual studio 2015\Projects\ProjectIndividual\Grids\grid2.grid",complexGrid);

        //}
        //[TestMethod]
        //public void CreateRuleSet()
        //{
        //    Sentence sentence1 = new Sentence(2,CellState.Alive, Area.Column, 3); //if alive and sentence1 then dead

        //    Sentence sentence2 = new Sentence(3, CellState.Dead, Area.Column, 1); //if dead and sentence3 then alive

        //    var rule1 = new Rule(new List<Statement>() {new Statement(null,sentence1)},CellState.Dead, 1,CellState.Alive);
        //    //var rule2 = new Rule(new List<Statement>() { new Statement(null, sentence2) }, CellState.Alive, 2, CellState.Dead);
        //    var rule3 = new Rule(new List<Statement>() { new Statement(null, sentence2) }, CellState.Alive, 2, CellState.Unvisited);

        //    RulesSet set = new RulesSet(new List<Rule>() {rule1, rule3});
        //    set.Name = "Moving column rule";
        //    FileCreator.WriteToBinaryFile(@"C:\Users\Adam\Source\Repos\CellularAutomaton\Rules\rule2.rules",set);
        //}
        //[TestMethod]
        //public void CreateGridFile()
        //{
        //    var initCells = new List<Cell>();
        //    initCells.Add(new Cell(new Position(0, 0), CellState.Alive));
        //    initCells.Add(new Cell(new Position(0, 1), CellState.Alive));
        //    initCells.Add(new Cell(new Position(0, 2), CellState.Alive));
        //    initCells.Add(new Cell(new Position(0, 15), CellState.Alive));
        //    initCells.Add(new Cell(new Position(0, 16), CellState.Alive));
        //    initCells.Add(new Cell(new Position(0, 17), CellState.Alive));

        //    initCells.Add(new Cell(new Position(-2, 0), CellState.Dead));
        //    initCells.Add(new Cell(new Position(-2, 1), CellState.Dead));
        //    initCells.Add(new Cell(new Position(-2, 2), CellState.Dead));
        //    initCells.Add(new Cell(new Position(-2, 15), CellState.Dead));
        //    initCells.Add(new Cell(new Position(-2, 16), CellState.Dead));
        //    initCells.Add(new Cell(new Position(-2, 17), CellState.Dead));
        //    Grid grid = new Grid(initCells);
        //    FileCreator.WriteToBinaryFile(@"C:\Users\Adam\Source\Repos\CellularAutomaton\Grids\otherGrid.grid", grid);
        //}
        //[TestMethod]

        //[DeploymentItem(@"C:\Users\Adam\Source\Repos\CellularAutomaton\Grids\growingGreedWithRules.grid", "targetFolder")]
        //public void LoadFile()
        //{
        //    var ret = FileLoader.ReadFromBinaryFile<Grid>(
        //        @"C:\Users\Adam\Source\Repos\CellularAutomaton\Grids\growingGreedWithRules.grid");

        //    Assert.AreEqual(testGrid.VisitedCells.Count, ret.VisitedCells.Count);
        //    Assert.AreEqual(testGrid.Height, ret.Height);
        //}
    }
}
