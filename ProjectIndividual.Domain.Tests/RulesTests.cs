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
        private Grid grid1;
        private Grid grid2;
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
            grid1 = new Grid(initCells);

            statements = new List<Statement>();

            var initCells2 = new List<Cell>();
            initCells2.Add(new Cell(new Position(0,0), CellState.Alive ));
            initCells2.Add(new Cell(new Position(1,0), CellState.Dead ));
            initCells2.Add(new Cell(new Position(2, 2), CellState.Dead));
            grid2 = new Grid(initCells2);
        }

        [TestMethod]
        public void ComputeProperValueOfSentence()
        {
            //sentence1: 3 dead cells in the whole neighbourhood
            Sentence sentence1 = new Sentence(3,CellState.Dead, Area.Neghbourhood, 0 );
            Cell testCell1 = grid1.GetCell(10, 5);
            Cell testCell5 = new Cell(new Position(9,5), CellState.Unvisited );
            bool ret1 = sentence1.GetValue(grid1, testCell1);
            bool ret5 = sentence1.GetValue(grid1, testCell5);

            //sentence2: 2 alive cells in the 2nd row
            Sentence sentence2 = new Sentence(2, CellState.Alive, Area.Row, 2);
            Cell testCell2 = grid1.GetCell(10, 5);
            bool ret2 = sentence2.GetValue(grid1, testCell2);
            bool ret6 = sentence2.GetValue(grid1, testCell5);
            //sentence2: 1 alive cell in the 1st column
            Sentence sentence3 = new Sentence(1, CellState.Alive, Area.Column, 1);
            Cell testCell3 = grid1.GetCell(12, 5);
            bool ret3 = sentence3.GetValue(grid1, testCell3);
            bool ret7 = sentence3.GetValue(grid1, testCell5);
            Sentence sentence4 = new Sentence(2,CellState.Dead, Area.Neghbourhood, 0);
            Cell testCell4 = grid2.GetCell(0, 0);
            bool ret4 = sentence4.GetValue(grid2, testCell4);
            

            Assert.AreEqual(false, ret1);
            Assert.AreEqual(true, ret2);
            Assert.AreEqual(true, ret3);
            Assert.AreEqual(true, ret4);
            Assert.AreEqual(false, ret5);
            Assert.AreEqual(true, ret6);
            Assert.AreEqual(true, ret7);
        }
        [TestMethod]
        public void ComputeReturnCellStateFromRule()
        {
            Cell testCell1 = grid1.GetCell(10, 5);
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
            var rule1 = new Rule(statements,CellState.Alive, 1, CellState.Any);
            var retState1 = rule1.Apply(grid1, testCell1);
           

            statements.ForEach(s => s.Connector = LogicalConnector.Or);
            //sentence1 or sentence2 or sentence3
            var rule2 = new Rule(statements, CellState.Alive, 1, CellState.Any);
            var retState3 = rule2.Apply(grid1, testCell1);
            var retState2 = rule1.Apply(grid1, new Cell(new Position(9, 5), CellState.Unvisited));
            Assert.AreEqual(testCell1.State, retState1);
            Assert.AreEqual(CellState.Alive, retState2);
            Assert.AreEqual(CellState.Alive, retState3 );
            
        }
        [TestMethod]
        public void ComputeCellStateFromRuleSet()
        {
            Cell testCell1 = grid1.GetCell(10, 5);
            Cell testCell2 = grid1.GetCell(11, 5);
            var rule1 = new Rule(statements, CellState.Alive, 1, CellState.Any);
            var rule2 = new Rule(statements, CellState.Alive, 2, CellState.Any);
            var rules = new List<Rule>();
            rules.Add(rule2);
            rules.Add(rule1);
            RulesSet set = new RulesSet();
            var retState1 = set.Apply(grid1, testCell1);
            var retState2 = set.Apply(grid1, testCell2);
            Assert.AreEqual(CellState.Dead, retState1);
            Assert.AreEqual(CellState.Alive, retState2);
        }
    }
}
