/*
* Summer Xia - cpsc3200
* 4 / 28 / 23
* revision history: 4/27 -> 4/28/2023

* Class Invariant :
* 1)public Infantry(int[] w):
* -see parent Fighter contractual details
* 2)bool Move(int x, int y)
* -Return if succefully moved Infantry
* -Error handling: Different direction or request our of range will not change position
* 3)void Shift(int p):
* -enables the infantry to attack any object within range p
* -Expect p >= 0,will auto take the absolute value of the input p
* 4)bool Reset()
* -only inactive infantry may be reset
* -Return if the object has been reset succefully
*/
using System;
namespace P3
{
    public class Infantry:Fighter
    {
        public Infantry(int[] w):base(w)
        { }

        //PreCondition: Expect input same sign as the position
        public override bool Move(int x, int y)
		{
            //may not revserse direction:
            //check if the sign stays the same
            if ((Row * x >= 0) && (Column * y >= 0))
            {
                int newRow = Row + x;
                int newCol = Column + y;
                if (MoveRange.Contains(newRow) && MoveRange.Contains(newCol))
                {
                    Row = newRow;
                    Column = newCol;
                }
                return true;
            }
            return false;
        }
        //Post-Condition:Return if succefully moved Infantry;
        //Error handling: Different direction or request our of range will not change position

        public override void Shift(int p)
        {
            p = Math.Abs(p);
            RowAttkRange = ColAttkRange = Enumerable.Range(-p, 2 * p + 1).ToArray();
        }

        public bool Reset()
        {
            if (IsActive() == false)
            {
                //get back as new
                Artillery = OriginalArtilleryGetter;
                return true;
            }
            return false;
        }

    }
}


/* implementation invariant:

 * Attributes:
 * -N/A
 * Method:
* 1)public Infantry(int[] w):
* -see parent Fighter contractual details
* 2)bool Move(int x, int y)
* -change direction not allowed meanning position sign must be the same
* -positive * positive = positive
* -negative * negative = positive
* -Thus, passed in move request x,y times current position row,column must >= 0 to be in the same direction
* 3)void Shift(int p):
* -Infantry could attack the surrounding area
* -required passed in p to be a positive number to add range on both positive and negative range
* -error handling is to cast to absolte value anyways.
* 4)bool Reset()
* -Associated with Artillery value
* -is condition fit, use the static OriginalArtillery to reset
*/