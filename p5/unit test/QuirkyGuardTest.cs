global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
namespace QuirkyGuardTest;

[TestClass]
public class QuirkyGuardObjTest
{
    [TestMethod]
    public void TestQuirkyConstructor()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };
        quirkyGuard g = new quirkyGuard(handle);
        //Act
        int a = g.DurabilityGetter[0];
        int b = g.DurabilityGetter[1];
        int c = g.DurabilityGetter[2];
        //Assert
        Assert.AreEqual(1, a);
        Assert.AreEqual(3, b);
        Assert.AreEqual(5, c);
    }

    [TestMethod]
    public void TestQuirkyBlock()
    {
        //Arrange
        int[] handle = { 1, 3, 6 };//10 -> up
        quirkyGuard quirky = new quirkyGuard(handle);
        Guard guard = new Guard(handle);
        //Act
        quirky.block(2);//unknown
        guard.block(2);//6
        int fromBase = guard.DurabilityGetter[2];

        int sereat = quirky.selectedShield;
        int fromChild = quirky.DurabilityGetter[sereat];

        //Assert
        if (sereat != 2)
        {
            Assert.AreNotEqual(fromBase, fromChild, "Random are likely to be not equal.");
        }
        else  Assert.AreEqual(fromBase, fromChild, "Random could be equal in rare cases.");
        
    }
}