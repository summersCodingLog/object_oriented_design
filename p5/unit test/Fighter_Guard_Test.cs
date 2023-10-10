global using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using P5;
namespace Fighter_Guard_Test;

[TestClass]
public class FG_Obj_Test
{
    [TestMethod]
    public void F_Guard_Constructor()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };
        FighterGuard obj = new FighterGuard(handle);
        Guard guard = new Guard(handle);
        //Act
        int[] objArr = obj.ArtilleryGetter;
        int[] guardArr = guard.DurabilityGetter;
        //Assert
        CollectionAssert.AreEqual(objArr, guardArr);
    }

    [TestMethod]
    public void F_SkipGuard_Constructor()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };
        TurretGuard obj = new TurretGuard(handle, 2);
        skipGuard skip = new skipGuard(handle);
        //Act
        int[] objArr = obj.ArtilleryGetter;
        int[] guardArr = skip.DurabilityGetter;
        //Assert
        CollectionAssert.AreEqual(objArr, guardArr);
    }

    [TestMethod]
    public void F_QuirkyGuard_Constructor()
    {
        //Arrange
        int[] handle = { 1, 3, 5 };
        InfantryGuard obj = new InfantryGuard(handle, 3);
        quirkyGuard quirky = new quirkyGuard(handle);
        //Act
        int[] objArr = obj.ArtilleryGetter;
        int[] guardArr = quirky.DurabilityGetter;
        //Assert
        CollectionAssert.AreEqual(objArr, guardArr);
    }

    [TestMethod]
    public void FGuard_IsActive()
    {
        //Arrange
        int[] arr = { 1, 3, 5 };
        FighterGuard fg = new FighterGuard(arr);
        //Act
        bool TrueTest = fg.IsActive();
        //Assert
        Assert.IsTrue(TrueTest, "Expected 'IsActive' to be true");
    }

    [TestMethod]
    public void FGuard_IsNotActive()
    {
        //Arrange
        int[] arr = { };
        FighterGuard fg = new FighterGuard(arr);
        //Act 
        bool UnarmedTest = fg.IsAlive();
        //Assert
        Assert.IsFalse(UnarmedTest, "Expected 'IsActive' to be false for un-armed object");
    }

    [TestMethod]
    public void FGuard_IsAlive()
    {
        //Arrange
        int[] arr = { 1, 3, 5 };//9
        FighterGuard fg = new FighterGuard(arr);
        //Act
        bool TrueTest = fg.IsAlive();
        //Assert
        Assert.IsTrue(TrueTest, "Expected 'IsAlive' to be true");
    }

    [TestMethod]
    public void FGuard_IsNotAlive()
    {
        //Arrange
        int[] arr = { };
        FighterGuard fg = new FighterGuard(arr);
        //Act 
        bool UnarmedTest = fg.IsAlive();
        //Assert
        Assert.IsFalse(UnarmedTest, "Expected 'IsAlive' to be false for un-armed object");
    }

    [TestMethod]
    public void Guard_HasDefense()
    {
        //Arrange
        int[] handle = { 1, 2, 4 };//7
        FighterGuard fg = new FighterGuard(handle);
        Guard g = new Guard(handle);
        //Act
        bool FGalive = fg.IsAlive();
        bool Galive = g.IsAlive();
        //Assert
        Assert.IsTrue(FGalive, "Expect IsAlive is true when half of the shields are viable.");
        Assert.AreEqual(FGalive, Galive);
    }

    [TestMethod]
    public void FGuard_NotHaveDefense()
    {
        //Arrange
        int[] handle = { 1, 1, 1, 1, 1, 10 };//15
        FighterGuard fg = new FighterGuard(handle);
        Guard g = new Guard(handle);
        //Act
        bool FGDefense = fg.HasDefense();
        bool Galive = g.IsAlive();//F
        //Assert
        Assert.IsFalse(Galive,"guard is not alive.");
        Assert.AreEqual(FGDefense, Galive);
        Assert.IsFalse(FGDefense, "Expect IsAlive is false when hald of the shields are not viable.");
    }

    [TestMethod]
    public void FG_Attack_True()
    {
        //Arrange
        //known from TestFighterShift(), attack range is (-3,3)
        int[] arr = { 1, 2, 3 };
        FighterGuard fg = new FighterGuard(arr);
        Fighter f = new Fighter(arr);

        //Act
        //if(x,y in range(-3, 3) && q < 8) -> True 
        int expectStrength = Convert.ToInt32(f.StrengthGetter * 0.9);//7
        bool fgAttack = fg.Attack(-3, 3, 7);
        bool fTarget = f.Target(-3, 3, 7);
        //value decreased after call Target() 
        int fStrength = f.StrengthGetter;//7,former 8
        int fgStrength = fg.StrengthGetter;
        //Assert
        Assert.IsTrue(fTarget, "Fighter's Target expected to be true");
        Assert.IsTrue(fgAttack, "FighterGuard's Attack expected to be true");
        Assert.AreEqual(expectStrength, fStrength, "Expect 1st Target deduct Strength correctly");
        Assert.AreEqual(fgStrength, fStrength);
    }

    [TestMethod]
    public void FG_Attack_False()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };
        FighterGuard fg = new FighterGuard(arr);
        Fighter f = new Fighter(arr);
        //Act
        //if(x,y in range(-3, 3) && q < 8) -> True
        int fStrength = f.StrengthGetter;
        bool TargetArtilleryFalse = f.Target(0, 0, 8);
        bool fgAttack = fg.Attack(0, 0, 8);
        //Assert
        Assert.AreEqual(8, fStrength);
        Assert.IsFalse(TargetArtilleryFalse, "Target Expect false when Artillery < q");
        Assert.IsFalse(fgAttack, "Attack Expect false when Artillery < q");
    }

    [TestMethod]
    public void Guard_No_Hurt()
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
        int[] arr = { 1, 2, 3, 4};//k = 2, up
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

    [TestMethod]
    public void Guard_Revive()
    {
        //Arrange
        int[] arr = { 1, 3, 5 };//strength = 13; !IsAlive <= 2
        FighterGuard fg = new FighterGuard(arr);
        //Act
        int before = fg.StrengthGetter;
        fg.Hurt(2, 3);//hurt: 13 - 3 = 10
        int first = fg.StrengthGetter;
        fg.Hurt(2, 5);//strength = 5
        int middle = fg.StrengthGetter;//5
        fg.Hurt(2, 3);//strength = 2; !IsAlive 
        //at near dead state to trigger revive
        int after = fg.StrengthGetter;//13
        //Assert
        Assert.AreEqual(13, before);
        Assert.AreEqual(10, first);
        Assert.AreEqual(5, middle);
        Assert.AreEqual(13, after);
    }

}
