/*
* Summer Xia - cpsc3200
* 4 / 28 / 23
* revision history: 4/27 -> 4/28/2023
*/
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3;

namespace InfantryTest;

[TestClass]
public class InfantryObjTest
{
    [TestMethod]
    public void TestInfantryConstructor()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };
        Infantry inf = new Infantry(arr);
        //Act
        //int ExpectedArtillery = 6;
        int RealArtillery = inf.ArtilleryGetter;
        //Assert
        Assert.AreEqual(6, RealArtillery);
    }

    [TestMethod]
    public void TestInfantryMove()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };
        Infantry inf = new Infantry(arr);
        //Act
        //pos(-10,-10)
        bool FirstStep = inf.Move(-10, -10);
        //pos(-20,-20)
        bool SecondStep = inf.Move(-10, -10);
        //pos(-20,-20)
        bool ThirdStep = inf.Move(1, -1);
        //pos(-30,-30)
        bool ForthStep = inf.Move(-10, -10);

        //Assert
        Assert.IsTrue(FirstStep, "Expected 'Move' to be true when within MovrRange");
        Assert.IsTrue(SecondStep, "Expected 'Move' to be true when within MovrRange && same direction");
        Assert.IsFalse(ThirdStep, "Expected 'Move' to be false when change direction");
        Assert.IsTrue(ForthStep, "Expected 'Move' to be true when get back same direction");
    }

    [TestMethod]
    public void TestInfantryShift()
    {
        //Arrange
        //Create a Fighter with weapon range of 3
        //default range is array's size (-3,3) in this case
        int[] weapon = { 1, 2, 3 };
        Infantry inf = new Infantry(weapon);
        //Act
        inf.Shift(30);
        int[] x = inf.RowAttkRangeGetter;
        int[] y = inf.ColAttkRangeGetter;
        //Assert
        Assert.AreEqual(-30, x[0]);
        Assert.AreEqual(-30, y[0]);
        Assert.AreEqual(30, x[x.Length - 1]);
        Assert.AreEqual(30, y[y.Length - 1]);
    }

    [TestMethod]
    public void TestInfantryReset()
    {
        //Arrange
        //known from TestInfantryShift(), attack range is (-3,3)
        int[] weapon = { 1, 2, 7 };
        Infantry inf = new Infantry(weapon);
        Assert.AreEqual(10, inf.ArtilleryGetter);//10
        int originalArtillery = inf.ArtilleryGetter;//10

        // Act
        Assert.AreEqual(10, inf.ArtilleryGetter);
        inf.Target(0, 0, 0);//9
        inf.Target(0, 0, 0);//8
        inf.Target(0, 0, 0);//7
        inf.Target(0, 0, 0);//6
        inf.Target(0, 0, 0);//5
        inf.Target(0, 0, 0);//4
        inf.Target(0, 0, 0);//3
        inf.Target(0, 0, 0);//2
        inf.Target(0, 0, 0);//1,!isActive

        //Assert
        Assert.AreEqual(1, inf.ArtilleryGetter);
        Assert.AreEqual(false, inf.IsActive());
        bool test = inf.Reset();
        int PostReset = inf.ArtilleryGetter;

        if (test)
        {
            Assert.IsTrue(test);
            Assert.AreEqual(originalArtillery, PostReset);
        }
        else Assert.IsFalse(test, "Did not reset.");
    }
}