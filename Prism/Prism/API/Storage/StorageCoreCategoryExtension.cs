using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.API.Storage
{
    public partial class StorageCore
    {
        private void InitializePhotoCategoryTable()
        {
            CategoryTable = new Dictionary<int, string>();
            CategoryTable[0] = "Uncategorized";
            CategoryTable[1] = "Celebrities";
            CategoryTable[2] = "Film";
            CategoryTable[3] = "Journalism";
            CategoryTable[4] = "Nude";
            CategoryTable[5] = "Black and White";
            CategoryTable[6] = "Still Life";
            CategoryTable[7] = "People";
            CategoryTable[8] = "Landscapes";
            CategoryTable[9] = "City and Architecture";
            CategoryTable[10] = "Abstract";
            CategoryTable[11] = "Animals";
            CategoryTable[12] = "Macro";
            CategoryTable[13] = "Travel";
            CategoryTable[14] = "Fashion";
            CategoryTable[15] = "Commercial";
            CategoryTable[16] = "Concert";
            CategoryTable[17] = "Sport";
            CategoryTable[18] = "Nature";
            CategoryTable[19] = "Performing Arts";
            CategoryTable[20] = "Family";
            CategoryTable[21] = "Street";
            CategoryTable[22] = "Underwater";
            CategoryTable[23] = "Food";
            CategoryTable[24] = "Fine Art";
            CategoryTable[25] = "Wedding ";
            CategoryTable[26] = "Transportation ";
            CategoryTable[27] = "Urban Exploration ";

        }

        public string GetCategoryName(int category)
        {
            if (!CategoryTable.ContainsKey(category))
            {
                return "Unknown";
            }
            else
            {
                return CategoryTable[category];
            }
        }
    }
}
