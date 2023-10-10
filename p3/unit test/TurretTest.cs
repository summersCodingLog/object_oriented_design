/*
* Summer Xia - cpsc3200
* 4 / 28 / 23
* revision history: 4/27 -> 4/28/2023
*/
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3;
namespace TurretTest;

[TestClass]
public class TurretObjTest
{
    [TestMethod]
    public void TestTurretConstructor()
    {
        // Arrange
        int[] handle = { 1, 2, 3 };
        Turret turret = new Turret(handle);
        // Act
        int[] x = turret.RowAttkRangeGetter;
        int[] y = turret.ColAttkRangeGetter;
        // Assert
        Assert.AreEqual(0, turret.CountGetter);
        Assert.AreEqual(0, x[0]);
        Assert.AreEqual(0, x[x.Length - 1]);
        Assert.AreEqual(-100, y[0]);
        Assert.AreEqual(100, y[y.Length - 1]);
    }

    [TestMethod]
    public void TestTurretMove()
    {
        //Arrange
        int[] handle = { 1, 2, 3 };
        Turret turret = new Turret(handle);
        //Act
        bool expectF = turret.Move(3, 3);
        bool expectT = turret.Move(0, 0);
        int expectFailCount = 1;
        //Assert
        Assert.IsFalse(expectF, "Expect false for any move in Turret object.");
        Assert.IsTrue(expectT, "Expect true when it is not moving");
        Assert.AreEqual(expectFailCount, turret.CountGetter);

    }

    [TestMethod]
    public void TestTurretShift()
    {
        // Arrange
        int[] handle = { 1, 2, 3 };
        Turret turret = new Turret(handle);
        // Act
        turret.Shift(5);
        int[] x = turret.RowAttkRangeGetter;
        int[] y = turret.ColAttkRangeGetter;
        // Assert
        Assert.AreEqual(5, x[0]);
        Assert.AreEqual(5, x[x.Length - 1]);
        Assert.AreEqual(-100, y[0]);
        Assert.AreEqual(100, y[y.Length - 1]);
    }

    [TestMethod]
    public void TestTurretIsRevived()
    {
        //Arrange
        //range:(0,column)
        int[] handle = { 1, 2, 3 };
        Turret turret = new Turret(handle);

        //Act
        int curS = turret.StrengthGetter;//8
        int curA = turret.ArtilleryGetter;//6
        bool expectFalse = turret.IsRevived();
        turret.Target(0, 0, 0);//7,5
        turret.Target(0, 0, 0);//6,4
        turret.Target(0, 0, 0);//5,3
        turret.Target(0, 0, 0);//4,2
        turret.Target(0, 0, 0);//3,1
        turret.Target(0, 0, 0);//2,0
        turret.Target(0, 0, 0);//1,0 (!isAlive & !isActive)

        //Assert
        Assert.AreEqual(1, turret.StrengthGetter);
        Assert.AreEqual(false, turret.IsAlive());
        Assert.AreEqual(0, turret.ArtilleryGetter);
        Assert.AreEqual(false, turret.IsActive());
        bool test = turret.IsRevived();
        int PostReviveStrength = turret.StrengthGetter;
        int PostReviveArtillery = turret.ArtilleryGetter;

        if (test)
        {
            Assert.IsTrue(test);
            Assert.AreEqual(curS, PostReviveStrength);
            Assert.AreEqual(curA, PostReviveArtillery);
        }
        else Assert.IsFalse(test, "Did not reset.");

    }
}

