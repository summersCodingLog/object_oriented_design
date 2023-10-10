/*
* Summer Xia - cpsc3200
* 4 / 28 / 23
* revision history: 4/27 -> 4/28/2023
*/
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3;

namespace FighterTest;

[TestClass]
public class FighterObjTest
{
    [TestMethod]
    public void TestFigherConstrucor()
    {
        //Arrange
        int[] testArrayT = {99, 5, 11};
        int[] testArrayF = { };
        //Act
        Fighter fighterT = new Fighter(testArrayT);
        Fighter fighterF = new Fighter(testArrayF);
    
        int Ts = fighterT.StrengthGetter;
        int Ta = fighterT.ArtilleryGetter;
        int[] TRow = fighterT.RowAttkRangeGetter;
        int[] TCol = fighterT.ColAttkRangeGetter;
        bool armedTestT = fighterT.ArmedGetter;

        int Fs = fighterF.StrengthGetter;
        int Fa = fighterF.ArtilleryGetter;
        int[] FRow = fighterF.RowAttkRangeGetter;
        int[] FCol = fighterF.ColAttkRangeGetter;
        bool armedTestF = fighterF.ArmedGetter;
        //Assert
        Assert.AreEqual(27,Ts);
        Assert.AreEqual(115,Ta);
        Assert.IsTrue(armedTestT, "Expected 'Armed' to be true for non-empty array");
        Assert.AreEqual(-3, TRow[0]);
        Assert.AreEqual(3, TCol[TCol.Length - 1]);

        Assert.AreEqual(0,Fs);
        Assert.AreEqual(0,Fa);
        Assert.IsFalse(armedTestF, "Expected 'Armed' to be false for empty array");
        Assert.AreEqual(0, FRow[0]);
        Assert.AreEqual(0, FCol[FCol.Length - 1]);
    }

    [TestMethod]
    public void TestFighterMove()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };
        Fighter f = new Fighter(arr);
        //Act
        //pos(-100,-100)
        bool LowEdgeTrue = f.Move(-100,-100);
        //pos(100,100)
        bool HighEdgeTrue = f.Move(200,200);
        //pos(101,101)
        bool HighEdgeFalse = f.Move(1,1);
        //pos(-101,100)
        bool LowEdgeFalse = f.Move(-202,1);
        
        //Assert
        Assert.IsTrue(LowEdgeTrue, "Expected 'Move' to be true when within lowest MovrRange");
        Assert.IsTrue(HighEdgeTrue, "Expected 'Move' to be true when within highest MovrRange");
        Assert.IsFalse(LowEdgeFalse, "Expected 'Move' to be false when exceed lowest MovrRange");
        Assert.IsFalse(HighEdgeFalse,"Expected 'Move' to be false when exceed highest MovrRange");
    }

    [TestMethod]
    public void TestFighterShift()
    {
        //Arrange
        //Create a Fighter with weapon range of 3
        //default range is array's size (-3,3) in this case
        int[] weapon = { 1, 2, 3 };
        Fighter fighter = new Fighter(weapon);
        //Act
        fighter.Shift(99);
        int[] x = fighter.RowAttkRangeGetter;
        int[] y = fighter.ColAttkRangeGetter;
        //Assert
        Assert.AreEqual(-3, x[0]);
        Assert.AreEqual(-3, y[0]);
        Assert.AreEqual(3, x[x.Length - 1]);
        Assert.AreEqual(3, y[y.Length - 1]); 
    }

    [TestMethod]
    public void TestFighterActive()
    {
        //Arrange
        int[] arr = { };
        Fighter Unarmed = new Fighter(arr);
        int[] arr1 = { 1, 2, 3 };//6
        Fighter Normal = new Fighter(arr1);

        //Act
        bool UnarmedTest = Unarmed.IsActive();
        bool NormalTestTrue = Normal.IsActive();
        Normal.Target(0, 0, 0);//5
        Normal.Target(0, 0, 0);//4
        Normal.Target(0, 0, 0);//3
        Normal.Target(0, 0, 0);//2
        Normal.Target(0, 0, 0);//1
        int ExpectArtillery = Normal.ArtilleryGetter;
        bool NormalTestFalse = Normal.IsActive();

        //Assert
        Assert.IsFalse(UnarmedTest, "Expected 'IsActive' to be false for un-armed object");
        Assert.IsTrue(NormalTestTrue, "Expected 'IsActive' to be true for armed object");
        Assert.IsFalse(NormalTestFalse, "Expected 'IsActive' to be false for lack of Artillery while armed onject");
        Assert.AreEqual(1, ExpectArtillery);
    }

    [TestMethod]
    public void TestFighterAlive()
    {
        //Arrange
        int[] arr = { };
        Fighter Unarmed = new Fighter(arr);
        int[] arr1 = { 1, 2, 3 };//8
        Fighter Normal = new Fighter(arr1);

        //Act
        bool UnarmedTest = Unarmed.IsAlive();
        bool NormalTestTrue = Normal.IsAlive();
        Normal.Target(0, 0, 0);//7
        Normal.Target(0, 0, 0);//6
        Normal.Target(0, 0, 0);//5
        Normal.Target(0, 0, 0);//4
        Normal.Target(0, 0, 0);//3
        Normal.Target(0, 0, 0);//2
        Normal.Target(0, 0, 0);//1
        int ExpectStrength = Normal.StrengthGetter;
        bool NormalTestFalse = Normal.IsAlive();

        //Assert
        Assert.IsFalse(UnarmedTest, "Expected 'IsAlive' to be false for un-armed object");
        Assert.IsTrue(NormalTestTrue, "Expected 'IsAlive' to be true for armed object");
        Assert.IsFalse(NormalTestFalse, "Expected 'IsAlive' to be false for lack of Strength while armed onject");
        Assert.AreEqual(1, ExpectStrength);
    }

    [TestMethod]
    public void TestFighterTarget()
    {
        //Arrange
        //known from TestFighterShift(), attack range is (-3,3)
        int[] arr = { 1, 2, 3 };
        Fighter f = new Fighter(arr); 

        //Act
        //1st Tatget
        //if(x,y in range(-3, 3) && q < 6) -> True
        int expectAArtiller = Convert.ToInt32(f.ArtilleryGetter * 0.9);//5
        int expectAStrength = Convert.ToInt32(f.StrengthGetter * 0.9);//7
        bool TargetATrue = f.Target(-3, 3, 5);
        //both value decreased after call Target()
        int AArtiller = f.ArtilleryGetter;//5,former 6
        int AStrength = f.StrengthGetter;//7,former 8

        //2nd Target
        //if(x,y in range(-3, 3) && q < 5) -> True
        int expectBArtillery = Convert.ToInt32(expectAArtiller * 0.9);//4
        int expectBStrength = Convert.ToInt32(expectAStrength * 0.9);//6
        bool TargetBTrue = f.Target(3, -3, 4);
        //both value keep decreasing after call Target()
        int BArtiller = f.ArtilleryGetter;//4,former 5
        int BStrength = f.StrengthGetter;//6,former 7

        //3rd Target
        //if(x,y in range(-3, 3) && q < 4) -> True
        bool TargetArtilleryFalse = f.Target(0, 0, 4);

        //4th Target
        //if(x,y in range(-3, 3) && q < 4) -> True
        bool TargetRangeFalse = f.Target(-4, 3, 0);

        //Assert
        Assert.IsTrue(TargetATrue, "A expected to be true");
        Assert.AreEqual(expectAArtiller, AArtiller, "Expect 1st Target deduct Artillery correctly");
        Assert.AreEqual(expectAStrength, AStrength, "Expect 1st Target deduct Strength correctly");

        Assert.IsTrue(TargetBTrue, "B expected to be true");
        Assert.AreEqual(expectBArtillery, BArtiller, "Expect 2nd Target deduct Artillery correcttly");
        Assert.AreEqual(expectBStrength, BStrength, "Expect 2nd Target deduct Strength correctly");

        Assert.IsFalse(TargetArtilleryFalse,"Expect false when Artillery < q");
        Assert.IsFalse(TargetRangeFalse,"Expect false when target out of attack range.");
    }

    [TestMethod]
    public void TestFighterSum()
    {
        //Arrange
        int[] arr = { 1, 2, 3 };
        Fighter f = new Fighter(arr);
        //Act
        bool TargetATrue = f.Target(0, 0, 0);//T, sum+1
        bool TargetBTrue = f.Target(-3, 3, 3);//T, sum+1
        bool TargetArtilleryFalse = f.Target(0, 0, 6);//F
        bool TargetRangeFalse = f.Target(-4, 3, 0);//F
        
        int realSum = f.Sum();
        int expectSum = 2;
        //Assert
        Assert.AreEqual(expectSum, realSum);
    }

}
