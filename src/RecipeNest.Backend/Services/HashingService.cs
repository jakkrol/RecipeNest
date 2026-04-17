using Microsoft.AspNetCore.Identity;

namespace RecipeNest.Backend.Services
{
    public class HashingService
    {
        private readonly PasswordHasher<string> _hasher = new PasswordHasher<string>();

        public string HashUserPassword(string password)
        {
            return _hasher.HashPassword("password", password);
        }

        public bool VerifyPassword(string hashedPass, string providedPass) 
        {
            var result = _hasher.VerifyHashedPassword("password", hashedPass, providedPass);

            if(result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
