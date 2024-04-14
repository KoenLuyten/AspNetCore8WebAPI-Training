using Microsoft.AspNetCore.Authorization;

namespace AspCoreIdentityDemo
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; }

        public MinimumAgeRequirement(int age) => Age = age;
    }
}
