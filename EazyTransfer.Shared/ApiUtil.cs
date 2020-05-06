using System;
using System.Collections.Generic;
using System.Text;

namespace EazyTransfer.Shared
{
    public class ApiUtil
    {
        public static string RandomDigits()
        {
            try
            {
                return DateTime.UtcNow.Ticks.ToString().Substring(0, 10);
            }
            catch (Exception ex)
            {
                return "0";
            }
          
        }

        public static string GenerateKey()
        {
            Guid obj = Guid.NewGuid();
            return obj.ToString().Substring(0, 14);
        }
    }
}
