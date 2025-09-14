using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTR_Text_Editor
{
    public static class Constants
    {
        // locations in ISO
        public static Int64 OffsetTable = 0x2A1AD0;
        public static Int64 TextDataTable = 0x2A4AD4;

        public static Int64 EnglishOffsetStart = 0x2A1B48;
        public static Int64 EnglishTextStart = 0x2A4F0C;

        public static int TotalStringCount = 3073;
        public static int TotalTextLength = 74252;

        // non-English indexes that shouldn't be messed with
        public static List<int> Uneditable = new List<int>() { 36, 58, 68, 69, 70, 81, 170, 171, 172, 173, 243, 284, 285, 
                                                             286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 
                                                             298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 
                                                             310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 2028, 2480, 2481 };

        // index of the start of card names and card abilities
        public static int CardNameStart = 320;
        public static int CardAbilityStart = 1174;

		// start of offset for index 30
		public static int FirstEnglishOffset = 540;



	}
}
