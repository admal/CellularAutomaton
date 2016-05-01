using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectIndividual.Domain.GridComponent.Entities;
using ProjectIndividual.Domain.RulesComponent.Entities;

namespace ProjectIndividual.Domain.Tests
{
    [TestClass]
    public class RulesTests
    {
        private Grid grid;
        private List<Statement> statements;
        public RulesTests()
        {
            var initCells = new List<Cell>();
            initCells.Add(new Cell(new Position(7,3), CellState.Alive ));
            initCells.Add(new Cell(new Position(10, 4), CellState.Alive));
            initCells.Add(new Cell(new Position(11, 4), CellState.Alive));
            initCells.Add(new Cell(new Position(11, 5), CellState.Alive));
            initCells.Add(new Cell(new Position(10, 5), CellState.Dead));
            initCells.Add(new Cell(new Position(12, 5), CellState.Dead));
            grid = new Grid(initCells);

            statements = new List<Statement>();
        }

        [TestMethod]
        public void ComputeProperValueOfSentence()
        {
            //sentence1: 3 dead cells in the whole neighbourhood
            Sentence sentence1 = new Sentence(3,CellState.Dead, Area.Neghbourhood, 0 );
            Cell testCell1 = grid.GetCell(10, 5);
            bool ret1 = sentence1.GetValue(grid, testCell1);

            //sentence2: 2 alive cells in the 2nd row
            Sentence sentence2 = new Sentence(2, CellState.Alive, Area.Row, 2);
            Cell testCell2 = grid.GetCell(10, 5);
            bool ret2 = sentence2.GetValue(grid, testCell2);

            //sentence2: 1 alive cell in the 1st column
            Sentence sentence3 = new Sentence(1, CellState.Alive, Area.Column, 1);
            Cell testCell3 = grid.GetCell(12, 5);
            bool ret3 = sentence3.GetValue(grid, testCell3);

            Assert.AreEqual(false, ret1);
            Assert.AreEqual(true, ret2);
            Assert.AreEqual(true, ret3);
        }
        [TestMethod]
        public void ComputeReturnCellStateFromRule()
        {
            Cell testCell1 = grid.GetCell(10, 5);
            //sentence1: 3 dead cells in the whole neighbourhood
            Sentence sentence1 = new Sentence(3, CellState.Dead, Area.Neghbourhood, 0);
            //sentence2: 2 alive cells in the 2nd row
            Sentence sentence2 = new Sentence(2, CellState.Alive, Area.Row, 2);
            //sentence2: 1 alive cell in the 1st column
            Sentence sentence3 = new Sentence(1, CellState.Alive, Area.Column, 1);
            statements.Add(new Statement(null,sentence1));
            statements.Add(new Statement(LogicalConnector.And, sentence2));
            statements.Add(new Statement(LogicalConnector.And, sentence3));
            //sentence1 and sentence2 and sentence3
            var rule1 = new Rule(statements,CellState.Alive, 1);
            var retState1 = rule1.Apply(grid, testCell1);

            statements.ForEach(s => s.Connector = LogicalConnector.Or);
            //sentence1 or sentence2 or sentence3
            var rule2 = new Rule(statements, CellState.Alive, 1);
            var retState2 = rule2.Apply(grid, testCell1);
            Assert.AreEqual(testCell1.State, retState1);
            Assert.AreEqual(CellState.Alive, retState2 );
        }
        [TestMethod]
        public void ComputeCellStateFromRuleSet()
        {
            Cell testCell1 = grid.GetCell(10, 5);
            Cell testCell2 = grid.GetCell(11, 5);
            var rule1 = new Rule(statements, CellState.Alive, 1);
            var rule2 = new Rule(statements, CellState.Alive, 2);
            var rules = new List<Rule>();
            rules.Add(rule2);
            rules.Add(rule1);
            RulesSet set = new RulesSet();
            var retState1 = set.Apply(grid, testCell1);
            var retState2 = set.Apply(grid, testCell2);
            Assert.AreEqual(CellState.Dead, retState1);
            Assert.AreEqual(CellState.Alive, retState2);
        }
    }
}
