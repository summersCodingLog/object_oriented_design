/*
* Summer Xia - cpsc3200
* 4 / 4 / 23
* revision history: 3/31 -> 4/3 -> 4/4/2023
*/

global using Microsoft.VisualStudio.TestTools.UnitTesting;
using p1;

namespace LumenTest
{
    [TestClass]
    public class LumenObjTests
    {
        [TestMethod]
        public void TestLumenConstructor()
        {
            // Arrange
            uint testBrightness = 100;
            uint testSize = 4;

            // Act
            Lumen light = new Lumen(testBrightness, testSize);

            // Assert
            Assert.AreEqual(testBrightness, light.GetBrtns);
            Assert.AreEqual(testSize, light.GetSize);
            Assert.AreEqual(testBrightness * testSize, light.GetPower);
        }

        [TestMethod]
        public void TestIsStable()
        {
            //is Stable <= 400
            // Arrange
            uint testBrightness = 100;
            uint testSize = 4;
            Lumen light = new Lumen(testBrightness, testSize);

            //Act
            bool isStableLow = light.IsStable();//Power <= 400

            // Change the power value to test the false case
            uint testBrightness1 = 101;
            uint testSize1 = 4;
            Lumen light1 = new Lumen(testBrightness1, testSize1);

            //light.GetPower = 401;
            bool isStableHigh = light1.IsStable();//Power <= 400

            //Assert
            Assert.IsTrue(isStableLow, "Expected IsStable() to be true for Power <= 400");
            Assert.IsFalse(isStableHigh, "Expected IsStable() to be false for Power >= 400");
            

        }


        [TestMethod]
        public void TestIsActive()
        {
            //isActive >= 50
            //Aarrange
            uint testBrightness = 50;
            uint testSize = 1;
            Lumen light = new Lumen(testBrightness, testSize);
            //Act
            bool IsActiveHigh = light.IsActive();
            // Change the power value to test the false case

            uint testBrightness1 = 49;
            uint testSize1 = 1;
            Lumen light1 = new Lumen(testBrightness1, testSize1);
            //light.GetPower = 49;
            bool isSActiveLow = light1.IsActive();//Power <= 400
            //Assert
            Assert.IsTrue(IsActiveHigh, "Expected IsActive() to be true for power >= 50");
            Assert.IsFalse(isSActiveLow, "Expected IsActive() to be false for power < 50");
        }


        [TestMethod]
        public void TestGlow()
        {
         
            //Test 1 : !Active, pow < 50
            //Power = 49;
            uint testBrightness1 = 49;
            uint testSize1 = 1;
            Lumen light1 = new Lumen(testBrightness1, testSize1);

            uint expected1 = Convert.ToUInt32(light1.GetBrtns * 0.2);
            uint result1 = light1.Glow();
            Assert.AreEqual(expected1, result1, "Not Active state expected Brightness * 0.2");

            //Test 2 : Stable, pow <= 400
            //Power = 400;
            uint testBrightness2 = 100;
            uint testSize2 = 4;
            Lumen light2 = new Lumen(testBrightness2, testSize2);
            
            uint expected2 = Convert.ToUInt32(light2.GetBrtns * light2.GetSize);
            uint result2 = light2.Glow();
            Assert.AreEqual(expected2, result2, "Stable state expected Brightness * Size");

            //Test 3: Neither(Active && !Stable), pow > 400
            //case1: pow % 2 != 0, Brightness = 0
            //Power = 404;

            uint testBrightness3 = 401;
            uint testSize3 = 1;
            Lumen light3 = new Lumen(testBrightness3, testSize3);

            uint expected3 = 0;
            uint result3 = light3.Glow();
            Assert.AreEqual(expected3 , result3, "Erratical state expected generate random machine to 0.");

            //case 2: power % 2 == 0, lucky, Brightness * 0.8
            //Power = 402;

            uint testBrightness4 = 402;
            uint testSize4 = 1;
            Lumen light4 = new Lumen(testBrightness4, testSize4);
            
            uint expected4 = Convert.ToUInt32(light4.GetBrtns * 0.8);
            uint result4 = light4.Glow();
            Assert.AreEqual(expected4, result4, "Erratical state expected generate random machine to Brightness * 0.8.");

        }

        [TestMethod]
        public void TestReset()
        {
            // Arrange
            uint testBrightness = 100;
            uint testSize = 4;
            Lumen light = new Lumen(testBrightness, testSize);
            uint initialPower = light.GetPower;//400
            uint initialBrtns = light.GetBrtns;//100

            //if failed reset1, expect brigntness = 80; power = 320
            uint expectedBrightnessF1 = Convert.ToUInt32(testBrightness * 0.8);
            uint expectedPowerF1 = Convert.ToUInt32(initialPower * 0.8);

            //if failed reset2, expect brigntness = 64; power = 256
            uint expectedBrightnessF2 = Convert.ToUInt32(expectedBrightnessF1 * 0.8);
            uint expectedPowerF2 = Convert.ToUInt32(expectedPowerF1 * 0.8);

            //if succefully reset, expect brightness = 100; Power = 400
            uint expectedBrightnessT = initialBrtns;
            uint expectedPowerT = initialPower;

            // Act
            bool result1 = light.Reset();
            bool result2 = light.Reset();
            bool result3 = light.Reset();

            // Assert
            //1st try:
            Assert.IsFalse(result1, "Expected 1st time not reset"); // GlowRequest = 1
            Assert.AreEqual(expectedBrightnessF1, 80u);
            Assert.AreEqual(expectedPowerF1, 320u);

            //2nd try:
            Assert.IsFalse(result2, "Expected 2nd time not reset"); // GlowRequest = 2
            Assert.AreEqual(expectedBrightnessF2, 64u);
            Assert.AreEqual(expectedPowerF2, 256u);

            Assert.IsTrue(result3,"Expected 3rd time to be reset"); // GlowRequest = 3, should reset
            Assert.AreEqual(expectedBrightnessT, initialBrtns);
            Assert.AreEqual(expectedPowerT, initialPower);
        }

    }
}
