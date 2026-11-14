namespace EFDemo2.Service
{
    public class CodeGenerator : ICodeGenerator
    {
        public string GenerateFeeCode()
        {
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random random = new Random();
            string rcode = "S";
            int year = DateTime.Now.Year;
            rcode += year;
            int month = DateTime.Now.Month;
            if(month < 10)
            {
                rcode += "0" + month;
            }
            else
            {
                rcode += month;
            }
            rcode += random.NextInt64(100, 999); // Generate a random number between 100 and 999
            //string feeCode = $"FEE-{randomNumber}"; // Create the fee code with the prefix "FEE-"
            return rcode;
        }
    
    }
}
