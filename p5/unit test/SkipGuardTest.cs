global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
namespace skipGuardTest;

[TestClass]
public class SkipGuardObjTest
{
    [TestMethod]
    public void TestSkipConstructor()
    {
        //Arrange
        int[] handle = { 1, 3, 6 };
        skipGuard g = new skipGuard(handle);
        //Act
        int k = g.kGetter;// k = 10 % 3 = 1
        //Assert
        Assert.AreEqual(1, k);
    }

    [TestMethod]
    public void TestSkipBlockUp()
    {
        //Arrange
        int[] handle = { 1, 3, 6 };
        skipGuard s = new skipGuard(handle);
        Guard g = new Guard(handle);
        //Act
        int k = s.kGetter;// k = 10 % 3 = 1
        s.block(0);
        int sD = s.DurabilityGetter[1];
        g.block(1);
        int gD = g.DurabilityGetter[1];
        //Assert
        Assert.AreEqual(1, k);
        Assert.AreEqual(sD, gD);
    }

    [TestMethod]
    public void TestSkipBlockDown()
    {
        //Arrange
        int[] handle = { 1, 3, 6, 2, 2, 1 };//15->down
        skipGuard c = new skipGuard(handle);
        Guard b = new Guard(handle);
        //Act
        int k = c.kGetter;// k = 15 % 6 = 3
        c.block(0);//1+3
        b.block(3);
        int fromBase = b.DurabilityGetter[3];
        int fromChild = c.DurabilityGetter[3];
        //Assert
        Assert.AreEqual(0, fromBase);
        Assert.AreEqual(0, fromChild); 
    }
}
