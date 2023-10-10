global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
namespace Infantry_Guard_Test;

[TestClass]
public class IG_Obj_Test
{
    [TestMethod]
    public void Guard_Reset()
    {
        //Arrange
        int[] arr = { 1, 2, 4 };//7->up
        FighterGuard fg = new FighterGuard(arr);
        //Act
        int before = fg.StrengthGetter;
        fg.Hurt(2, 3);//no hurt
        int after = fg.StrengthGetter;
        //Assert
        Assert.AreEqual(before, after);
    }

    [TestMethod]
    public void Guard_Hurt()
    {
        //Arrange
        int[] arr = { 1, 3, 5 };//9->down
        FighterGuard fg = new FighterGuard(arr);
        //Act
        int before = fg.StrengthGetter;
        fg.Hurt(2, 3);//hurt: strength - 3 = 6
        int after = fg.StrengthGetter;
        //Assert
        Assert.AreEqual(13, before);
        Assert.AreEqual(10, after);
    }

    [TestMethod]
    public void Skip_Guard_No_Hurt()
    {
        //Arrange
        int[] arr = { 1, 2, 3, 4 };//k = 2, up
        FighterGuard fg = new FighterGuard(arr, 2);
        //Act
        int before = fg.StrengthGetter;
        fg.Hurt(1, 3);//no hurt, block(1+2)
        int after = fg.StrengthGetter;
        //Assert
        Assert.AreEqual(before, after);
    }

    [TestMethod]
    public void Skip_Guard_Hurt()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };//k = 0, down
        FighterGuard fg = new FighterGuard(arr, 2);
        //Act
        int before = fg.StrengthGetter;//0+2+6=8
        fg.Hurt(2, 3);//no hurt, block(2+1)
        int after = fg.StrengthGetter;//8-3=5
        //Assert
        Assert.AreEqual(8, before);
        Assert.AreEqual(5, after);
    }
}
