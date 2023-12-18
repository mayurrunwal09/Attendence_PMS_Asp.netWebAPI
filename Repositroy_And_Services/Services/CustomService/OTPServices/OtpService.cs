using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositroy_And_Services.Services.CustomService.OTPServices
{
    public class OtpService
    {
        private readonly Dictionary<string, string> otpDictionary = new Dictionary<string, string>();

        public string GenerateOtp(string userEmail)
        {
            var otp = GenerateRandomOtp();
            otpDictionary[userEmail] = otp;
            return otp;
        }

        public bool VerifyOtp(string userEmail, string enteredOtp)
        {
            if (otpDictionary.TryGetValue(userEmail, out var storedOtp))
            {
                return storedOtp == enteredOtp;
            }
            return false;
        }

        private string GenerateRandomOtp()
        {
            const string chars = "0123456789";
            var random = new Random();

           
            string otp = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return otp;
        }
    }
}
