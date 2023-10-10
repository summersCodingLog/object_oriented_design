global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
namespace GuardTest;

[TestClass]
public class GuardObjTest
{
    [TestMethod]
    public void TestGuardConstructorNull()
    {
        //Arrange
        int[] handle = { };
        Guard g = new Guard(handle);
        //Act
        bool a = g.ArmedGetter;
        //Assert
        Assert.IsFalse(a, "Expect IsArmed to be false when array is null.");
    }

    [TestMethod]
    public void TestGuardConstructor()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };
        Guard g = new Guard(handle);
        //Act
        bool arm = g.ArmedGetter;
        int avg = g.AverageGetter;//(1+3+5)/2 = 4
        bool up = g.UpGetter;//9 % 3 == 0
        int shield = g.ShieldGetter;
        //Assert
        Assert.AreEqual(3, avg);
        Assert.IsTrue(arm, "Expect IsArmed to be true when array is not null.");
        Assert.IsFalse(up, "Expect IsUp is false when array is odd");
        Assert.AreEqual(3, shield);
    }

    [TestMethod]
    public void TestAliveNull()
    {
        //Arrange
        int[] handle = { };
        Guard g = new Guard(handle);
        //Act
        bool a = g.IsAlive();
        //Assert
        Assert.IsFalse(a, "Expect IsAlive to be false when array is null.");
    }

    [TestMethod]
    public void TestAliveTrue()
    {
        //Arrange
        int[] handle = { 1, 2, 4 };//7
        Guard g = new Guard(handle);
        //Act
        bool arm = g.ArmedGetter;
        bool alive = g.IsAlive();
        bool up = g.UpGetter;
        int d = g.AverageGetter;// 7/3 = 2
        //Assert
        Assert.IsTrue(arm, "Expect IsArmed to be true when array is not null.");
        Assert.IsTrue(alive, "Expect IsAlive is true when half of the shields are viable.");
        Assert.IsTrue(up, "Expect IsUp is true when totalD % 3 != 0;");
        Assert.AreEqual(2, d);
    }

    [TestMethod]
    public void TestAliveFalse()
    {
        //Arrange
        int[] handle = { 1, 1, 1, 1, 1, 10 };//15
        Guard g = new Guard(handle);
        //Act
        bool arm = g.ArmedGetter;
        bool alive = g.IsAlive();
        bool u = g.UpGetter;
        int d = g.AverageGetter;//15/6 = 2
        //Assert
        Assert.IsTrue(arm, "Expect IsArmed to be true when array is not null.");
        Assert.IsFalse(alive, "Expect IsAlive is false when hald of the shields are not viable.");
        Assert.IsFalse(u, "Expect IsUp is false when totalD totalD % 3 == 0;");
        Assert.AreEqual(2, d);
    }

    [TestMethod]
    public void TestBlockInvalid()
    {
        //Arrange
        int[] handle = { 1, 3, 4 };//8->up
        Guard g = new Guard(handle);
        //Act
        int d = g.DurabilityGetter[0];
        g.block(-1);
        int e = g.DurabilityGetter[0];
        //Assert
        Assert.AreEqual(1, d);
        Assert.AreEqual(1, e);
    }

    [TestMethod]
    public void TestBlockUp()
    {
        //Arrange
        int[] handle = { 1, 3, 4 };//8->up
        Guard g = new Guard(handle);
        //Act
        bool before = g.UpGetter;//up
        int original = g.DurabilityGetter[2];//4
        g.block(2);
        bool after = g.UpGetter;//up
        int altered = g.DurabilityGetter[2];//4*0.8 = 3
        int x = g.ShieldGetter;
        //Assert 
        Assert.IsTrue(before);
        Assert.AreEqual(4, original);
        Assert.IsTrue(after);
        Assert.AreEqual(3, altered);
    }

    [TestMethod]
    public void TestBlockDown()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };//9/3 ->down
        Guard g = new Guard(handle);
        //Act
        bool before = g.UpGetter;//down
        int original = g.DurabilityGetter[2];//5
        g.block(2);
        bool after = g.UpGetter;//up
        int altered = g.DurabilityGetter[2];//0
        //Assert
        Assert.IsFalse(before);
        Assert.AreEqual(5, original);
        Assert.IsTrue(after);
        Assert.AreEqual(0, altered);
    }
}
