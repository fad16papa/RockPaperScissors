using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Helper
{
    public static class EnumHelper
    {
        private static Random random = new Random(Environment.TickCount);


        public static T Of<T>()
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException("Must use Enum type");

            Array enumValues = Enum.GetValues(typeof(T));
            return (T)enumValues.GetValue(random.Next(enumValues.Length));
        }
    }
}
