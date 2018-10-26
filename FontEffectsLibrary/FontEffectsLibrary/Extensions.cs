using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontEffectsLibrary
{
    public static class Extensions
    {
        public static string Slice(this StringBuilder builder, int start, int length)
        {
            string text = "";
            for(int i = start; i < length; i++)
            {
                text += builder.ToString()[i];
            }
            return text;
        }
    }
}
